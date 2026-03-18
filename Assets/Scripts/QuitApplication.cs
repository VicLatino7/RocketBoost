using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        RespondToQuitApplication();
    }

    void RespondToQuitApplication()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("Quitting Application");
            Application.Quit();
        }
    }
}
