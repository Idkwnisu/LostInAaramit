using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBetweenPoints : MonoBehaviour {
    public Transform point1;
    public Transform point2;

    public bool pause = false;


    public float speed;

    private Rigidbody _rb;

    private Animator animator;

    private Transform currentTarget;

    public MusicController musicController;

    public Transform player;

    private Vector3 transf;

	// Use this for initialization
	void Start () {
        currentTarget = point1;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicController._win == true)
        {
            pause = true;
            animator.SetBool("Stopped", true);
            transf.x = player.transform.position.x;
            transf.y = this.transform.position.y;
            transf.z = player.transform.position.z;
            transform.LookAt(transf);
        }
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
                transform.LookAt(currentTarget);

            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pause = true;
            animator.SetBool("Stopped", true);
            if (musicController._win != true)
            {
                transf.x = other.transform.position.x;
                transf.y = this.transform.position.y;
                transf.z = other.transform.position.z;
                transform.LookAt(transf);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pause = false;
            animator.SetBool("Stopped", false);
            transform.LookAt(currentTarget);
        }
    }
}
