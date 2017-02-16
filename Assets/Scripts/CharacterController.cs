using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private HeroManager heroManager;

    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.

    // Use this for initialization
    void Start () {
        heroManager = GetComponent<HeroManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v);

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (move.magnitude != 0)
        {
            if (move.magnitude * GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(move * moveForce);

            if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
                GetComponent<Rigidbody2D>().velocity = move * maxSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // If the input is moving the player right and the player is facing left...
        Flip(move);

        if (Input.GetKey(KeyCode.Space))
        {
            heroManager.Hit();
        }

    }

    private void Flip(Vector2 move)
    {
        //TODO
    }
}
