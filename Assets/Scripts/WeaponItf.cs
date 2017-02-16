using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItf : MonoBehaviour
{
    protected int Attack { get; set; }

    private float animationDuration = 0.5f;

    private List<GameObject> alreadyHit = new List<GameObject>();

    public int GetAttack()
    {
        return Attack;
    }

    virtual public void Hit()
    {
        if(!IsActive())
        {
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Animator>().SetTrigger("Hit");
            StartCoroutine(CountDown());
        }
    }

    public bool CanHit(GameObject pObject)
    {
        if (IsActive() && !alreadyHit.Contains(pObject))
        {
            alreadyHit.Add(pObject);
            return true;
        }
        return false;
    }

    private bool IsActive()
    {
        bool result = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hit");
        if(result)
        {
            animationDuration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        return result;
    }

    private IEnumerator CountDown()
    {
        Debug.Log("Hit time: " + animationDuration);
        yield return new WaitForSeconds(animationDuration);
        alreadyHit.Clear();
        GetComponent<Collider2D>().isTrigger = false;
    }

}
