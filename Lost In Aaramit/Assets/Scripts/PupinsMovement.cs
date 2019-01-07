using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupinsMovement : MonoBehaviour {
    public Transform[] target;
    public GameObject Pupino;

    public float speed;

    public bool loop;

    private int c = 0;

    private void Start()
    {
        enabled = true;
    }

    private void Update()
    {
        if (Pupino.transform.position != target[c].position)
        {
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
    }
}
