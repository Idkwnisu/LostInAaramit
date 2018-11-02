using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalChange : MonoBehaviour {
    enum Season { AUTUMN, WINTER, SPRING, SUMMER };

    [HideInInspector]
    public int currentSeason;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChangeSeason(int newSeason)
    {
        
    }
}
