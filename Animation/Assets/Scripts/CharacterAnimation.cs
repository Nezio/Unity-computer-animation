using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private MovementInput mi;
    private bool playerMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        mi = player.GetComponent<MovementInput>();

    }

    void Update()
    {
        if(mi.speedInput != 0 && !playerMoving)
        { // run
            playerMoving = true;

            anim.CrossFade("Run", 0.5f);

            anim.SetBool("Idle", false);
            anim.SetBool("Run", true);
        }
        if(mi.speedInput == 0)
        { // stop running
            playerMoving = false;

            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
        }
        

    }
}
