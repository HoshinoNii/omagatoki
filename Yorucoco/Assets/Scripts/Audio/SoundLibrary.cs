using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Tower_Defense/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    [Tooltip("0 = Attacking Audio, 1 = Movement Audio, 2 = Death Audio, 3 = Reloading")]
    public AudioClip[] clips;

    public AudioClip GetSound(int clipIndex)
    {
        //Debug.Log(clips[clipIndex]);
        return clips[clipIndex];
    }
}
