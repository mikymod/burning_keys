using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static UnityEvent<KeyCode> KeyCodeInput = new UnityEvent<KeyCode>();
    public static UnityEvent MouseInput = new UnityEvent();

    void Update()
    {
        // FIXME: workaround with camera
        if (CameraMgr.moving)
        {
            print("Microphone muovo");
            return;
        }
        
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                KeyCodeInput?.Invoke(keyCode);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            MouseInput?.Invoke();
        }        
    }
}
