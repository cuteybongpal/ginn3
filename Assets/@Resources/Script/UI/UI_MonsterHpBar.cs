using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonsterHpBar : MonoBehaviour
{
    Slider hpSlider;
    public void Init(int maxValue)
    {
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = maxValue;
    }
    public void SetHp(int value)
    {
        hpSlider.value = value;
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
