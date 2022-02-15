using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "new Movement", menuName = "Mobile/Movement", order = 42)]
public class Movement : Mobile2DComponent
{
    public float speed = 5f;

    public override void Apply(MobileLifetimeObject mlo, InputToken inputToken, System.Action<string> eventDelegate)
    {
        mlo.mobile.HVelocity = inputToken.Direction.x * speed;
    }
}
