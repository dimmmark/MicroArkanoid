using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelsManager : MonoBehaviour
{
    [SerializeField] Pixel _pixelPrefab;
    [SerializeField] Texture2D[] _textures;
    [SerializeField] Texture2D _texture;
    [SerializeField] float _distanceBetweenPixels;
    private List<Pixel> _pixelList = new List<Pixel>();
    private float _numberOfPixelsOnStart;
    public static event System.Action OnEndPixels;
    private void Start()
    {
        //Invoke(nameof(MakeImage), .25f);
    }
    public void MakeImage()
    {
        for (int y = 0; y < GetTextureFromArray().width; y++)
        {
            for (int x = 0; x < GetTextureFromArray().height; x++)
            {
                Color color = _texture.GetPixel(x, y);
                if (color.a < 0.5f) continue;
                Vector3 position = transform.position + new Vector3(x * _pixelPrefab.transform.localScale.x
                    + 0.5f, y * _pixelPrefab.transform.localScale.y + .5f, 0) * _distanceBetweenPixels;
                if(color.a >.5f && color.a < .9f )
                {
                    Pooler.Instance.SpawnFromPool("IronPixel", position, Quaternion.identity);
                }
                else
                {
                Pixel pixel = Pooler.Instance.SpawnFromPool("Pixel", position, Quaternion.identity).GetComponent<Pixel>();
                pixel.SetColor(color);
                _pixelList.Add(pixel);
                }
            }
        }
    }
   
    public void RemoveOnePixel(Pixel pixel)
    {
        _pixelList.Remove(pixel);
        if(_pixelList.Count <= 0)
            OnEndPixels?.Invoke();
    }
    private Texture2D GetTextureFromArray()
    {
        _texture = _textures[Game.Instance.LevelIndex];
        //_texture = _textures[0];
        return _texture;
    }
    private void OnEnable()
    {
        Ball.OnCollidedPixel += RemoveOnePixel;
    }
    private void OnDisable()
    {
        Ball.OnCollidedPixel -= RemoveOnePixel;
    }
}
