using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector2 origPos, targetPos;
    private float timeToMove = 0.2f;
    private LayerMask collisionCheck;


    private void Start()
    {
        collisionCheck = LayerMask.GetMask("Obstacles");
    }
    void Update()
    {
        if (!isMoving && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                       Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            Vector2 direction = GetDirection();
            if (CanMove(transform.position, direction))
            {
                StartCoroutine(moveToken(direction));
            }
        }
    }

    private Vector2 GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return Vector2.down;
        }
        else  if (Input.GetKeyDown(KeyCode.D))
        {
            return Vector2.right;
        }
        else
        {
           return Vector2.zero;
        }
    }

    private bool CanMove(Vector2 currentPosition, Vector2 direction)
    {
        // Check for collisions in the target position
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, 1.0f, collisionCheck);
        return !hit.collider; // Return true if no collision detected
    }



    private IEnumerator moveToken(Vector2 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;



        isMoving = false;

    }
}
