using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonsClock : MonoBehaviour {
    enum Season{ AUTUMN, WINTER, SPRING,  SUMMER };

    private Season currentSeason = Season.AUTUMN;

    public Text seasonChangeText;
    private bool nearPlayer = false;


    public GameObject[] toChangeSingle;
    public GameObject[] toChangeChildren;

    public GameObject summer;
    public GameObject spring;
    public GameObject winter;
    public GameObject autumn;

    public Material summer_mat;
    public Material spring_mat;
    public Material winter_mat;
    public Material autumn_mat;

    public Material neutral_mat;

	// Use this for initialization
	void Start () {
        autumn.GetComponent<Renderer>().material = autumn_mat;
        spring.GetComponent<Renderer>().material = neutral_mat;
        winter.GetComponent<Renderer>().material = neutral_mat;
        summer.GetComponent<Renderer>().material = neutral_mat;

        seasonChangeText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (nearPlayer)
        {
            if (Input.GetButtonDown("ChangeSeason"))
            {
                nextSeason();
            }
        }
	}

    public void nextSeason()
    {
        currentSeason += 1;
        if((int)currentSeason > 3)
        {
            currentSeason = Season.AUTUMN;
        }
        switch(currentSeason)
        {
            case Season.AUTUMN:
             
                    autumn.GetComponent<Renderer>().material = autumn_mat;
                    spring.GetComponent<Renderer>().material = neutral_mat;
                    winter.GetComponent<Renderer>().material = neutral_mat;
                    summer.GetComponent<Renderer>().material = neutral_mat;
                break;

            case Season.SUMMER:
                autumn.GetComponent<Renderer>().material = neutral_mat;
                spring.GetComponent<Renderer>().material = neutral_mat;
                winter.GetComponent<Renderer>().material = neutral_mat;
                summer.GetComponent<Renderer>().material = summer_mat;
                break;
            case Season.WINTER:
                autumn.GetComponent<Renderer>().material = neutral_mat;
                spring.GetComponent<Renderer>().material = neutral_mat;
                winter.GetComponent<Renderer>().material = winter_mat;
                summer.GetComponent<Renderer>().material = neutral_mat;
                break;
            case Season.SPRING:
                autumn.GetComponent<Renderer>().material = neutral_mat;
                spring.GetComponent<Renderer>().material = spring_mat;
                winter.GetComponent<Renderer>().material = neutral_mat;
                summer.GetComponent<Renderer>().material = neutral_mat;
                break;
        }

        for(int i = 0; i < toChangeSingle.Length; i++)
        {
            SeasonalChange sc = toChangeSingle[i].GetComponent<SeasonalChange>();
            if (toChangeSingle[i].activeSelf == true)
            {
                
                sc.ChangeSeason((int)currentSeason);
            }
            else
            {
                sc.currentSeason = (int)currentSeason;
            }
        }

        for (int i = 0; i < toChangeChildren.Length; i++)
        {
            foreach(Transform child in toChangeChildren[i].transform)
            {
                SeasonalChange sc = child.GetComponent<SeasonalChange>();

                if (toChangeChildren[i].activeSelf)
                {
                    sc.ChangeSeason((int)currentSeason);
                }
                else
                {
                    sc.currentSeason = (int)currentSeason;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            nearPlayer = true;
            seasonChangeText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearPlayer = false;
            seasonChangeText.enabled = false;
        }
    }
}
