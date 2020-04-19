﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputToken InputToken;

    // Start is called before the first frame update
    void Start()
    {
        InputToken = new InputToken();
        GetComponent<PlatformingCharacter>().InputToken = InputToken;
        GetComponent<Repair>().InputToken = InputToken;
    }

    public void HandleMove(InputAction.CallbackContext e)
    {
        InputToken.Direction = e.ReadValue<Vector2>();
    }

    public void HandleUse(InputAction.CallbackContext e)
    {
        if (e.started)
        {
            InputToken.PressUse();
            InputToken.UseHeld = true;
        }
        else if(e.canceled)
        {
            InputToken.UseHeld = false;
        }
    }

    public void HandleJump(InputAction.CallbackContext e)
    {
        if (e.started)
        {
            InputToken.PressJump();
            InputToken.JumpHeld = true;
        }
        else if(e.canceled)
        {
            InputToken.JumpHeld = false;
        }
    }
}
