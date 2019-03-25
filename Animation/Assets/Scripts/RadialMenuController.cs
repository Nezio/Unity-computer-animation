using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuController : MonoBehaviour
{
    public GameObject radialMenu;
    public CameraFollow cameraFollow;

    private bool radialMenuOpen = false;
    private float prevCameraInputSensitivity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !radialMenuOpen)
        { // show radial menu
            radialMenuOpen = true;
            radialMenu.SetActive(true);
            Debug.Log("Radial");

            // enable cursor
            Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;

            // disable camera movement
            prevCameraInputSensitivity = cameraFollow.inputSensitivity;
            cameraFollow.inputSensitivity = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && radialMenuOpen)
        { // hide radial menu
            radialMenuOpen = false;
            radialMenu.SetActive(false);
            Debug.Log("resumed");

            // disable cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable camera movement
            cameraFollow.inputSensitivity = prevCameraInputSensitivity;
        }



    }
}
