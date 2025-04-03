using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IStorable
{
    public int Weight;
    public Sprite Sprite;
    public int GetWeight {  get { return Weight; } }
    public Sprite GetSprite { get { return Sprite; } }
    public virtual void UseItem()
    {

    }
    public virtual object Clone()
    {
        return null;
    }

    public void Store()
    {
        GameManager.Instance.AddItemToInventory(this);
    }
}
