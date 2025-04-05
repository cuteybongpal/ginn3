using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ad : UI_Popup
{
    public GameObject InventoryItemInfo;
    public Transform AdTransform;
    public Button Close;
    public Text TotalMoney;
    void Start()
    {
        Init();
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
        Close.gameObject.SetActive(false);
        StartCoroutine(AD());
    }
    IEnumerator AD()
    {
        int money = 0;
        foreach (IStorable storable in GameManager.Instance.Inventory)
        {
            if (storable is Item)
                continue;
            yield return new WaitForSeconds(.2f);
            Treasure t = storable as Treasure;
            UI_InventoryTreasureInfo ui =  Instantiate(InventoryItemInfo).GetComponent<UI_InventoryTreasureInfo>();
            ui.Init(t);
            ui.transform.parent = AdTransform;
            money += t.Worth;
        }
        yield return new WaitForSeconds(.5f);

        float sum = 0;
        for (int i = 0; i < 100; i++)
        {
            sum += (float)money / 100f;
            TotalMoney.text = $"Total : {sum}";
            yield return null;
        }
        GameManager.Instance.RemoveAllInventoryItem();
        GameManager.Instance.CurrentMoney += money;
        Close.gameObject.SetActive(true);
    }
}
