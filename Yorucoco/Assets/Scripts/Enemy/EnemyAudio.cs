using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [Header("Hover Over Elements for more details")]
    [Tooltip("This library will store the shared sound effects of all towers such as enemy weapon impacts or death sounds")]
    public SoundLibrary GeneralEnemySoundLibrary;

    [Tooltip("This is Tower Specific Data")]
    public SoundLibrary EnemySpecificLibrary;

    public AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    //this will pull from the general tower sounds and should be used 
    //as a reference for the enemy (When they deal damage for example)

    //NOTE - Array Values: 0 = Attacking Audio, 1 = Movement Audio, 2 = Death Audio, 3 = Reloading
    public void GeneralAudio(int index = 0, float volume = 1) {
        audioSource.PlayOneShot(GeneralEnemySoundLibrary.GetSound(index), volume);
    }

    public void Death(float volume = 1) {
        audioSource.PlayOneShot(EnemySpecificLibrary.GetSound(2), volume);
    }

    public void Shooting(float volume = 1) {
        audioSource.PlayOneShot(EnemySpecificLibrary.GetSound(0), volume);
    }

    public void Walking(float volume = 1) {
        audioSource.PlayOneShot(EnemySpecificLibrary.GetSound(1), volume);
    }
}
