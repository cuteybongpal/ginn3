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

    public void UpGrade(int ProductNum)
    {

    }

}
