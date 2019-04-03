using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GroundCollider groundCollider;
    public GroundNearDetector groundNearDetector;

    [HideInInspector]
    public string moveMode = "run";

    private Animator anim;
    private GameObject player;
    private MovementInput moveInpt;
    private bool moveAnimPlaying = false;
    private bool fallAnimPlaying = false;
    private bool fallLandAnimPlaying = false;
    private float maxFallVelocity = 0;
    private bool allowFallLandEndCheck = false;
    private AudioPlayer audioPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        moveInpt = player.GetComponent<MovementInput>();
        audioPlayer = GameObject.FindGameObjectWithTag("AudioSources").GetComponent<AudioPlayer>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !fallAnimPlaying && !fallLandAnimPlaying)
        {
            if (moveMode == "run")
            { // switch to walk
                moveInpt.speed = moveInpt.walkSpeed;

                moveMode = "walk";
                SetAnimParametarAndClearAllOther("walk");

                if(moveInpt.speedInput != 0)
                    anim.CrossFade("Walk", 0.1f);
            }
            else
            { // switch to run
                moveInpt.speed = moveInpt.runSpeed;

                moveMode = "run";
                SetAnimParametarAndClearAllOther("run");

                if (moveInpt.speedInput != 0)
                    anim.CrossFade("Run", 0.1f);
            }
        }

        if (moveMode == "run" && !fallAnimPlaying && !fallLandAnimPlaying)
        {
            if (moveInpt.speedInput != 0 && !moveAnimPlaying)
            { // run
                moveAnimPlaying = true;
                anim.CrossFade("Run", 0.1f);
                SetAnimParametarAndClearAllOther("run");
            }
            if (moveInpt.speedInput == 0)
            { // stop running and walking
                moveAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");
            }
        }
        else if (moveMode == "walk" && !fallAnimPlaying && !fallLandAnimPlaying)
        {
            if (moveInpt.speedInput != 0 && !moveAnimPlaying)
            { // walk
                moveAnimPlaying = true;
                anim.CrossFade("Walk", 0.1f);
                SetAnimParametarAndClearAllOther("walk");
            }
            if (moveInpt.speedInput == 0)
            { // stop walking and running
                moveAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");
            }
        }

        if(groundCollider.GetFallVelocity().y < -5 && !fallAnimPlaying)
        { // fall
            fallAnimPlaying = true;
            anim.CrossFade("Fall", 0.1f);
            SetAnimParametarAndClearAllOther("fall");
            moveAnimPlaying = false;
        }
        if(fallAnimPlaying)
        {
            // record max velocity to decide should landing animation be played
            if (groundCollider.GetFallVelocity().y < maxFallVelocity)
                maxFallVelocity = groundCollider.GetFallVelocity().y;
            
            if (groundNearDetector.nearGround && maxFallVelocity < -12)
            { // fall-land
                SetAnimParametarAndClearAllOther("fall-land");
                anim.CrossFade("Fall-Land", 0.1f);
                fallLandAnimPlaying = true;
                maxFallVelocity = 0;

                audioPlayer.FindAudioSource("AS-Fall-Land").Play();
            }
            if (groundCollider.GetFallVelocity().y == 0)
            { // stop falling
                /*if (maxFallVelocity < -12)
                { // fall-land
                    SetAnimParametarAndClearAllOther("fall-land");
                    anim.CrossFade("Fall-Land", 0.1f);
                }*/

                

                fallAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");

            }

        }

        if (fallLandAnimPlaying)
        {
            moveInpt.fallLandMultiplier = 0;
        }
        if (AnimIsPlaying("Fall-Land"))
        {
            allowFallLandEndCheck = true;
        }
        if (allowFallLandEndCheck && !AnimIsPlaying("Fall-Land"))
        {
            fallAnimPlaying = false;
            moveInpt.fallLandMultiplier = 1;
            allowFallLandEndCheck = false;
            fallLandAnimPlaying = false;
        }
        

    }

    public void PlayAnim(string animation)
    {
        if (animation == "Idle")
        {
            SetAnimParametarAndClearAllOther("idle");
        }

        if (animation  == "Wave")
        {
            anim.CrossFade("Wave", 0.1f);
            SetAnimParametarAndClearAllOther("wave");
        }

        if (animation == "Nod")
        {
            anim.CrossFade("Nod", 0.1f);
            SetAnimParametarAndClearAllOther("nod");
        }

        if (animation == "Moonwalk")
        {
            anim.CrossFade("Moonwalk", 0.1f);
            SetAnimParametarAndClearAllOther("moonwalk");
        }

        if (animation == "Gunsling")
        {
            anim.CrossFade("Gunsling", 0.1f);
            SetAnimParametarAndClearAllOther("gunsling");
        }

    }

    private void SetAnimParametarAndClearAllOther(string animParametar)
    {
        foreach (AnimatorControllerParameter parametar in anim.parameters)
        {
            var x = parametar.name;
            anim.SetBool(parametar.name, false);
        }

        anim.SetBool(animParametar, true);
    }

    public bool AnimIsPlaying(string animation)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }
}
