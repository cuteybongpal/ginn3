using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string MoveText;
    public Define.Scenes DestScene;
    public GameObject UI_SceneMoveConfirm;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        UI_SceneConfirm ui = Instantiate(UI_SceneMoveConfirm).GetComponent<UI_SceneConfirm>();
        ui.Initialize(MoveText, DestScene);
    }
}