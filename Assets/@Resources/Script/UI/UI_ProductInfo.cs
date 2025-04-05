using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProductInfo : MonoBehaviour
{
    public Button Buy;
    public Text Name;
    public Text Lv;
    public Image ProductSprite;
    int productNum;
    public void Init(int productNum)
    {
        this.productNum = productNum;
        Refresh();
    }
    public void Refresh()
    {
        Name.text = ProductManager.Instance.ProductNames[productNum];
        Lv.text = $"Lv. {ProductManager.Instance.Lvs[productNum]}";
        ProductSprite.sprite = ProductManager.Instance.ProductSprites[productNum];
        Buy.onClick.RemoveAllListeners();
        Buy.GetComponentInChildren<Text>().text = $"{ProductManager.Instance.Prices[productNum]}";
        Buy.onClick.AddListener(() =>
        {
            ProductManager.Instance.UpGrade(productNum);
            Refresh();
        });
    }
}
