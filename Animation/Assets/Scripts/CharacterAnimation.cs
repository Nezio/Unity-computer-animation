using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GroundCollider groundCollider;
    public GroundNearDetector groundNearDetector;

    [HideInInspector]
    public string moveMode = "run";

    private Animator anim;
    private GameObject player;
    private MovementInput mi;
    private bool moveAnimPlaying = false;
    private bool fallAnimPlaying = false;
    private float maxFallVelocity = 0;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        mi = player.GetComponent<MovementInput>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !fallAnimPlaying)
        {
            if (moveMode == "run")
            { // switch to walk
                mi.speed = mi.walkSpeed;

                moveMode = "walk";
                SetAnimParametarAndClearAllOther("walk");

                if(mi.speedInput != 0)
                    anim.CrossFade("Walk", 0.1f);
            }
            else
            { // switch to run
                mi.speed = mi.runSpeed;

                moveMode = "run";
                SetAnimParametarAndClearAllOther("run");

                if (mi.speedInput != 0)
                    anim.CrossFade("Run", 0.1f);
            }
        }

        if (moveMode == "run" && !fallAnimPlaying)
        {
            if (mi.speedInput != 0 && !moveAnimPlaying)
            { // run
                moveAnimPlaying = true;
                anim.CrossFade("Run", 0.1f);
                SetAnimParametarAndClearAllOther("run");
            }
            if (mi.speedInput == 0)
            { // stop running and walking
                moveAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");
            }
        }
        else if (moveMode == "walk" && !fallAnimPlaying)
        {
            if (mi.speedInput != 0 && !moveAnimPlaying)
            { // walk
                moveAnimPlaying = true;
                anim.CrossFade("Walk", 0.1f);
                SetAnimParametarAndClearAllOther("walk");
            }
            if (mi.speedInput == 0)
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
                maxFallVelocity = 0;
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


        if (AnimIsPlaying("Fall-Land"))
        {
            mi.fallLandMultiplier = 0;
            //Debug.Log("fall-land");
        }
        else
        {
            mi.fallLandMultiplier = 1;
            //Debug.Log("ready!");
        }

        //Debug.Log(mi.fallLandMultiplier);


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

    private bool AnimIsPlaying(string animation)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }
}
