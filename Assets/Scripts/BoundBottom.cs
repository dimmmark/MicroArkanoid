using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBottom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ball>())
        {
        Game.Instance.RemoveOneBall(collision.GetComponent<Ball>());
        collision.gameObject.SetActive(false);

        }
        Destroy(collision.gameObject);
    }
}
