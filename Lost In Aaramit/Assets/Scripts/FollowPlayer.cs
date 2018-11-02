using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform Player;
    public float Smooth = 0.4f;
    public float MaxDistance = 2.0f;
    
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) > MaxDistance)
        {
            transform.position = Vector3.Slerp(transform.position, Player.position, Smooth);

        }
    }
}
