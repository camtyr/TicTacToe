using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI winnerText;

    public void SetName(string s)
    {
        if(s == "X") winnerText.text = StartGame.playerName1 + " Wins!";
        else winnerText.text = StartGame.playerName2 + " Wins!";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}