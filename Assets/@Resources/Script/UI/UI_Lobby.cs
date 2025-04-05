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
    public GameObject UI_Ad;
    void Start()
    {
        Init();
        if (GameManager.Instance.Inventory.Count > 0)
        {
            Instantiate(UI_Ad);
        }
        SetCoin(GameManager.Instance.CurrentMoney);
        Shop.onClick.AddListener(() =>
        {
            Instantiate(UI_Shop);
        });
        ToMenu.onClick.AddListener(() =>
        {
            Instantiate(UI_SceneMoveConfirm).GetComponent<UI_SceneConfirm>().Initialize("메뉴로 이동하시겠습니까?", Define.Scenes.StartScene);
        });
    }
    public void SetCoin(int value)
    {
        CoinText.text = $"{value} 원";
    }
}
