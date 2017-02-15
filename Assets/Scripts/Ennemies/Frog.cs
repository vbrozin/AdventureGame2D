using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour,EnnemieItf {
    int currentLife = 2;

    public int Attack
    {
        get
        {
            return 1;
        }
    }

    public int Life
    {
        get
        {
            return currentLife;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

}
