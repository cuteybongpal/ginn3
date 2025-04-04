using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
    public Text CoinText;
    public Button Shop;
    public Button ToMenu;

    public GameObject UI_SceneMoveConfirm;
    public GameObject UI_Shop;
    void Start()
    {
        Init();
        SetCoin(GameManager.Instance.CurrentMoney);
    }
    public void SetCoin(int value)
    {
        CoinText.text = $"{value} ¿ø";
    }
}
