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

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerControllerRun player = other.GetComponent<PlayerControllerRun>();
            if(audioSource != null)
                audioSource.Play();
            player.ControlDisabling();
            player.resetSpeed();
            player.applyForce(target.position-transform.position, VerticalSpeed);
        }
    }
}
