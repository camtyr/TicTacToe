using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverImage;
    public TextMeshProUGUI winnerText;

    public void SetWin(Player player)
    {
        if(player == Player.CROSS) winnerText.text = StartGame.playerName1 + " Wins!";
        else winnerText.text = StartGame.playerName2 + " Wins!";
    }

    public void SetDraw()
    {
        winnerText.text = "It's a Draw!";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}