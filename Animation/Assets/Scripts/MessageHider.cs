using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHider : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            gameObject.SetActive(false);
        }
    }
}
