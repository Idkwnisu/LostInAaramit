using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBetweenPoints : MonoBehaviour {
    public Transform point1;
    public Transform point2;

    private bool pause = false;


    public float speed;

    private Rigidbody _rb;

    private Transform currentTarget;

	// Use this for initialization
	void Start () {
        currentTarget = point1;
        _rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        if (!pause)
        {
            _rb.MovePosition(transform.position + (currentTarget.position - transform.position).normalized * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentTarget.position) < 0.5)
            {
                if (Object.Equals(currentTarget, point1))
                {
                    currentTarget = point2;
                }
                else
                {
                    currentTarget = point1;
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
             pause = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            pause = false;
    }
}
