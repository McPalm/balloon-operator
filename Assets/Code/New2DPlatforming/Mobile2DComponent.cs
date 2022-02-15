using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Mobile2DComponent : ScriptableObject
{

    abstract public void Apply(MobileLifetimeObject mlo, InputToken inputToken, System.Action<string> eventDelegate);



    public class MobileLifetimeObject
    {
        public bool cancel = false;
        public float gravityMultipler = 1f;

        public Mobile2D mobile;
        public bool grounded;
        public int touchingWallDirection;

    }
}


