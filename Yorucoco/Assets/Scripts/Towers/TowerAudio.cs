using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudio : MonoBehaviour
{   [Header("Hover Over Elements for more details")]
    [Tooltip("This library will store the shared sound effects of all towers such as enemy weapon impacts or death sounds")]
    public SoundLibrary GeneralTowerSoundLibrary;

    [Tooltip("This is Tower Specific Data")]
    public SoundLibrary TowerSpecificLibrary;

    public AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        Construction(.3f);
    }

    //this will pull from the general tower sounds and should be used 
    //as a reference for the enemy (When they deal damage for example)

    //NOTE - Array Values: 0 = Attacking Audio, 1 = Movement Audio, 2 = Death Audio, 3 = Reloading

    public void PlayGeneralAudio(int index = 0, float  volume = 1) {
        audioSource.PlayOneShot(GeneralTowerSoundLibrary.GetSound(index), volume);
    }

    //Plays shooting sound
    public void Shooting(float volume = 1) {
        audioSource.PlayOneShot(TowerSpecificLibrary.GetSound(0), volume);
    }

    public void Death(float volume = 1) {
        audioSource.PlayOneShot(GeneralTowerSoundLibrary.GetSound(1), volume);
    }

    public void Construction(float volume = 1) {
        audioSource.PlayOneShot(GeneralTowerSoundLibrary.GetSound(2), volume);
    }

    //Plays Reloading sound (If Exists)
    public void Reloading(float volume = 1) {
        audioSource.PlayOneShot(TowerSpecificLibrary.GetSound(3), volume);
    }
}
