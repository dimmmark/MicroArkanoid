using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sound[] _sounds;
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        foreach (var s in _sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;

        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.Source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.Source.Stop();
    }
}
