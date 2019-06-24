using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAndShowCollectible : MonoBehaviour {

	public GameObject toActivate;
	public Text text;

	public float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CollectiblesManager.instance.toUpdate)
		{
			UpdateCollectibles();
			CollectiblesManager.instance.toUpdate = false;
		}
	}

	public void UpdateCollectibles()
	{
		toActivate.SetActive(true);
		text.text = ""+CollectiblesManager.instance.howManyPoints();
		
		Invoke("Deactivate",time);
	}

	public void Deactivate()
	{
		toActivate.SetActive(false);
	}
}
