using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    public Transform ShopTransform;
    public GameObject ProductInfo;
    UI_ProductInfo[] ui_ProductInfos;
    public Button Close;
    void Start()
    {
        Init();
        ui_ProductInfos = new UI_ProductInfo[3];
        for (int i = 0; i < ProductManager.Instance.ProductNames.Length; i++)
        {
            UI_ProductInfo ui = Instantiate(ProductInfo).GetComponent<UI_ProductInfo>();
            ui.transform.parent = ShopTransform;
            ui_ProductInfos[i] = ui;
            ui.Init(i);
        }
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
}
