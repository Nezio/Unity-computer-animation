using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public GameObject player;

    private CharacterController controller;
    private MovementInput mi;

    // Start is called before the first frame update
    void Start()
    {
         mi = player.GetComponent<MovementInput>();
        controller = mi.controller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide!");

        controller.SimpleMove(new Vector3(0, mi.fallYspeed, 0));
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
