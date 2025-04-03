using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TreasureInfo : UI_Popup
{
    public Image image;
    public Text Name;
    public Text Weight;
    public Text Worth;

    public Button Take;
    public Button Close;
    public void Initialize(Treasure t, TreasureChest chest)
    {
        Init();
        Name.text = t.name;
        image.sprite = t.Sprite;
        Weight.text = $"{t.GetWeight} kg";
        Worth.text = $"{t.Worth}¿ø";

        Take.onClick.AddListener(() =>
        {
            t.Store();
            Hide();
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }

}
