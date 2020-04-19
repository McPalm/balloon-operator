using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveResolver : MonoBehaviour
{
    public LayerMask Solid { set; get; }

    public (bool obstructed, int wallTouch, bool moved) Translate(Vector2 distance, float radius)
    {
        var hit = Physics2D.BoxCast(transform.position, Vector2.one * radius, 0f, distance, distance.magnitude, Solid);
        bool TouchingWall = false;
        int TouchingWallDirection = 0;
        bool moved = true;

        if (hit)
        {
            TouchingWall = distance.x != 0f && Mathf.Sign(distance.x) == Mathf.Sign(hit.point.x - transform.position.x);
            // try again slightly higher up
            var hit2 = Physics2D.BoxCast(
                origin: transform.position+ new Vector3(0f, .01f), 
                size: Vector2.one * radius, 
                angle: 0f, 
                direction: distance + new Vector2(0f, Mathf.Abs(distance.x)), 
                distance: distance.magnitude, 
                layerMask: Solid);
            if (!hit2)
            {
                transform.position += (Vector3)distance + new Vector3(0f, Mathf.Abs(distance.x));
                return (false, 0, true);
            }

            if (TouchingWall)
            {
                TouchingWallDirection = (int)Mathf.Sign(hit.point.x - transform.position.x);
            }
            if (hit.fraction > 0f)
            {
                distance *= hit.fraction;
                
            }
            else
            {
                moved = false;
                if (distance.x != 0f)
                {
                    if (distance.x < 0f && hit.point.x < transform.position.x)
                    {
                        distance = new Vector2(0f, distance.y);
                        //HMomentum *= wallStop;

                    }
                    else if (distance.x > 0f && hit.point.x > transform.position.x)
                    {
                        distance = new Vector2(0f, distance.y);
                        //HMomentum *= wallStop;
                    }
                }
                else
                {

                }
            }

        }
        else
            TouchingWall = false;

        transform.position += (Vector3)distance;

        return ( hit, TouchingWallDirection , moved);
    }
}
