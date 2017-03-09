using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour, InteractionItf
{
    [SerializeField]
    private bool vertical;

    [SerializeField]
    private float moveDistance;

    private Vector3 closePosition;

    private Vector3 openPostion;

    private Vector3 target;


    public void Activate()
    {
        target = openPostion;
    }

    public void Deactivate()
    {
        target = closePosition;
    }

    void Start()
    {
        closePosition = transform.position;
        if(vertical)
        {
            openPostion = closePosition + new Vector3(0, moveDistance, 0);
        } else
        {
            openPostion = closePosition + new Vector3(moveDistance, 0, 0);
        }

        target = closePosition;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(target, transform.position) >0.01)
        {
            transform.Translate((target - transform.position )* Time.deltaTime);
        }
    }
}
