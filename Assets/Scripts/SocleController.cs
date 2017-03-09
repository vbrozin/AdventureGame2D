using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocleController : MonoBehaviour
{

    private int layermask;

    [SerializeField]
    private GameObject[] interactions;

    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private Sprite inactiveSprite;

    private bool isActive;

    private Coroutine coroutine;

    private bool isCoroutineRunning;

    private int collidingCount = 0;

    void Awake()
    {
        layermask = LayerMask.GetMask("Hero", "Movable", "Ennemie");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (layermask == (layermask | (1 << col.gameObject.layer)))
        {
            collidingCount--;
            if (collidingCount == 0)
            {
                coroutine = StartCoroutine(Fade());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (layermask == (layermask | (1 << col.gameObject.layer)))
        {
            Activate();
            collidingCount++;
            if (coroutine != null && isCoroutineRunning)
            {
                StopCoroutine(coroutine);
                isCoroutineRunning = false;
            }
        }
    }

    private IEnumerator Fade()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.3f);
        Deactivate();
        isCoroutineRunning = false;
    }

    private void Deactivate()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        foreach (GameObject obj in interactions)
        {
            InteractionItf interation = obj.GetComponent<InteractionItf>();
            if (interation != null)
            {
                interation.Deactivate();
            }
            else {
                Debug.Log("Null interaction on object: " + gameObject);
            }
        }
    }

    private void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            GetComponent<SpriteRenderer>().sprite = activeSprite;
            foreach (GameObject obj in interactions)
            {
                InteractionItf interation = obj.GetComponent<InteractionItf>();
                if (interation != null)
                {
                    interation.Activate();
                }
                else {
                    Debug.Log("Null interaction on object: " + gameObject);
                }
            }
        }
    }
}
