using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryItem : MonoBehaviour
{
    public Image image;
    public void Init(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
