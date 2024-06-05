using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void PlayStepSound()
    {
        float rndPitch = Random.Range(0.5f, 1f);
        AudioSource playerAudio = this.GetComponent<AudioSource>();
        playerAudio.pitch = rndPitch;
        playerAudio.volume = 0.2f;
        playerAudio.Play();
    }

    public void PlayStepRunSound()
    {
        AudioSource playerAudio = this.GetComponent<AudioSource>();
        playerAudio.pitch = 1.3f;
        playerAudio.volume = 0.4f;
        playerAudio.Play();
    }
}
