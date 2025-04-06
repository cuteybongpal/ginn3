using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Maze : UI_Popup
{
    Cage cage;
    UI_MazeCell[,] cells = new UI_MazeCell[10,10];
    public Transform MazeTransform;
    List<UI_MazeCell> path = new List<UI_MazeCell>();
    public Text ClearText;
    public Button MoveButton;
    public Button Close;
    private void Start()
    {
        
    }
    public void Initialize(Cage cage)
    {
        ClearText.gameObject.SetActive(false);
        this.cage = cage;
        Init();
        for (int y = 0; y < cells.GetLength(0); y++)
        {
            for (int x = 0;  x < cells.GetLength(1); x++)
            {
                cells[y,x] = MazeTransform.GetChild(y * 10 + x).GetComponent<UI_MazeCell>();
                cells[y, x].Init(this);
            }
        }
        MoveButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlayerMove());
            MoveButton.onClick.RemoveAllListeners();
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void AddPath(UI_MazeCell cell)
    {
        int cellY = 0;
        int cellX = 0;
        for (int y = 0; y < cells.GetLength(0); y++)
        {
            for (int x = 0; x < cells.GetLength(1); x++)
            {
                if (cells[y,x] == cell)
                {
                    cellY = y;
                    cellX = x;
                    break;
                }
            }
        }

        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        if (path.Count == 0)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                if (cellX + dirs[i].x < 0 && cellX + dirs[i].x >= 10)
                    continue;
                if (cellY + dirs[i].y < 0 && cellY + dirs[i].y >= 10)
                    continue;

                if (cells[cellY + dirs[i].y, cellX + dirs[i].x].Type == UI_MazeCell.BlockType.Player)
                {
                    path.Add(cell);
                    if (cell.Type == UI_MazeCell.BlockType.None)
                    {
                        cell.Type = UI_MazeCell.BlockType.Path;
                        cell.GetComponent<Image>().color = new Color(0.2f,0.2f,1);
                    }    
                }
            }
        }
        else
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                if (cellX + dirs[i].x < 0 && cellX + dirs[i].x >= 10)
                    continue;
                if (cellY + dirs[i].y < 0 && cellY + dirs[i].y >= 10)
                    continue;

                if (cells[cellY + dirs[i].y, cellX + dirs[i].x] == path[^1])
                {
                    path.Add(cell);
                    if (cell.Type == UI_MazeCell.BlockType.None)
                    {
                        cell.Type = UI_MazeCell.BlockType.Path;
                        cell.GetComponent<Image>().color = new Color(0.2f, 0.2f, 1);
                    }
                }
            }

        }
    }
    public void RemovePath(UI_MazeCell cell)
    {
        int index = path.IndexOf(cell);
        if (index == -1)
            return;

        for (int i = path.Count - 1; i >= 0; i--)
        {
            UI_MazeCell cell2 = path[i];
            path.RemoveAt(i);
            if (cell2.Type != UI_MazeCell.BlockType.Goal)
            {
                cell2.Type = UI_MazeCell.BlockType.None;
                cell2.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }
    }
    IEnumerator PlayerMove()
    {
        cells[1, 1].GetComponent<Image>().color = Color.white;
        cells[1, 1].Type = UI_MazeCell.BlockType.None;
        yield return new WaitForSeconds(.1f);
        UI_MazeCell prevCell = null;
        foreach (UI_MazeCell cell in path)
        {
            cell.GetComponent <Image>().color = Color.blue;
            if (prevCell != null)
            {
                prevCell.GetComponent<Image>().color = Color.white;
                prevCell.Type = UI_MazeCell.BlockType.None;
            }
            if (cell.Type == UI_MazeCell.BlockType.Goal)
            {
                ClearText.gameObject.SetActive(true);
                cage.Solve();
                yield return new WaitForSeconds(.5f);
                Hide();
            }
            prevCell = cell;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.2f);

        Hide();
    }
    
}
