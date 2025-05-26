using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI
{
    private Player[,] matrix;
    private int boardSize;
    private int winCondition;
    private int maxDepth;
    private List<Vector2Int> emptyCells;

    public TicTacToeAI(Player[,] matrix)
    {
        this.matrix = matrix;
        this.boardSize = (int)Mathf.Sqrt(matrix.Length);
        this.winCondition = boardSize == 3 ? 3 : (boardSize == 5 ? 4 : 5);
        this.maxDepth = 4;
        this.emptyCells = new List<Vector2Int>();
        UpdateEmptyCells();
    }

    // hàm cập nhật những ô chưa được đánh của board hiện tại
    private void UpdateEmptyCells()
    {
        emptyCells.Clear();
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (matrix[row, col] == Player.EMPTY && IsNearOccupied(row, col, 2))
                {
                    emptyCells.Add(new Vector2Int(row, col));
                }
            }
        }

        if (emptyCells.Count == 0)
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (matrix[row, col] == Player.EMPTY)
                    {
                        emptyCells.Add(new Vector2Int(row, col));
                    }
                }
            }
        }
    }

    private bool IsNearOccupied(int row, int col, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (i == 0 && j == 0) continue;
                int newRow = row + i;
                int newCol = col + j;
                if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize)
                {
                    if (matrix[newRow, newCol] != Player.EMPTY)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public Vector2Int GetBestMove()
    {
        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);

        List<Vector2Int> currentEmptyCells = new List<Vector2Int>(emptyCells);

        foreach (Vector2Int cell in currentEmptyCells)
        {
            int row = cell.x;
            int col = cell.y;

            matrix[row, col] = Player.NOUGHT;
            emptyCells.Remove(cell);

            int score = Minimax(matrix, false, 1, row, col, int.MinValue, int.MaxValue);

            matrix[row, col] = Player.EMPTY;
            emptyCells.Add(cell);

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = new Vector2Int(row, col);
            }
        }

        return bestMove;
    }

    private int Minimax(Player[,] matrix, bool isMaximizing, int depth, int lastMoveRow, int lastMoveCol, int alpha, int beta) // lastMoveRow và laseMoveCol là row và col của ô đã đánh trước đó
    {
        Player previousTurn = isMaximizing ? Player.CROSS : Player.NOUGHT;
        Player currentTurn = isMaximizing ? Player.NOUGHT : Player.CROSS;

        // tính điểm cho nước đi thực hiện trước đó

        if (GameLogic.CheckWin(matrix, lastMoveRow, lastMoveCol, previousTurn, winCondition))
        {
            return isMaximizing ? -100000 : 100000;
        }

        if (GameLogic.IsBoardFull(matrix))
        {
            return 0;
        }

        if (depth == maxDepth) // đạt độ sâu tối đa cho phép thì tính điểm board tại trạng thái đó
        {
            return EvaluateBoard(matrix, isMaximizing);
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue; // khởi tạo bestscore với giá trị thấp nhất hoặc lớn nhất của kiểu dữ liệu int

        List<Vector2Int> currentEmptyCells = new List<Vector2Int>(emptyCells);

        foreach (Vector2Int cell in currentEmptyCells) // duyệt các ô còn trống hiện tại
        {
            int row = cell.x;
            int col = cell.y;

            matrix[row, col] = currentTurn; // giả định đánh vào ô này rồi thực hiện đệ quy trả về score
            emptyCells.Remove(cell); // đồng thời giả định bỏ ô này trong danh sách các ô trống

            int score = Minimax(matrix, !isMaximizing, depth + 1, row, col, alpha, beta);

            matrix[row, col] = Player.EMPTY; // hoàn trả ô giả định
            emptyCells.Add(cell); // hoàn trả ô giả định

            if (isMaximizing)
            {
                bestScore = Mathf.Max(score, bestScore);
                alpha = Mathf.Max(alpha, bestScore);
            }
            else
            {
                bestScore = Mathf.Min(score, bestScore);
                beta = Mathf.Min(beta, bestScore);
            }

            if (alpha >= beta) break; // cắt tỉa alpha-beta
        }
        return bestScore;
    }

    private int EvaluateBoard(Player[,] matrix, bool isMaximizing)
    {
        Player player = isMaximizing ? Player.NOUGHT : Player.CROSS;
        Player opponent = isMaximizing ? Player.CROSS : Player.NOUGHT;
        int score = 0;

        // Đánh giá các hàng, cột, chéo
        for (int i = 0; i < boardSize; i++)
        {
            score += EvaluateLine(matrix, i, 0, 0, 1, player, opponent); // Hàng
            score += EvaluateLine(matrix, 0, i, 1, 0, player, opponent); // Cột
        }
        // Chéo chính và chéo phụ
        for (int i = -boardSize + 1; i < boardSize; i++)
        {
            score += EvaluateDiagonal(matrix, i, true, player, opponent);  // Chéo chính
            score += EvaluateDiagonal(matrix, i, false, player, opponent); // Chéo phụ
        }

        return score;
    }

    private int EvaluateLine(Player[,] matrix, int startRow, int startCol, int dRow, int dCol, Player player, Player opponent)
    {
        int score = 0;
        int playerCount = 0;
        int opponentCount = 0;
        bool isOpenStart = false;

        for (int i = 0; i < boardSize; i++)
        {
            int row = startRow + i * dRow;
            int col = startCol + i * dCol;
            if (row < 0 || row >= boardSize || col < 0 || col >= boardSize)
                break;

            if (matrix[row, col] == player)
            {
                if (opponentCount > 0)
                {
                    score -= ScorePattern(opponentCount, isOpenStart, false);
                    isOpenStart = false;
                    opponentCount = 0;
                }
                playerCount++;
            }
            else if (matrix[row, col] == opponent)
            {
                if (playerCount > 0)
                {
                    score += ScorePattern(playerCount, isOpenStart, false);
                    isOpenStart = false;
                    playerCount = 0;
                }
                opponentCount++;
            }
            else
            {
                if (playerCount > 0)
                {
                    score += ScorePattern(playerCount, isOpenStart, true);
                    isOpenStart = true;
                    playerCount = 0;
                }
                else if (opponentCount > 0)
                {
                    score -= ScorePattern(opponentCount, isOpenStart, true);
                    isOpenStart = true;
                    opponentCount = 0;
                }
            }
        }

        if (playerCount > 0)
            score += ScorePattern(playerCount, isOpenStart, false);
        if (opponentCount > 0)
            score -= ScorePattern(opponentCount, isOpenStart, false);

        return score;
    }

    private int EvaluateDiagonal(Player[,] matrix, int offset, bool isMainDiagonal, Player player, Player opponent)
    {
        int score = 0;
        int startRow = offset >= 0 ? offset : 0;
        int startCol = offset >= 0 ? 0 : -offset;
        int count = 0;
        int playerCount = 0;
        int opponentCount = 0;
        bool isOpenStart = false;

        while (startRow < boardSize && startCol < boardSize)
        {
            int row = startRow;
            int col = isMainDiagonal ? startCol : boardSize - 1 - startRow + offset;
            if (row >= boardSize || col >= boardSize || col < 0)
                break;

            if (matrix[row, col] == player)
            {
                if (opponentCount > 0)
                {
                    score -= ScorePattern(opponentCount, isOpenStart, false);
                    isOpenStart = opponentCount == 0;
                    opponentCount = 0;
                }
                playerCount++;
            }
            else if (matrix[row, col] == opponent)
            {
                if (playerCount > 0)
                {
                    score += ScorePattern(playerCount, isOpenStart, false);
                    isOpenStart = playerCount == 0;
                    playerCount = 0;
                }
                opponentCount++;
            }
            else
            {
                if (playerCount > 0)
                {
                    score += ScorePattern(playerCount, isOpenStart, true);
                    isOpenStart = true;
                    playerCount = 0;
                }
                else if (opponentCount > 0)
                {
                    score -= ScorePattern(opponentCount, isOpenStart, true);
                    isOpenStart = true;
                    opponentCount = 0;
                }
            }

            startRow++;
            startCol++;
            count++;
        }

        if (playerCount > 0)
            score += ScorePattern(playerCount, isOpenStart, false);
        if (opponentCount > 0)
            score -= ScorePattern(opponentCount, isOpenStart, false);

        return score;
    }

    private int ScorePattern(int count, bool isOpenStart, bool isOpenEnd)
    {
        if (count == 4)
            return winCondition <= 4 ? 10000 : (isOpenStart && isOpenEnd ? 10000 : (isOpenStart || isOpenEnd ? 5000 : 0));
        if (count == 3)
            return winCondition == 3 ? 1000: (isOpenStart && isOpenEnd ? 1000 : (isOpenStart || isOpenEnd ? 500 : 0));
        if (count == 2)
            return isOpenStart && isOpenEnd ? 100 : (isOpenStart || isOpenEnd ? 50 : 0);
        if (count == 1)
            return isOpenStart && isOpenEnd ? 10 : (isOpenStart || isOpenEnd ? 5 : 0);
        return 0;
    }
}