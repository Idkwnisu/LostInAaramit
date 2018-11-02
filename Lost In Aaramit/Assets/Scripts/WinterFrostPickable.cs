using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterFrostPickable : SeasonalChange
{
    public GameObject frozen;
    public GameObject defrosted;
    private bool _toUpdate = false;

    enum Season { AUTUMN, WINTER, SPRING, SUMMER };
    // Use this for initialization
    void Start () {
        defrosted.active = true;
        frozen.active = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void LateUpdate()
    {
        if(_toUpdate)
        {
            ChangeSeason(currentSeason);
        }
    }

    public override void ChangeSeason(int newSeason)
    {
        currentSeason = newSeason;
        if (currentSeason == (int)Season.WINTER)
        {
            gameObject.GetComponent<PickableItem>().pickable = false;
            defrosted.active = false;
            frozen.active = true;
        }
        else
        {
            gameObject.GetComponent<PickableItem>().pickable = true;

            defrosted.active = true;
            frozen.active = false;
        }
        
    }

    public void OnEnable()
    {
        _toUpdate = true;
    }
}
