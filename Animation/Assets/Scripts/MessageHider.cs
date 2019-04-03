using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHider : MonoBehaviour
{
    public GameObject message;

    private bool messageShown = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(messageShown)
            { // hide mesage
                message.SetActive(false);
                messageShown = false;
            }
            else
            { // show message
                message.SetActive(true);
                messageShown = true;
            }
            
        }
    }
}
