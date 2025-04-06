using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Credit : UI_Base
{
    public Text ScoreText;
    public GameObject Image;
    private void Start()
    {
        Init();
        ScoreText.text = $"당신의 점수 : {GameManager.Instance.Score}";
        DataManager.Instance.Add(GameManager.Instance.Score);
        StartCoroutine(CreditDown());
    }
    IEnumerator CreditDown()
    {
        float elapsedTime = 0;
        float duration = 5;

        Vector3 origin = Image.gameObject.transform.position;
        Vector3 destPos = origin + Vector3.up * 1000;
        while (elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, destPos, elapsedTime / duration);
            Image.gameObject.transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        while (!Input.GetKeyDown(KeyCode.Mouse0))
            yield return null;
        GameManager.Instance.CurrentScene = Define.Scenes.StartScene;
    }

}
