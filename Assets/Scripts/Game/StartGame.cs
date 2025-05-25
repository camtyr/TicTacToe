using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static string gameMode;
    public static string playerName1, playerName2;

    private void Update()
    {
        playerName1 = "Player 1";
        playerName2 = gameMode == "vsbot" ? "bot" : "Player 2";
    }

    public void SelectedMode(string mode)
    {
        gameMode = mode;
        SceneManager.LoadScene("GameScene");
    }
}