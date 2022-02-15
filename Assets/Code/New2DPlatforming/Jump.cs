using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "new Jump", menuName = "Mobile/Jump", order = 42)]
public class Jump : Mobile2DComponent
{
    public float JumpForce = 10f;

    public override void Apply(MobileLifetimeObject mlo, InputToken inputToken, Action<string> eventDelegate)
    {
        if(inputToken.JumpPressed && mlo.grounded)
        {
            Debug.Log("Jump");
            mlo.mobile.VVelocity = JumpForce;
        }
    }
}
