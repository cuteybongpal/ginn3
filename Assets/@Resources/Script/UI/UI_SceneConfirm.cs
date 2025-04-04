using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SceneConfirm : UI_Popup
{
    public Text Message;
    public Button Move;
    public Button Close;
    public void Initialized(string message, Define.Scenes scenes)
    {
        Message.text = message;
        Move.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentScene = scenes;
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
}
