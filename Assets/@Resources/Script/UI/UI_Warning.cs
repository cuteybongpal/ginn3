using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : MonoBehaviour
{
    Text warningText;
    public void Init(string message)
    {
        warningText = GetComponentInChildren<Text>();
        warningText.text = message;
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        float elapsedTime = 0;
        float duration = 1f;
        Color originColor = warningText.color;
        originColor.a = 0;
        Color destColor = originColor;
        while (elapsedTime < duration)
        {
            Color color = Color.Lerp(originColor, destColor, elapsedTime / duration);
            warningText.color = color;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
