using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public GameObject player;

    private CharacterController controller;
    private MovementInput mi;
    private Vector3 fallVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        mi = player.GetComponent<MovementInput>();
        controller = mi.controller;

    }

    // Update is called once per frame
    void Update()
    {
        fallVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);

        float GroundDistance = 0.7f;
        bool isGrounded = Physics.CheckSphere(gameObject.transform.position, GroundDistance, 1, QueryTriggerInteraction.Ignore);
        if (isGrounded && fallVelocity.y < 0)
            fallVelocity.y = 0;

        //Debug.Log("Ground: " + isGrounded);
    }

    
}
