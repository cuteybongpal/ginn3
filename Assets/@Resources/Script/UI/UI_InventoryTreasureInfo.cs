using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryTreasureInfo : MonoBehaviour
{
    public Text Worth;
    public Image TreasureImage;
    public void Init(Treasure t)
    {
        Worth.text = $"+{t.Worth}";
        TreasureImage.sprite = t.Sprite;
    }
}
