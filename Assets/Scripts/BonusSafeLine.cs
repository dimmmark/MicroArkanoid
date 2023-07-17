using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSafeLine : Bonus
{
    public static event System.Action OnBonusSafeLine;
    protected override void Activate()
    {
        base.Activate();
        OnBonusSafeLine?.Invoke();
        Destroy(gameObject);
    }
    
}
