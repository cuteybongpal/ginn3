using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public float PlayerSpeed;
    public float PlayerOriginSpeed;
    public int PlayerMaxHp;
    public int PlayerMaxO2;
    int playerCurrentHp;
    int playerCurrentO2;

    public List<IStorable> Inventory = new List<IStorable>();
    public int MaxStroableItemCount = 4;
    public int MaxStroableItemWeight = 200;
    public int PlayerCurrentHp
    {
        get { return playerCurrentHp; }
        set
        {
            playerCurrentHp = value;
            if (playerCurrentHp >  PlayerMaxHp)
                playerCurrentHp = PlayerMaxHp;
            UI_Player ui = UIManager.Instance.GetCurrentMainUI<UI_Player>();
            if (ui == null)
                return;
            ui.SetHP(playerCurrentHp);
            if (playerCurrentHp <= 0)
            {
                playerCurrentHp = 0;
            }
        }
    }
    public int PlayerCurrentO2
    {
        get { return playerCurrentO2; }
        set
        {
            playerCurrentO2 = value;
            UI_Player ui = UIManager.Instance.GetCurrentMainUI<UI_Player>();
            if (ui == null)
                return;
            
            if (playerCurrentO2 < 0)
            {
                PlayerCurrentO2 = 0;
                PlayerCurrentHp--;
            }
            else if (playerCurrentO2 > PlayerMaxO2)
                PlayerCurrentO2 = PlayerMaxO2;
            ui.SetO2(playerCurrentO2);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerOriginSpeed = PlayerSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public bool AddItemToInventory(IStorable storable)
    {
        if (Inventory.Count >= MaxStroableItemCount)
        {
            //todo : 경고
            return false;
        }
        int weight = storable.GetWeight;
        foreach(var item in Inventory)
        {
            weight += item.GetWeight;
        }
        if (weight > MaxStroableItemWeight)
        {
            //todo : 경고
            PlayerSpeed /= 2;
        }
        else
        {
            PlayerSpeed = PlayerOriginSpeed;
        }
        IStorable st = (IStorable)storable.Clone();
        Treasure t = st as Treasure;
        Item it = st as Item;
        if (st is Treasure)
        {
            DontDestroyOnLoad(t.gameObject);
            Inventory.Add(t);
        }
        else
        {
            DontDestroyOnLoad(it.gameObject);
            Inventory.Add(it);
        }
        UIManager.Instance.GetCurrentMainUI<UI_Player>().RefreshInventory();
        return true;
    }

    public void GameStart()
    {
        PlayerCurrentHp = PlayerMaxHp;
        PlayerCurrentO2 = PlayerMaxO2;
    }
    public void GameOver()
    {
        for (int i = Inventory.Count - 1; i <= 0; i--)
        {
            IStorable st = Inventory[i];
            if (st is Treasure)
            {
                Inventory.RemoveAt(i);
                Treasure t = st as Treasure;
                Destroy(t.gameObject);
            }
            else
            {
                Inventory.RemoveAt(i);
                Item t = st as Item;
                Destroy(t.gameObject);
            }
        }
    }
}
