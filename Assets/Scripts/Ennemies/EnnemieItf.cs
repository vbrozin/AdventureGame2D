using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnnemieItf : MonoBehaviour
{

    protected int Life { get; set; }

    protected int Attack { get; set; }

    public int GetAttack()
    {
        return Attack;
    }

    // On collision
    //void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
    //    {
    //        WeaponItf weapon = col.gameObject.GetComponent<WeaponItf>();
    //        if (weapon.CanHit(gameObject))
    //        {
    //            LooseLife(weapon.GetAttack());
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            WeaponItf weapon = col.gameObject.GetComponent<WeaponItf>();
            if (weapon.CanHit(gameObject))
            {
                LooseLife(weapon.GetAttack());
            }
        }
    }

    private void LooseLife(int loss)
    {
        Life = Life - loss;
        Debug.Log("loose life:" + Life);
        if(Life <= 0)
        {
            Dies();
        }
    }

    private void Dies()
    {
        GameObject.Destroy(gameObject);
    }

}
