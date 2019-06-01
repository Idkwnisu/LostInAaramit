using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public float amount = 2.0f;
	public float speedMulti = 0.5f;
	private Vector3 center;
	// Use this for initialization
	void Start () {
		center = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = center + Vector3.up * Mathf.Sin(Time.fixedTime*speedMulti) * amount;
	}
}
