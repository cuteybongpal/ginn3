using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject Puzzle;
    Maze maze;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (maze != null)
            return;
        if (DataManager.Instance.PuzzleSolve[(int)GameManager.Instance.CurrentScene - 2])
            return;
        maze = Instantiate(Puzzle).GetComponent<Maze>();
        maze.Initialize(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (maze == null)
            return;
        if (DataManager.Instance.PuzzleSolve[(int)GameManager.Instance.CurrentScene - 2])
            return;
        Destroy(maze.gameObject);
    }
    public void Solve()
    {
        DataManager.Instance.PuzzleSolve[(int)GameManager.Instance.CurrentScene - 2] = true;
    }
}
