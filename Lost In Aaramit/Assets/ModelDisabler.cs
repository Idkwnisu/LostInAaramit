using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDisabler : MonoBehaviour {
    public GameObject[] toDisable;
    public float distance;
    public GameObject player;
	// Use this for initialization
	void Start () {
        Invoke("Check", 2.0f);
	}

    private void Check()
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
			Debug.Log(player.transform.position);
            //Debug.Log(Vector3.Distance(player.transform.position, toDisable[i].transform.position));
            if (Vector3.Distance(player.transform.position, toDisable[i].transform.position) > distance)
            {
                toDisable[i].SetActive(false);
            }
            else
            {
                toDisable[i].SetActive(true);
            }
        }
        Invoke("Check", 0.5f);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
