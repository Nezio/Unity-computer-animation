using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDialog : MonoBehaviour
{
    public GameObject dialog;
    public CameraFollow cameraFollow;

    private bool dialogShown = false;
    private float prevCameraInputSensitivity;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dialogShown)
            {
                HideDialog();
            }
            else
            {
                ShowDialog();
            }

        }
    }

    private void HideDialog()
    {
        dialog.SetActive(false);
        dialogShown = false;

        // disable cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // enable camera movement
        cameraFollow.inputSensitivity = prevCameraInputSensitivity;
    }

    private void ShowDialog()
    {
        dialog.SetActive(true);
        dialogShown = true;

        // enable cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // disable camera movement
        prevCameraInputSensitivity = cameraFollow.inputSensitivity;
        cameraFollow.inputSensitivity = 0;
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void Resume()
    {
        HideDialog();
    }
}
