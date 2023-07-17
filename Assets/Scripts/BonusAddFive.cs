using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAddFive : Bonus
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
       // transform.position += Vector3.down * _speed * Time.deltaTime;
    }
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
