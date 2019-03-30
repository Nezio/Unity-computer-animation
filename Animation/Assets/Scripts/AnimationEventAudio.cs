using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventAudio : MonoBehaviour
{
    private AudioPlayer audioPlayer;

    private void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("AudioSources").GetComponent<AudioPlayer>();
    }
    
    private void PlayGunshot()
    {
        audioPlayer.PlayGunshot();
    }

    private void PlayFootstep()
    {
        audioPlayer.PlayFootstep();
    }

}
