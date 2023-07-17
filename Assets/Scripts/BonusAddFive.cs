using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BonusAddFive : Bonus
{
    protected override void Activate()
    {
        base.Activate();
       for (int i = 0; i < 5; i++)
        {
            Game.Instance.SetBall();
        }
        Destroy(gameObject);
    }
}
