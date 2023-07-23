using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float _sencetivity = 25f;
    [SerializeField] float _maxXPosition = 24f;
    private float _xPosition;
    private float _oldMouseX;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPosition += delta * _sencetivity / Screen.width;
            _xPosition = Mathf.Clamp(_xPosition, -_maxXPosition, _maxXPosition);
            transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
        }
    }
    public void ExpandPlatform()
    {
        transform.localScale = new Vector3(transform.localScale.x +.2f, 1, 1);
    }
    private void OnEnable()
    {
        BonusPlatform.OnBonusPlatform += ExpandPlatform;

    }
    private void OnDisable()
    {
        BonusPlatform.OnBonusPlatform -= ExpandPlatform;
    }
}
