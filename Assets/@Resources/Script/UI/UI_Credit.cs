using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Credit : UI_Base
{
    public Text ScoreText;
    private void Start()
    {
        Init();
        ScoreText.text = $"����� ���� : {GameManager.Instance.Score}";
        DataManager.Instance.Add(GameManager.Instance.Score);
        StartCoroutine(CreditDown());
    }
    IEnumerator CreditDown()
    {
        float elapsedTime = 0;
        float duration = 5;

        Vector3 origin = transform.position;
        Vector3 destPos = origin + Vector3.up * 500;
        while (elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, destPos, elapsedTime / duration);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        while (!Input.GetKeyDown(KeyCode.Mouse0))
            ;
        GameManager.Instance.CurrentScene = Define.Scenes.StartScene;
    }

}
