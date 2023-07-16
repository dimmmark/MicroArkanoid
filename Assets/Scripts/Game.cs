using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetBall), .1f);
    }

    private void SetBall()
    {
        Pooler.Instance.SpawnFromPool("Ball", new Vector3(0, -10, 0), Quaternion.identity);
    }

}
