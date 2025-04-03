using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        UIManager.Instance.Push(this);
    }

    public void Hide()
    {
        UIManager.Instance.Pop();
    }
}
