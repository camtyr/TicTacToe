using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform board;
    public GridLayoutGroup gridLayout;
    public int boardSize;
    public string currentTurn = "X";
    private string[,] matrix;
    public TextMeshProUGUI playerTurn;

    void Start()
    {
        matrix = new string[boardSize, boardSize];
        gridLayout.constraintCount = boardSize;
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                Cell cell = Instantiate(cellPrefab, board).GetComponent<Cell>();
                cell.row = i;
                cell.col = j;
                matrix[i, j] = "";
            }
        }
    }

    public bool checkWin(int row, int col)
    {
        matrix[row, col] = currentTurn;

        // Vertical
        int count = 1;

        for (int i = row - 1; i >= 0; i--)
        {
            if (matrix[i, col] == currentTurn) count++;
            else break;
        }
        for (int i = row + 1; i < boardSize; i++)
        {
            if (matrix[i, col] == currentTurn) count++;
            else break;
        }
        if (count > 4) return true;

        // Horizontal
        count = 1;

        for (int i = col - 1; i >= 0; i--)
        {
            if (matrix[row, i] == currentTurn) count++;
            else break;
        }
        for (int i = col + 1; i < boardSize; i++)
        {
            if (matrix[row, i] == currentTurn) count++;
            else break;
        }
        if (count > 4) return true;

        // Left diagonal
        count = 1;

        for (int i = 1; row - i >= 0 && col - i >= 0; i++)
        {
            if (matrix[row - i, col - i] == currentTurn) count++;
            else break;
        }
        for (int i = 1; row + i < boardSize && col + i < boardSize; i++)
        {
            if (matrix[row + 1, col + i] == currentTurn) count++;
            else break;
        }
        if (count > 4) return true;

        // Right diagonal
        count = 1;

        for (int i = 1; row - i >= 0 && col + i < boardSize; i++)
        {
            if (matrix[row - i, col + i] == currentTurn) count++;
            else break;
        }
        for (int i = 1; row + i < boardSize && col - i >= 0; i++)
        {
            if (matrix[row + i, col - i] == currentTurn) count++;
            else break;
        }
        if (count > 4) return true;

        return false;
    }
}
