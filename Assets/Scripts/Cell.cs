using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite xSprite, oSprite;
    private Image image;
    private Button button;
    private Board board;
    public int row, col;
    private GameOver gameOver;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        board = FindFirstObjectByType<Board>();
        gameOver = FindFirstObjectByType<GameOver>();
    }

    private void OnClick()
    {
        if (image.sprite != xSprite && image.sprite != oSprite)
        {
            ChangeImage(board.currentTurn);

            if (board.checkWin(row, col))
            {
                gameOver.gameOver.SetActive(true);
                gameOver.SetName(board.currentTurn);
            };

            if (board.currentTurn == "X")
            {
                board.currentTurn = "O";
                board.playerTurn.text = "Turn: \n" + StartGame.playerName2;
            }
            else
            {
                board.currentTurn = "X";
                board.playerTurn.text = "Turn: \n" + StartGame.playerName1;
            }
        }
    }

    public void ChangeImage(string mark)
    {
        if (mark == "X")
        {
            image.sprite = xSprite;
        }
        else if (mark == "O")
        {
            image.sprite = oSprite;
        }
    }
}