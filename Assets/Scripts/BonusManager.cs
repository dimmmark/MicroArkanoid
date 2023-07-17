using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonusArray;
    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 5, 5);
    }

    private void SpawnBall()
    {
        Instantiate(_bonusArray[1], new Vector3(Random.Range(-24,24), 46.5f, 0), Quaternion.identity);
    }
}
