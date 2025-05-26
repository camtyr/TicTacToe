using Assets.Scripts.Utils;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject BoardsizeSelectionImage;

    public void OnSelectMode(string mode)
    {
        GameConfig.gameMode = mode;

        if (GameConfig.gameMode == "pvb")
        {
            GameConfig.player1Name = "Player";
            GameConfig.player2Name = "Bot";
        }
        else
        {
            GameConfig.player1Name = "Player 1";
            GameConfig.player2Name = "Player 2";
        }

        BoardsizeSelectionImage.SetActive(true);
    }
    public void OnSelectBoardsize(int size)
    {
        GameConfig.boardSize = size;
        SceneManager.LoadScene("GameScene");
    }

    public void CloseBoardsizeSelectionImage()
    {
        BoardsizeSelectionImage.SetActive(false);
    }
}