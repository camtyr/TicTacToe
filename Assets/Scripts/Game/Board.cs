using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform board;
    private GridLayoutGroup gridLayoutGroup;
    public TextMeshProUGUI playerTurn;
    private GameOver gameOver;

    public string gameMode;
    public int boardSize;
    public Player currentTurn;
    public Player[,] matrix;
    public bool isGameOver = false;

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = boardSize;

        gameMode = StartGame.gameMode;
        currentTurn = Player.CROSS;
        matrix = new Player[boardSize, boardSize];
    }

    private void Start()
    {
        gameOver = FindFirstObjectByType<GameOver>();

        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Instantiate(cellPrefab, board).GetComponent<Cell>().Initialize(i, j);
            }
        }
    }

    public void MakeMove(int row, int col)
    {
        if (matrix[row, col] == Player.EMPTY && !isGameOver)
        {
            matrix[row, col] = currentTurn;
            board.GetChild(row * boardSize + col).GetComponent<Cell>().ChangeImage(currentTurn);

            if (GameLogic.CheckWin(matrix, row, col, currentTurn))
            {
                isGameOver = true;
                gameOver.gameOverImage.SetActive(true);
                gameOver.SetWin(currentTurn);
            }
            else if (GameLogic.IsBoardFull(matrix))
            {
                isGameOver = true;
                gameOver.gameOverImage.SetActive(true);
                gameOver.SetDraw();
            }
            else
            {
                currentTurn = (currentTurn == Player.CROSS) ? Player.NOUGHT : Player.CROSS;
                UpdatePlayerTurn();
                if (currentTurn == Player.NOUGHT && gameMode == "vsbot")
                {
                    StartCoroutine(AIPlay());
                }
            }
        }
    }

    private void UpdatePlayerTurn()
    {
        playerTurn.text = $"{(currentTurn == Player.CROSS ? StartGame.playerName1 : StartGame.playerName2)}'s turn";
    }

    private IEnumerator AIPlay()
    {
        yield return new WaitForSeconds(0.5f);
        TicTacToeAI ai = new TicTacToeAI(matrix);
        Vector2Int bestMove = ai.GetBestMove();
        MakeMove(bestMove.x, bestMove.y);
    }

    private bool IsBoardFull()
    {
        foreach (var item in matrix)
        {
            if (item == Player.EMPTY)
                return false;
        }
        return true;
    }
}