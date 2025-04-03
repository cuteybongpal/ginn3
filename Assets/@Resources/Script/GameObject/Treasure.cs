using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IStorable
{
    public Sprite Sprite;
    public string Name;
    public int Weight;
    public int Worth;
    public int GetWeight {  get { return Weight; } }
    public Sprite GetSprite { get { return Sprite; } }
    public void Store()
    {
        if (GameManager.Instance.AddItemToInventory(this))
            Destroy(this);
    }

    public virtual object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        Treasure t = go.AddComponent<Treasure>();
        t.Weight = Weight;
        t.Sprite = Sprite;
        t.Name = Name;
        t.Worth = Worth;
        t.Sprite = Sprite;
        return t;
    }
}
