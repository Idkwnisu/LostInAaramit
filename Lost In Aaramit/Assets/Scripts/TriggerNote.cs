using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNote : MonoBehaviour {

    private GameObject platNoteTriggered;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.name);
            transform.parent.GetComponent<BossFight>().UpdateLevel(this.gameObject);
        }
    }

}
