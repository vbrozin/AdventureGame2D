using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILifeManager : MonoBehaviour
{

    public GameObject heartPrefab;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    private int maxLife;

    private int currentLife;

    private List<GameObject> heartList = new List<GameObject>();

    // Singleton
    private static UILifeManager _instance;
    public static UILifeManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void initialize(int life)
    {
        maxLife = life;
        currentLife = life;
        for (int i = 0; i < maxLife; i++)
        {
            AddHeart();
        }
    }

    private void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, transform);
        heartList.Add(heart);
    }

    internal void update(int pMaxLife, int pCurrentLife)
    {
        // Update max life
        if(!maxLife.Equals(pMaxLife))
        {
            // Add max life slot
            for (int i = 0; i < pMaxLife- maxLife; i++)
            {
                AddHeart();
            }
            maxLife = pMaxLife;
            // Restore to full health
            currentLife = pMaxLife;
        }

        // Update current life
        int counter = 0;
        currentLife = pCurrentLife;
        foreach (GameObject heart in heartList)
        {
            counter++;
            if(counter<=currentLife)
            {
                SetFull(heart);
            }else
            {
                SetEmpty(heart);
            }
        }
    }

    private void SetFull(GameObject heart)
    {
        Image image = heart.GetComponent<Image>();
        if (image && image.sprite != fullHeart)
        {
            image.sprite = fullHeart;
        }
    }

    private void SetEmpty(GameObject heart)
    {
        Image image = heart.GetComponent<Image>();
        if (image && image.sprite != emptyHeart)
        {
            image.sprite = emptyHeart;
        }
    }
}
