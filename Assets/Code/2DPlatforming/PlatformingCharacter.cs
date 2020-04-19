using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingCharacter : Mobile
{
    public InputToken InputToken { get; set; }

    public PlatformingCharacterProperties Properties;


    int cyoteTime = 5;
    public event System.Action OnJump;

    // Update is called once per frame
    new protected void FixedUpdate()
    {
        if(InputToken == null)
        {
            base.FixedUpdate();
            return;
        }
        var x = InputToken.Direction.x;
        var currentSpeed = Mathf.Abs(HMomentum);
        if (x < .25f && x > -.25f)
            x = 0f;
        if (x > .75f)
            x = 1f;
        if (x < -.75f)
            x = -1f;
        if (x != 0f && (Grounded || Properties.airTurn) )
            FaceRight = x > 0f;

        var desiredSpeed = x * Properties.MaxSpeed;
        bool breaking = (Mathf.Abs(desiredSpeed) < Mathf.Abs(HMomentum) || Mathf.Sign(desiredSpeed) != Mathf.Sign(HMomentum));
        var accel = Properties.AccelerationCurve.Evaluate(breaking ? -currentSpeed :currentSpeed);
        if (!Grounded)
            accel *= Properties.AirControl;
        HMomentum = Mathf.Clamp(desiredSpeed, HMomentum - accel, HMomentum + accel);
        if (Grounded && breaking)
            HMomentum *= Properties.Traction;

        // jump resolver
        if (Grounded)
            cyoteTime = Properties.CyoteTime + 1;
        else
            cyoteTime--;
        if (cyoteTime > 0 && InputToken.JumpPressed)
        {
            VMomentum = Properties.JumpForce;
            OnJump?.Invoke();
            cyoteTime = 0;
            InputToken.ConsumeJump();
        }
        
        if (Grounded == false && InputToken.JumpHeld == false && VMomentum > 0f)
        {
            VMomentum *= Properties.JumpCap;
        }

        // Gravity Manipulation
        if (Grounded == false && InputToken.JumpHeld && (VMomentum < 1f && VMomentum > -1f))
        {
            Gravity = Properties.Gravity * Properties.PeakJumpGravity;
        }
        else if (VMomentum < -Properties.MaxFallSpeed)
            Gravity = 0f;
        else
            Gravity = Properties.Gravity;

        

        base.FixedUpdate();
    }
}
