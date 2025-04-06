using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MazeCell : MonoBehaviour
{
    Button button;
    Maze maze;

    public enum BlockType
    {
        None,
        Blocked,
        Player,
        Goal,
        Path
    }
    public BlockType Type = BlockType.None;
    public void Init(Maze maze)
    {
        this.maze = maze;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (Type == BlockType.None || Type == BlockType.Goal)
            {
                maze.AddPath(this);
            }
            else if (Type == BlockType.Path)
            {
                maze.RemovePath(this);
            }
        });
    }
}
