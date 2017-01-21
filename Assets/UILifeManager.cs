using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifeManager : MonoBehaviour
{

    public GameObject heartPrefab;

    private int maxLife = 3;

    private int currentLife = 3;
    private float xpos = 50f;

    private List<GameObject> heartList = new List<GameObject>();

    // Singleton
    private static UILifeManager _instance;
    public static UILifeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UILifeManager();
            }

            return _instance;
        }
    }

    public void initialize(int life)
    {
        maxLife = life;
        currentLife = life;
        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.SetParent(transform);
            heartList.Add(heart);
        }
    }

    void Start()
    {
        initialize(3);
    }

}
