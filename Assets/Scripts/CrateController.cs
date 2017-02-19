using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 3f;
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 currentMove = GetComponent<Rigidbody2D>().velocity;
        if (currentMove.magnitude > maxSpeed)
        {
            Vector2 direction = ComputeDirection(currentMove);
            GetComponent<Rigidbody2D>().velocity = direction * maxSpeed;
        }

    }

    private Vector2 ComputeDirection(Vector2 currentMove)
    {
        Vector2 result = Vector2.up;
        float current = Vector2.Dot(currentMove, Vector2.up);
        if(current < Vector2.Dot(currentMove, Vector2.down))
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

    // On collision Exit
    void OnCollisionExit2D(Collision2D col)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
