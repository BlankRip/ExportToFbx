using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_AudioKing : MonoBehaviour
{
    public static LB_AudioKing instance;
    private AudioSource seSource;

    private void Awake() {
        if(instance == null) {
            instance = this;
            seSource = GetComponent<AudioSource>();
        }
    }

    public void PlayOneShot(AudioClip clip) {
        seSource.PlayOneShot(clip);
    }
}
