using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    static DataManager instance = new DataManager();
    public static DataManager Instance { get { return instance; } }
    public static List<bool>[] IsTreasureFind { get { return instance.isTreasureFind; } set { IsTreasureFind = value; } }
    public List<bool>[] isTreasureFind = new List<bool>[5]
    {
        new List<bool>()
        {
            false,
            false,
            false,
            false,
        },
        new List<bool>()
        {
            false,
            false,
            false,
            false,
        },
        new List<bool>()
        {
            false,
            false,
        },
        new List<bool>()
        {
            false,
            false,
        },
        new List<bool>(),
    };
}
