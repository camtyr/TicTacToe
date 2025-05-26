using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverImage;
    public TextMeshProUGUI winnerText;

    public void SetWin(Player player)
    {
        if(player == Player.CROSS) winnerText.text = GameConfig.player1Name + " Wins!";
        else winnerText.text = GameConfig.player2Name + " Wins!";
    }

    public void SetDraw()
    {
        winnerText.text = "It's a Draw!";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartGameScene");
    }
}