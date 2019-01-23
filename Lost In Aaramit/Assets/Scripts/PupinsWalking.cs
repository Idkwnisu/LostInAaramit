using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupinsWalking : MonoBehaviour {
    private bool paused = false;
    private Vector3 direction;
    private Vector3 initialPosition;

    private Rigidbody _rb;

    public float width;
    public float height;

    public float maxDistance;

    public float minTime = 0.8f;
    public float maxTime = 2.3f;

    public float speed = 400;
    public float rotationSpeed;

    private Animator _anim;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        StartMove();
        Invoke("Stop", 2.0f);
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (direction == Vector3.zero)
        {
            _rb.velocity = _rb.velocity / 2.0f;
            _anim.SetBool("Walking", false);
        }
        else
        {
            _rb.AddForce(direction * speed * Time.deltaTime);

            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.time);
            _anim.SetBool("Walking", true);

        }
    }

    public void Stop()
    {
        direction = Vector3.zero;
        float randTime = Random.Range(minTime, maxTime);
        Invoke("StartMove", randTime);
    }

    public void StartMove()
    {
        if (Vector3.Distance(transform.position, initialPosition) < maxDistance)
        {
        float randx = Random.Range(-1.0f, 1.0f);
        float randz = Random.Range(-1.0f, 1.0f);
        direction = new Vector3(randx, 0, randz);
        direction = direction.normalized;
        }
        else
        {
            direction = initialPosition - transform.position;
            direction = direction.normalized;
        }
        float randTime = Random.Range(minTime, maxTime);
        Invoke("Stop", randTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        direction = Vector3.zero;
    }
}
