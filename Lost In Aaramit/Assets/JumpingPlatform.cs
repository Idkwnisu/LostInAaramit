using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {
    public Transform target;
    public float VerticalSpeed;
    [Range(0.01f,1)]
    public float JumpSpeed;

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
            audioSource.Play();
            other.GetComponent<ParablePlayer>().Jump(target, VerticalSpeed,JumpSpeed);
        }
    }
}
