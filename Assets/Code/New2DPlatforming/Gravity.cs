using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "new Gravity", menuName = "Mobile/Gravity", order = 42)]
public class GravityMultiplier : Mobile2DComponent
{
    public float multiplier = 0f;

    public override void Apply(MobileLifetimeObject mlo, InputToken inputToken, System.Action<string> eventDelegate)
    {
        mlo.gravityMultipler *= multiplier;
    }
}
