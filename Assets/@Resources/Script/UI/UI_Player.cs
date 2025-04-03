using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : UI_Base
{
    public GameObject[] Hps;
    public Text WeightText;
    public Text O2Text;
    public Slider O2Slider;
    public Transform InventoryTransform;

    public GameObject UI_InventoryItem;
    UI_InventoryItem[] uI_InventoryItems;
    
    void Start()
    {
        Init();
        O2Slider.maxValue = GameManager.Instance.PlayerMaxO2;
        uI_InventoryItems = new UI_InventoryItem[GameManager.Instance.MaxStroableItemCount];
        for (int i = 0; i < GameManager.Instance.MaxStroableItemCount; i++)
        {
            UI_InventoryItem item = Instantiate(UI_InventoryItem).GetComponent<UI_InventoryItem>();
            item.transform.parent = InventoryTransform;
            uI_InventoryItems[i] = item;
            item.Init(null);
        }
        RefreshInventory();
    }
    public void SetO2(int value)
    {
        O2Slider.value = value;
        O2Text.text = $"남은 산소량 : {value}";
    }
    public void SetHP(int value)
    {
        for (int i = 0; i < Hps.Length; i++)
        {
            Hps[i].SetActive(true);
        }

        for (int i = value; i < Hps.Length; i++)
        {
            Hps[i].SetActive(false);
        }
    }
    public void RefreshInventory()
    {
        int weight = 0;
        foreach(var item in uI_InventoryItems)
        {
            item.Init(null);
        }
        for (int i =0; i < GameManager.Instance.Inventory.Count; i++)
        {
            uI_InventoryItems[i].Init(GameManager.Instance.Inventory[i].GetSprite);
            weight += GameManager.Instance.Inventory[i].GetWeight;
        }
        WeightText.text = $"{weight} / {GameManager.Instance.MaxStroableItemWeight} kg";
    }
}
