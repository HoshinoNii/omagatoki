using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

[Serializable]
public class Sound {
    public string name;
    public AudioClip Clip;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

    private void Awake() {

        Instance = this;

        //Automatically assign the sounds
        foreach(Sound sound in Sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start() {
        Play("PreperationPhaseBGM");
    }

    public void Play(string name) {
        //get the element from array
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
        else
            Debug.LogWarning("Cant Find Audio");   
    }

    public void PlayMultiple(string name) {
        //get the element from array
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s != null)
            s.source.PlayOneShot(s.Clip, s.volume);
        else
            Debug.LogWarning("Cant Find Audio of " + name);
    }

    public void Stop(string name) {
        //get the element from array
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
        else
            Debug.LogWarning("Cant Find Audio of " + name);
    }
}
