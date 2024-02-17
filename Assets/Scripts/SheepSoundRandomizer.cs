﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSoundRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;

    [Range(0.1f, 0.3f)]
    public float volumeChangeMultiplier = 0.2f;

    [Range(0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f;

    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            RandomBleat();
        }
    }

    public void RandomBleat() {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.volume = Random.Range(0.5f - volumeChangeMultiplier, 0.5f);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.PlayOneShot(source.clip);
    }
}
