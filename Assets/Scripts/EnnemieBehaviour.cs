using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieBehaviour : MonoBehaviour {

    public List<Vector2> pattern;

    public float detectionRadius = 2f;

    public float speed = 5f;

    private float currentDistance = 0f;

    private Vector2 currentTarget;

    private float valid = 0.1f;

    private GameObject hero = null;

    private IEnumerator<Vector2> enumerator;

    private bool isBumping = false;

    // Use this for initialization
    void Start () {
        // Insert initial position to pattern
       Vector2  initialPosition = gameObject.transform.position;
        pattern.Insert(0, initialPosition);

        // Initialise Enumerator and start to next position
       enumerator = pattern.GetEnumerator();
        enumerator.MoveNext();
       currentTarget = enumerator.Current;
    }

    // Update is called once per frame
    void Update() { 
        if (!isBumping) { 
        if (seeHero())
        {
            currentTarget = hero.transform.position;
            Vector2 delta = currentTarget - new Vector2(transform.position.x, transform.position.y);
            GetComponent<Rigidbody2D>().velocity = delta.normalized * speed;

        }
        else if (pattern.Count > 1)
        {
            Vector2 delta = currentTarget - new Vector2(transform.position.x, transform.position.y);

            if (delta.magnitude < valid)
            {
                if (!enumerator.MoveNext())
                {
                    enumerator.Reset();
                }
                currentTarget = enumerator.Current;
            }


            GetComponent<Rigidbody2D>().velocity = delta.normalized * speed;
        }
    }
	}

    // On collision
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 direction = transform.position - col.transform.position;
        StartCoroutine("Bump",direction);

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {

        }
    }

    private IEnumerator Bump(Vector2 direction)
    {
        isBumping = true;
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed * 0.5f;
        yield return new WaitForSeconds(0.3f);
        isBumping = false;
    }

    private bool seeHero()
    {
        if(!hero)
        {
            RaycastHit2D result = Physics2D.CircleCast(transform.position, detectionRadius, transform.forward,detectionRadius,LayerMask.GetMask("Hero"));
            if (result)
            {
                hero = result.collider.gameObject;
                return true;
            }else
            {
                return false;
            }
        } else
        {
            return true;
        }
    }
}
