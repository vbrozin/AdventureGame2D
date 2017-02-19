using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.

    private bool isMovingObj = false;
    private bool isInPlace = false;

    private Vector3 placeTarget;
    private Vector3 placeDirection;

    private GameObject movableObj;

    public void Move () {

        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v);

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        if(!isMovingObj)
        {
            // Normal move
            if (move.magnitude != 0)
            {
                if (move.magnitude * GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
                    // ... add a force to the player.
                    GetComponent<Rigidbody2D>().AddForce(move * moveForce);

                if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
                    GetComponent<Rigidbody2D>().velocity = move * maxSpeed;

                // If the input is moving the player right and the player is facing left...
                Flip(move);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        } else if(movableObj)
        {
            // Move object mode
            if (!isInPlace && Vector2.Distance(transform.position, placeTarget) > 0.01)
            {
                PlaceCharacter();
                transform.Translate(placeDirection * Time.deltaTime);
            } else if(!isInPlace)
            {
                isInPlace = true;
            }
                else
            {
                transform.Translate(move * Time.deltaTime);
                movableObj.transform.Translate(move * Time.deltaTime);
            }
        }


    }

    internal void StopMoving()
    {
        if(isMovingObj)
        {
            isMovingObj = false;
            placeTarget = Vector3.zero;
            movableObj = null;
        }
    }

    private void Flip(Vector2 move)
    {
        //TODO
    }

    public void StartMoving(GameObject movable)
    {
        if (!isMovingObj)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isMovingObj = true;
            isInPlace = false;
            movableObj = movable;
            PlaceCharacter();
        }
    }

    private void PlaceCharacter()
    {
        Vector2 delta = movableObj.transform.position - transform.position;
        Vector2 direction = ComputeDirection(delta);
        if (direction.Equals(Vector2.up) || direction.Equals(Vector2.down))
        {
            placeTarget = new Vector3(movableObj.transform.position.x, transform.position.y, transform.position.z);
            placeDirection = Vector3.right * Math.Sign(delta.x);
        }
        else
        {
            placeTarget = new Vector3(transform.position.x, movableObj.transform.position.y, transform.position.z);
            placeDirection = Vector3.up * Math.Sign(delta.y);
        }
    }

    private Vector2 ComputeDirection(Vector2 currentMove)
    {
        Vector2 result = Vector2.up;
        float current = Vector2.Dot(currentMove, Vector2.up);
        if (current < Vector2.Dot(currentMove, Vector2.down))
        {
            current = Vector2.Dot(currentMove, Vector2.down);
            result = Vector2.down;
        }
        if (current < Vector2.Dot(currentMove, Vector2.left))
        {
            current = Vector2.Dot(currentMove, Vector2.left);
            result = Vector2.left;
        }
        if (current < Vector2.Dot(currentMove, Vector2.right))
        {
            current = Vector2.Dot(currentMove, Vector2.right);
            result = Vector2.right;
        }
        return result;
    }
}
