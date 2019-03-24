using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private MovementInput mi;
    private bool moveAnimPlaying = false;
    public string moveMode = "run";

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        mi = player.GetComponent<MovementInput>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (moveMode == "run")
            { // switch to walk
                mi.speed = mi.walkSpeed;

                moveMode = "walk";
                SetAnimParametarAndClearAllOther("walk");

                if(mi.speedInput != 0)
                    anim.CrossFade("Walk", 0.5f);
            }
            else
            { // switch to run
                mi.speed = mi.runSpeed;

                moveMode = "run";
                SetAnimParametarAndClearAllOther("run");

                if (mi.speedInput != 0)
                    anim.CrossFade("Run", 0.5f);
            }
        }

        if (moveMode == "run")
        {
            if (mi.speedInput != 0 && !moveAnimPlaying)
            { // run
                moveAnimPlaying = true;
                anim.CrossFade("Run", 0.5f);
                SetAnimParametarAndClearAllOther("run");
            }
            if (mi.speedInput == 0)
            { // stop running and walking
                moveAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");
            }
        }
        else if (moveMode == "walk")
        {
            if (mi.speedInput != 0 && !moveAnimPlaying)
            { // walk
                moveAnimPlaying = true;
                anim.CrossFade("Walk", 0.5f);
                SetAnimParametarAndClearAllOther("walk");
            }
            if (mi.speedInput == 0)
            { // stop walking and running
                moveAnimPlaying = false;
                SetAnimParametarAndClearAllOther("idle");
            }
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
}
