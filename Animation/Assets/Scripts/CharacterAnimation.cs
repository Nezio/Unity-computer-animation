using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        { // run
            anim.CrossFade("Run", 0.5f);

            anim.SetBool("Idle", false);
            anim.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        { // stop runing
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
        }

    }
}
