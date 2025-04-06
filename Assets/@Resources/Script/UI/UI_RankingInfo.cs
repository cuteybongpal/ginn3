using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankingInfo : MonoBehaviour
{
    public Text Number;
    public Text Score;
    public void Init(int number, int score)
    {
        Number.text = $"#{number + 1}";
        Score.text = $"{score} ��";
    }
}
