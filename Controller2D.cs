using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    private const float rayWidth = 0.015f;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    public LayerMask collisionMask;
    public LayerMask shuriken;
    public CollisionInfo collisions;

    private RaycastOrigins raycastOrigins;
    private BoxCollider2D collider;
    private Player player;
  
    void Start()
    {   
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + rayWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			//Debug.Log(rayOrigin);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength*3, Color.green);
            if (hit)
            {
                velocity.x = (hit.distance - rayWidth) * directionX;
				Debug.Log(hit.distance);
				rayLength = hit.distance;

                collisions.left = directionX == -1;
				//Debug.Log("direc " + directionX + " left " + collisions.left);
                collisions.right = directionX == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        //Mathf.Sign =  Return value is 1 when f is positive or zero, -1 when f is negative.
        float directionY = Mathf.Sign(velocity.y);
        //Mathf.Abs = Returns the absolute value of f.
        float rayLength = Mathf.Abs(velocity.y) + rayWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            //Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.green);
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            RaycastHit2D hitShurikenTop = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, shuriken);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength*10, Color.red);
            if (hit)
            {
                velocity.y = (hit.distance - rayWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
            if (hitShurikenTop)
            {
                Debug.Log("ana are mere");
            }
        }
    }

    void UpdateRaycastOrigins()
    {

        Bounds bounds = collider.bounds;  // Bounds Represents an axis aligned bounding box.
        bounds.Expand(rayWidth * -2);    //Expand the bounds by increasing its size by amount along each side.

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(rayWidth * -2);

        //Clamps a value between a minimum float and maximum float value.
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        // bounds.size = The total size of the box. This is always twice as large as the extents.
    }
    
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
    
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}