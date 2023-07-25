using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _winScrene;
    [SerializeField] private GameObject _loseScrene;
    void Start()
    {
        
    }

    public void ShowWinScrene()
    {
        _winScrene.SetActive(true);
    }
    public void ShowLoseScrene()
    {
        _loseScrene.SetActive(true);
    }
}
