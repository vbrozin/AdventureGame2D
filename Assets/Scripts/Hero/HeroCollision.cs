using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollision {

    private HeroManager heroManager;

    private GameObject movable;

    public HeroCollision(HeroManager heroManager)
    {
        this.heroManager = heroManager;
    }

    // On collision
    public void OnCollisionEnter(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ennemie"))
        {
            EnnemieItf ennemie = col.gameObject.GetComponent<EnnemieItf>();
            if(ennemie != null)
            {
                heroManager.LooseLife(ennemie.GetAttack());
            }
        }
    }

    // On collision
    public void OnInteractionEnter(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            movable = col.gameObject;
        }
    }

    // On collision
    public void OnInteractionExit(Collider2D col)
    {
        if (col.gameObject.Equals(movable))
        {
            movable = null;
        }
    }

    public GameObject GetMovable()
    {
        return movable;
    }

}
