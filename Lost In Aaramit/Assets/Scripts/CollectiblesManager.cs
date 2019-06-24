using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour {

    public static CollectiblesManager instance = null;

    public UpdateAndShowCollectible updater;

    private int collectibles = 0;

    public bool toUpdate = false;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;


        else if (instance != this)
            Destroy(gameObject);


        spendPoints(-10);
        DontDestroyOnLoad(this);
    }

    public void addPoint()
    {
        collectibles += 1;
        Debug.Log("Now you have " + collectibles + " points");
        toUpdate = true;
    }

    public int howManyPoints()
    {
        return collectibles;
    }

    public bool spendPoints(int points)
    {
        
        if (collectibles >= points)
        {
            collectibles -= points;
            toUpdate = true;
            return true;
        }
        return false;
    }
}
