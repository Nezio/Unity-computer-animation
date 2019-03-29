using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public GameObject player;

    private CharacterController controller;
    private MovementInput mi;
    private Vector3 fallVelocity;
    
    void Start()
    {
        mi = player.GetComponent<MovementInput>();
        controller = mi.controller;

    }
    
    void Update()
    {
        // fall
        fallVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);

        Vector3 capsuleStart = new Vector3(transform.position.x, transform.position.y - (gameObject.GetComponent<CapsuleCollider>().height / 8), transform.position.z);
        Vector3 capsuleEnd = new Vector3(transform.position.x, transform.position.y + (gameObject.GetComponent<CapsuleCollider>().height / 8), transform.position.z);
        bool isGrounded = Physics.CheckCapsule(capsuleStart, capsuleEnd, 0.5f, 1, QueryTriggerInteraction.Ignore);

        //bool isGrounded = Physics.CheckSphere(gameObject.transform.position, 0.7f, 1, QueryTriggerInteraction.Ignore);
        if (isGrounded && fallVelocity.y < 0)
            fallVelocity.y = 0;

        //Debug.Log("Fall V: " + fallVelocity);
    }

    public Vector3 GetFallVelocity()
    {
        return fallVelocity;
    }

    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - (gameObject.GetComponent<CapsuleCollider>().height/8), transform.position.z), 0.2f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + (gameObject.GetComponent<CapsuleCollider>().height / 8), transform.position.z), 0.2f);
        */
    }


}
