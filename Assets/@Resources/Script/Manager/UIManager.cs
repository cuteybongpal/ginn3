using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

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
    UI_Base currentMainUI;
    Stack<UI_Popup> ui_popup = new Stack<UI_Popup>();
    public UI_Base CurrentMainUI { set { currentMainUI = value; } }
    public T GetCurrentMainUI<T>() where T : UI_Base
    {
        return currentMainUI as T;
    }
    public void Push(UI_Popup popup)
    {
        ui_popup.Push(popup);
    }
    public void Pop()
    {
        UI_Popup popup = ui_popup.Pop();

        Destroy(popup.gameObject);
    }

}
