using UnityEngine;

public class Main : MonoBehaviour
{
    public Board board;

    private bool xTurn = true;
    private int turnCount = 0;

    void Awake()
    {
        board.Build(this);
    }

    public void Switch()
    {
        turnCount++;
/*
        if (board.CheckForWinner()) print("winnter!");*/

        xTurn = !xTurn;
    }

    public string GetTurnCharacter()
    {
        if (xTurn) return "X";
        else return "O";
    }
}
