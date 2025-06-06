using UnityEngine;

public static class GameLogic
{
    public static bool CheckWin(Player[,] matrix, int row, int col, Player currentTurn, int winCondition)
    {
        return CheckDirection(matrix, row, col, 1, 0, currentTurn, winCondition) ||
               CheckDirection(matrix, row, col, 0, 1, currentTurn, winCondition) ||
               CheckDirection(matrix, row, col, 1, 1, currentTurn, winCondition) ||
               CheckDirection(matrix, row, col, 1, -1, currentTurn, winCondition);
    }

    public static bool CheckDirection(Player[,] matrix, int row, int col, int countRow, int countCol, Player currentTurn, int winCondition)
    {
        int count = 1;
        int boardSize = (int)Mathf.Sqrt(matrix.Length);
        for (int i = 1; i < winCondition; i++)
        {
            int currentRow = row + i * countRow, currentCol = col + i * countCol;
            if (currentRow < 0 || currentRow >= boardSize || currentCol < 0 || currentCol >= boardSize) break;
            if(matrix[currentRow, currentCol] != currentTurn) break;
            count++;
        }

        for (int i = 1; i < winCondition; i++)
        {
            int currentRow = row - i * countRow, currentCol = col - i * countCol;
            if (currentRow < 0 || currentRow >= boardSize || currentCol < 0 || currentCol >= boardSize) break;
            if (matrix[currentRow, currentCol] != currentTurn) break;
            count++;
        }

        return count >= winCondition;
    }

    public static bool IsBoardFull(Player[,] matrix)
    {
        foreach (var cell in matrix)
        {
            if (cell == Player.EMPTY)
                return false;
        }
        return true;
    }
}