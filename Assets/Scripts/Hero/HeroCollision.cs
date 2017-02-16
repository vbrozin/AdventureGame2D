using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollision {

    private HeroManager heroManager;

    public HeroCollision(HeroManager heroManager)
    {
        this.heroManager = heroManager;
    }

    // On collision
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Ennemie"))
        {
            EnnemieItf ennemie = col.gameObject.GetComponent<EnnemieItf>();
            if(ennemie != null)
            {
                heroManager.LooseLife(ennemie.GetAttack());
            }
        }
    }
}
