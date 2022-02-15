using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile2D : MonoBehaviour
{
    public BoxCollider2D HardCollider;
    public BoxCollider2D SoftCollider;

    public LayerMask Solid { set; get; }

    public Mobile2DComponent[] components;

    public InputToken InputToken;

    public bool Grounded { get; set; }
    public int LastGrounded { get; set; }
    public int TouchingWallDirection { get; set; }

    [Range(0f, 1f)]
    public float popSpeed = .2f;
    [Range(0f, 1f)]
    public float VerticalWallDampen = .8f;
    [Range(0f, 1f)]
    public float HorizontalWallDampen = .8f;


    public event System.Action<string> OnTrigger;

    public float Gravity = 9f;

    RaycastHit2D[] results = new RaycastHit2D[1];

    public float SouthDelta
    {
        get => SoftCollider.offset.y + SoftCollider.size.y / 2f;
    }

    Vector2 debugPoint;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, debugPoint);
    }

    void Start()
    {
        Solid = LayerMask.GetMask("Solid");
    }

    public float HVelocity { get; set; }
    public float VVelocity { get; set; }

    public void FixedUpdate()
    {

        float distanceRight = DistanceInDirection(Vector2.right, Solid) - SoftCollider.size.x / 2f;
        float distanceLeft = DistanceInDirection(Vector2.left, Solid) - SoftCollider.size.x / 2f;
        float distanceToCeiling = DistanceInDirection(Vector2.up, Solid) - SoftCollider.size.y / 2f;

        Debug.Log(distanceLeft);
        debugPoint = transform.position + new Vector3(-distanceLeft, 0f);

        var mlo = new Mobile2DComponent.MobileLifetimeObject()
        {
            grounded = Grounded,
            touchingWallDirection = TouchingWallDirection,
            mobile = this,
        };
        // components
        for (int i = 0; i < components.Length; i++)
        {
            components[i].Apply(mlo, InputToken, OnTrigger);
            if (mlo.cancel)
                break;
        }
        // gravity
        Grounded = false;
        ApplyGravity(mlo.gravityMultipler);
        if (VVelocity < 0f)
        {
            // var distanceToGround = DistanceToGround();
            var groundLevel = transform.position.y - DistanceInDirection(Vector2.down, Solid);
            // Debug.Log($"groundLevel: {groundLevel}");
            if (transform.position.y + VVelocity * Time.fixedDeltaTime - SouthDelta <= groundLevel)
            {
                VVelocity = 0f;
                transform.position = new Vector2(transform.position.x, groundLevel + SouthDelta);
                Grounded = true;
            }
        }
        // movement
        var movement = new Vector2(HVelocity, VVelocity) * Time.fixedDeltaTime;
        if(VVelocity > 0f && distanceToCeiling < VVelocity * Time.fixedDeltaTime)
        {
            if (distanceToCeiling > 0f)
                movement.y = distanceToCeiling;
            else
                movement.y = 0f;
            VVelocity *= VerticalWallDampen;
        }
        if (movement.x > 0f && movement.x > distanceRight)
        {
            movement.x = Mathf.Max(distanceRight, 0f);
            HVelocity *= HorizontalWallDampen;
        }
        else if (movement.x < 0f && -movement.x > distanceLeft)
        {
            movement.x = Mathf.Min(-distanceLeft, 0f);
            HVelocity *= HorizontalWallDampen;
        }
        
        if(distanceLeft < 0f && distanceRight > 0f)
        {
            movement.x -= distanceLeft * popSpeed;
        }
        else if(distanceLeft > 0f && distanceRight < 0f)
        {
            movement.x += distanceRight * popSpeed;
        }

        var info = Translate(movement);
    }

    private void ApplyGravity(float multiplier)
    {
        VVelocity -= Time.fixedDeltaTime * Gravity * multiplier;
    }

    float DistanceInDirection(Vector2 direction, int LayerMask, float maxDistance = 120f)
    {
        var hit = Physics2D.Raycast(transform.position, direction, maxDistance, LayerMask);
        if(hit)
            return hit.distance;
        return maxDistance;
    }

    // for now, all the info returned is garbage
    public (bool obstructed, int wallTouch, bool moved, bool grounded) Translate(Vector2 movement)
    {
        //hit = Physics2D.BoxCast(transform.position, Vector2.one * radius, 0f, movement, movement.magnitude, Solid);
        RaycastHit2D[] result = new RaycastHit2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Solid);
        int hits = HardCollider.Cast(direction: movement, results: result, contactFilter: filter, distance: movement.magnitude);
        bool TouchingWall = false;
        int TouchingWallDirection = 0;
        bool moved = true;
        bool grounded = true;

        if (hits > 0)
        {
            var hit = result[0];
            moved = false;
        }

        if(moved)
            transform.position += (Vector3)movement;

        return (false, TouchingWallDirection, moved, grounded);
    }
}
