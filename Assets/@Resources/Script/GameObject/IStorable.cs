using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable : ICloneable
{
    public void Store();
    public int GetWeight { get; }
    public Sprite GetSprite { get; }
}
