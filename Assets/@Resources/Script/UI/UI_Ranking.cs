using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ranking : UI_Popup
{
    public GameObject RankInfo;
    public Transform RankingTrasnform;
    public Button Close;
    void Start()
    {
        Init();
        int rank = 0;
        foreach(int item in DataManager.Instance.Ranking)
        {
            UI_RankingInfo ui = Instantiate(RankInfo).GetComponent<UI_RankingInfo>();
            ui.Init(rank, item);
            ui.transform.parent = RankingTrasnform;
            rank++;
        }
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }

}
