using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public float PlayerSpeed;
    public float PlayerOriginSpeed;
    public int PlayerMaxHp;
    public int PlayerMaxO2;
    public int Score = 1000;
    int playerCurrentHp;
    int playerCurrentO2;
    int currentMoney = 10000;
    float[] reductCoolDown = new float[5]
    {
        .5f,
        .8f,
        1f,
        1.5f,
        2f
    };
    public List<IStorable> Inventory = new List<IStorable>();
    public int MaxStroableItemCount = 4;
    public int MaxStroableItemWeight = 200;
    public int PlayerAttack = 1;
    public List<bool>[] TreasureIsFind;
    Coroutine OxygenReduce;
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
                GameOver();
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
    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            currentMoney = value;
            UI_Lobby ui = UIManager.Instance.GetCurrentMainUI<UI_Lobby>();
            if (ui == null)
                return;
            ui.SetCoin(currentMoney);
        }
    }

    Define.Scenes currentScene = Define.Scenes.Lobby;
    public Define.Scenes CurrentScene
    {
        get { return currentScene; }
        set 
        {
            if (currentScene == Define.Scenes.Lobby && value == Define.Scenes.Stage1)
            {
                currentScene = value;
                GameStart();
            }
            else if (currentScene == Define.Scenes.Stage1 && value == Define.Scenes.Lobby)
            {
                currentScene = value;
                RoundOver();
            }
            SceneManager.LoadSceneAsync((int)currentScene);
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
    public void RemoveAllInventoryItem()
    {
        foreach(var item in Inventory)
        {
            if (item is Treasure)
            {
                Treasure t = (Treasure)item;
                Destroy(t.gameObject);
            }
            else
            {
                Item item1 = (Item)item;
                Destroy(item1.gameObject);
            }
        }
        Inventory.Clear();
    }
    public void GameStart()
    {
        PlayerCurrentHp = PlayerMaxHp;
        PlayerCurrentO2 = PlayerMaxO2;

        TreasureIsFind = new List<bool>[DataManager.IsTreasureFind.Length];
        for (int i = 0; i < DataManager.IsTreasureFind.Length; i++)
        {
            TreasureIsFind[i] = new List<bool>();
            foreach(bool tFind in DataManager.IsTreasureFind[i])
            {
                TreasureIsFind[i].Add(tFind);
            }
        }
        OxygenReduce = StartCoroutine(O2Reduce());
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
        if (OxygenReduce != null)
            StartCoroutine(O2Reduce());
        OxygenReduce = null;
    }
    public void RoundOver()
    {
        for (int i = 0; i < TreasureIsFind.Length; i++)
        {
            DataManager.Instance.isTreasureFind[i] = new List<bool>();
            foreach (bool tFind in TreasureIsFind[i])
            {
                DataManager.Instance.isTreasureFind[i].Add(tFind);
                Debug.Log($"{i}, {tFind}");
            }
        }
        if (OxygenReduce != null)
            StartCoroutine(O2Reduce());
        OxygenReduce = null;
    }
    IEnumerator O2Reduce()
    {
        PlayerCurrentO2 = PlayerCurrentO2;
        while (true)
        {
            yield return new WaitForSeconds(1 / reductCoolDown[(int)CurrentScene - 1]);
            PlayerCurrentO2--;
        }
    }
}
