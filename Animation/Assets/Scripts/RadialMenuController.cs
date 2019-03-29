using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuController : MonoBehaviour
{
    public GameObject radialMenu;
    public CameraFollow cameraFollow;

    private bool radialMenuOpen = false;
    private float prevCameraInputSensitivity;
    private GameObject player;
    private MovementInput moveInpt;
    private CharacterAnimation charAnim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveInpt = player.GetComponent<MovementInput>();
        charAnim = player.GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !radialMenuOpen && moveInpt.speedInput == 0)
        {
            ShowRadialMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.Q) && radialMenuOpen) || (moveInpt.speedInput != 0 && radialMenuOpen))
        {
            HideRadialMenu();
        }

    }

    private void ShowRadialMenu()
    {
        radialMenuOpen = true;
        radialMenu.SetActive(true);

        // enable cursor
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        // disable camera movement
        prevCameraInputSensitivity = cameraFollow.inputSensitivity;
        cameraFollow.inputSensitivity = 0;
    }

    private void HideRadialMenu()
    {
        radialMenuOpen = false;
        radialMenu.SetActive(false);

        // disable cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // enable camera movement
        cameraFollow.inputSensitivity = prevCameraInputSensitivity;
    }

    public void ActionA()
    {
        Debug.Log("Action A");
        HideRadialMenu();
        charAnim.PlayAnim("Wave");
    }

    public void ActionB()
    {
        Debug.Log("Action B");
        HideRadialMenu();
        charAnim.PlayAnim("Nod");
    }

    public void ActionC()
    {
        Debug.Log("Action C");
        HideRadialMenu();
        charAnim.PlayAnim("Moonwalk");
    }

    public void ActionD()
    {
        Debug.Log("Action D");
        HideRadialMenu();
        charAnim.PlayAnim("Gunsling");
    }
}
