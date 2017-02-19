using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {

    public GameObject weaponPrefab;

    public CharacterController moveController;

    private WeaponItf weaponInstance;

    private int maxLife = 3;

    private int currentLife = 3;

    private HeroCollision collisionManager;


    // Use this for initialization
    void Start()
    {
        collisionManager = new HeroCollision(this);
        weaponInstance = Instantiate(weaponPrefab, transform, false).GetComponent<WeaponItf>();
        moveController = GetComponent<CharacterController>();
        UILifeManager.Instance.initialize(maxLife);
    }

    // Called when box collider hit another collider
    void OnCollisionEnter2D(Collision2D col)
    {
        collisionManager.OnCollisionEnter(col);
    }

    // Called when circle collider hit another collider
    void OnTriggerEnter2D(Collider2D col)
    {
        collisionManager.OnInteractionEnter(col);
    }

    // Called when circle collider hit another collider
    void OnTriggerExit2D(Collider2D col)
    {
        collisionManager.OnInteractionExit(col);
    }

    // Update is called once per frame
    void FixedUpdate () {

        moveController.Move();

        if (Input.GetButton("Attack"))
        {
            Hit();
        }

        if (Input.GetButton("Action"))
        {
            Action();
        } else
        {
            ReleaseObject();
        }
    }

    private void ReleaseObject()
    {
        moveController.StopMoving();
    }

    internal void LooseLife(int attack)
    {
        currentLife -= attack;
        UpdateUI();
        if(currentLife <= 0)
        {
            HeroDies();
        }
    }

    private void UpdateUI()
    {
        UILifeManager.Instance.update(maxLife, currentLife);
    }

    private void HeroDies()
    {
    }

    public void Hit()
    {
        weaponInstance.Hit();
    }

    internal void Action()
    {
        GameObject movable = collisionManager.GetMovable();
        if(movable)
        {
            moveController.StartMoving(movable);
        }
    }

}
