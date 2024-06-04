using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void PlayStepSound()
    {
        float rndPitch = Random.Range(1f, 3f);
        AudioSource playerAudio = this.GetComponent<AudioSource>();
        playerAudio.pitch = rndPitch;
        playerAudio.Play();
    }
}
