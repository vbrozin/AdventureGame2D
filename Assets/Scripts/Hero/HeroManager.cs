using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour {

    public GameObject weaponPrefab;

    private GameObject weaponInstance;

    private int maxLife = 3;

    private int currentLife = 3;

    private HeroCollision collisionManager;

    // Use this for initialization
    void Start()
    {
        collisionManager = new HeroCollision(this);
        weaponInstance = Instantiate(weaponPrefab, transform, false);
        UILifeManager.Instance.initialize(maxLife);
    }

    // On collision
    void OnCollisionEnter2D(Collision2D col)
    {
        collisionManager.OnCollisionEnter2D(col);
    }
	
	// Update is called once per frame
	void Update () {
		
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
        throw new NotImplementedException();
    }

    public void Hit()
    {
        weaponInstance.GetComponent<Animator>().SetTrigger("Hit");
    }
}
