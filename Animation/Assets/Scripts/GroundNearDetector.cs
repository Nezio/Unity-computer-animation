using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundNearDetector : MonoBehaviour
{
    [HideInInspector]
    public bool nearGround = false;

    void Update()
    {
        //float groundNearDistance = 0.7f;
        //nearGround = Physics.CheckSphere(gameObject.transform.position, groundNearDistance, 1, QueryTriggerInteraction.Ignore);
    }

    private void OnTriggerEnter(Collider other)
    {
        nearGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        nearGround = false;
    }
}
