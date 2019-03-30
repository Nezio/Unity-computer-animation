using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private GameObject player;
    private MovementInput moveInpt;
    private CharacterAnimation charAnim;
    private bool AudioMoonwalkPlaying = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveInpt = player.GetComponent<MovementInput>();
        charAnim = player.GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        if(charAnim.AnimIsPlaying("Moonwalk") && !AudioMoonwalkPlaying)
        {
            AudioMoonwalkPlaying = true;
            FindAudioSource("AS-Moonwalk").Play();
        }
        else if(!charAnim.AnimIsPlaying("Moonwalk") && AudioMoonwalkPlaying)
        {
            AudioMoonwalkPlaying = false;
            FindAudioSource("AS-Moonwalk").Stop();
        }
         
    }

    public AudioSource FindAudioSource(string name)
    {
        foreach (Transform TRAudioSource in gameObject.transform)
        {
            if (TRAudioSource.gameObject.name == name)
            {
                return TRAudioSource.gameObject.GetComponent<AudioSource>();
            }
        }

        return null;
    }

    public void PlayGunshot()
    {
        FindAudioSource("AS-Gunshot").Play();
    }

    public void PlayFootstep()
    {
        FindAudioSource("AS-Footstep0" + Random.Range(1, 4)).Play();
    }
}
