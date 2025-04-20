using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public TMP_InputField playerInput1, playerInput2;
    public static string playerName1, playerName2;

    private void Update()
    {
        playerName1 = playerInput1.text;
        playerName2 = playerInput2.text;
    }

    public void PlayGame()
    {
        if(playerName1 != playerName2)
        {
            SceneManager.LoadScene("SceneGame");
        }
    }
}
