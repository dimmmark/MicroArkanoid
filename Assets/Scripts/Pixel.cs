using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    // private Color _color;
    //public static event System.Action<Pixel> OnCollidedPixel;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    public void SetColor(Color color)
    {
        // _renderer.material.color = color;
        //_color = color;
        _materialPropertyBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    OnCollidedPixel.Invoke(this);
    //    gameObject.SetActive(false);
    //}

}
