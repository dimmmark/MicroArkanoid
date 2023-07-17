using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    private void Start()
    {
       // _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + Vector3.down * _speed * Time.deltaTime );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>())
            Activate();
        else if (collision.GetComponent<BoundBottom>())
            Destroy(collision.gameObject);
    }

    protected virtual void Activate()
    {

    }
}
