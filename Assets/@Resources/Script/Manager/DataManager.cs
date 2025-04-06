using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
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
    public List<int> Ranking = new List<int>();
    public bool[] PuzzleSolve = new bool[5];
    public void Add(int score)
    {
        Ranking.Add(score);

        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));


#if UNITY_EDITOR
        using (StreamWriter sw = new StreamWriter("Assets/@Resources/Data/ranking.xml"))
        {
            serializer.Serialize(sw, Ranking);
        }
        return;
#endif
        using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath+"/Data/ranking.xml"))
        {
            serializer.Serialize(sw, Ranking);
        }
    }
    public void Init()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));

#if UNITY_EDITOR

        try
        {
            using (StreamReader sw = new StreamReader("Assets/@Resources/Data/ranking.xml"))
            {
                Ranking = (List<int>)serializer.Deserialize(sw);
            }
        }
        catch (Exception e)
        {
            Ranking = new List<int>();
        }
        return;
#endif
        try
        {
            using (StreamReader sw = new StreamReader(Application.streamingAssetsPath + "/Data/ranking.xml"))
            {
                Ranking = (List<int>)serializer.Deserialize(sw);
            }
        }
        catch (Exception e)
        {
            Ranking = new List<int>();
        }
    }
}
