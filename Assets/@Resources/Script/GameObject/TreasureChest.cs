using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    Treasure treasure;
    public GameObject UI_TreasureInfo;
    UI_TreasureInfo ui;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        treasure = GetComponent<Treasure>();
        if (GameManager.Instance.TreasureIsFind[(int)GameManager.Instance.CurrentScene - 2][treasure.TreasureNum])
            treasure = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (treasure == null)
            return;
        if (ui != null)
            return;
        ui = Instantiate(UI_TreasureInfo).GetComponent<UI_TreasureInfo>();
        ui.Initialize(treasure, this);
        Open();

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (treasure == null)
            return;
        if (ui == null)
            return;
        ui.Hide();
        Close();
    }
    void Open()
    {
        anim.Play("Open");
    }
    void Close()
    {
        anim.Play("Close");
    }
}
