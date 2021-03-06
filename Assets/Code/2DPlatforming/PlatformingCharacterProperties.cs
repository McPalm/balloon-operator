using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PC Props", menuName = "New PC Props", order = 10), System.Serializable]
public class PlatformingCharacterProperties : ScriptableObject
{
    public AnimationCurve AccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(5f, 0f) });

    public float MaxSpeed = 5f;
    [Range(0f, 1f)]
    public float Traction = .9f;
    [Range(0f, 1f)]
    public float AirControl = .25f;
    public float Gravity = 19f;
    public float JumpForce = 9f;
    [Range(0f, 1f)]
    public float PeakJumpGravity = .5f;
    [Range(0f, 1f)]
    public float JumpCap = .5f;
    public bool airTurn = false;
    public float MaxFallSpeed = 10f;
    public int CyoteTime = 5;
}
