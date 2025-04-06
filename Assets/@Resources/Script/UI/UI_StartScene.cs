using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartScene : UI_Base
{
    public Button StartGame;
    public Button Exit;
    public Button Ranking;

    public GameObject Rank;
    void Start()
    {
        DataManager.Instance.Init();
        StartGame.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentScene = Define.Scenes.Lobby;
        });
        Ranking.onClick.AddListener(() =>
        {
            Instantiate(Rank);
        });
        Exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
