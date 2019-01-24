using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupinsMovement : MonoBehaviour {
    public Transform[] target;
    public GameObject Pupino;

    public float speed;

    public bool loop;

    private int c = 0;

    private Quaternion initialRotation;
    private Animator animator;

    private void Start()
    {
        enabled = false;
        initialRotation = transform.rotation;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Pupino.transform.position != target[c].position)
        {
            if(animator.GetBool("Walking") == false)
            {
                animator.SetBool("Walking", true);
            }
            transform.LookAt(target[c].position);
            Vector3 pos = Vector3.MoveTowards(Pupino.transform.position, target[c].position, speed * Time.deltaTime);
            Pupino.GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            if (c == (target.Length - 1) && !loop)
            {
                setNotActive();
            }
            else
            {
                c = (c + 1) % target.Length;
            }
        }
    }

    public void setActive()
    {
        enabled = true;
    }

    public void setNotActive()
    {
        enabled = false;
        transform.rotation = initialRotation;
        animator.SetBool("Walking", false);
    }
}
