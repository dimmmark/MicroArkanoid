using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlatform : Bonus
{
    public static event System.Action OnBonusPlatform;
    protected override void Activate()
    {
        base.Activate();
        OnBonusPlatform?.Invoke();
        Destroy(gameObject);
    }
}
