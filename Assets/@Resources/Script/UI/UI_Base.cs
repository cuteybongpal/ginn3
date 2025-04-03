using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    public virtual void Init()
    {
        UIManager.Instance.CurrentMainUI = this;
    }
}
