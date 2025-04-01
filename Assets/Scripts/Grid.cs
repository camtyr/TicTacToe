using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject cellPrefab;

    private Cell[] cells = new Cell[9];

    void Start()
    {
        Build();
    }

    public void Build()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            GameObject newCell = Instantiate(cellPrefab, transform);

            cells[i] = newCell.GetComponent<Cell>();
        }
    }
}
