using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    private static ProductManager instance;
    public static ProductManager Instance {  get { return instance; } }

    public string[] ProductNames;
    public int[] Lvs;
    public Sprite[] ProductSprites;
    public int[] MaxLvs;
    public int[] Prices;
    public GameObject UI_Warning;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool UpGrade(int productNum)
    {
        if (GameManager.Instance.CurrentMoney < Prices[productNum])
        {
            Instantiate(UI_Warning).GetComponent<UI_Warning>().Init("돈이 부족해 업그레이드가 불가능합니다.");
            return false;
        }
        GameManager.Instance.CurrentMoney -= Prices[productNum];
        switch(productNum)
        {
            case 0:
                GameManager.Instance.PlayerMaxO2 += 50;
                Prices[productNum] += 200;
                break;
            case 1:
                GameManager.Instance.MaxStroableItemCount += 2;
                GameManager.Instance.MaxStroableItemWeight += 50;
                Prices[productNum] += 200;
                break;
            case 2:
                GameManager.Instance.PlayerAttack++;
                Prices[productNum] += 300;
                break;
        }
        if (Prices[productNum] <= 500)
            Prices[productNum] = 0;
        return true;
    }

}
