using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {
    public Transform target;
    public float VerticalSpeed;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        transform.hasChanged = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.hasChanged)
        {
            transform.parent.LookAt(target);
            transform.hasChanged = false;
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerControllerRun player = other.GetComponent<PlayerControllerRun>();
            if (player != null)
            {
                if (audioSource != null)
                    audioSource.Play();
                player.ControlDisabling();
                player.resetSpeed();
                player.applyForce(target.position - transform.position, VerticalSpeed);
            }
            else
            {
                PlayerControllerRunNoFreeCamera p = other.GetComponent<PlayerControllerRunNoFreeCamera>();
                if (audioSource != null)
                    audioSource.Play();
                p.ControlDisabling();
                p.resetSpeed();
                p.applyForce(target.position - transform.position, VerticalSpeed);
            }
            
        }
    }
}
