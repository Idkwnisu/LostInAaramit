using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNote : MonoBehaviour {

    private GameObject platNoteTriggered;

    public AudioClip noteClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log(this.gameObject.name);
            transform.parent.GetComponent<BossFight>().UpdateLevel(this.gameObject);
        }
    }

    public AudioClip GetAudioClip(){
        return noteClip;
    }

}
