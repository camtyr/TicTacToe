using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite xSprite, oSprite;
    private Image image;
    private Button button;
    private Board board;
    private GameOver gameOver;
    public int row, col;

    public void Initialize(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

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

    public void OnClick()
    {
        board.MakeMove(row, col);
    }

    public void ChangeImage(Player player)
    {
        if (player == Player.CROSS)
        {
            image.sprite = xSprite;
        }
        else if (player == Player.NOUGHT)
        {
            image.sprite = oSprite;
        }
    }
}