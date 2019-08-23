using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCloud : MonoBehaviour
{
    [SerializeField] private int goldMax;
    private int goldCurrent;

    private void Awake()
    {
        goldAction = goldMax;
    }

    public int goldAction
    {
        get { return goldCurrent; }
        set
        {
            goldCurrent = value;
            Depleted();
        }
    }

    void Depleted()
    {
        if (goldCurrent <= 0)
            gameObject.tag = "EmptyCloud";
    }

}