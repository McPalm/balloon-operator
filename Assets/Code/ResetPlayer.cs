using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform Player;


    private void Update()
    {
        if (Player.position.y < -12f)
            Player.position = transform.position;
    }
}
