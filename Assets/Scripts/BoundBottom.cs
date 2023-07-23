using UnityEngine;

public class BoundBottom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            Game.Instance.RemoveOneBall(collision.GetComponent<Ball>());
            collision.gameObject.SetActive(false);

        }
        else
            Destroy(collision.gameObject);
    }
}
