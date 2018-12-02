using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFollow : MonoBehaviour {

    public Transform player;
    public float speed = 300;
    public float chaseDistance = 0.4f;
    public float minDistance = 0.2f;
    public float maxSpeed = 10f;
    [Range(0.01f, 2f)]
    public float lateralSpeedDecrease;
    public float lateralOffset = 0.3f;

    public float stuckRecoveringSpeed = 150; //to check in different ambients, works in maze

    public bool flying = false;

    private Vector3 lastKnownPosition;
    private Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        lastKnownPosition = player.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = player.position - transform.position;
        RaycastHit hit;
        Vector3 offset = Vector3.Cross(direction.normalized, Vector3.up) * lateralOffset;
        Vector3 offsetToApply = Vector3.zero;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.tag);
         if (hit.transform.gameObject.CompareTag("Player"))
         {
             Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.yellow);
             lastKnownPosition = player.position;
         }
        else
        {
            direction = player.position - transform.position - offset;
            if (Physics.Raycast(transform.position + offset, direction, out hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position + offset, direction.normalized * hit.distance, Color.yellow);

                    offsetToApply = offset;
                    lastKnownPosition = player.position;
                }
            }
            direction = player.position - transform.position + offset;
            if (Physics.Raycast(transform.position - offset, direction, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position - offset, direction.normalized * hit.distance, Color.yellow);
                    offsetToApply = (-1)*offset;
                    lastKnownPosition = player.position;
                }
            }
            if (flying)
            {
                Vector3 verticalOffset = Vector3.Cross(direction.normalized, Vector3.right) * lateralOffset;
                direction = player.position - transform.position - verticalOffset;
                if (Physics.Raycast(transform.position + verticalOffset, direction, out hit, Mathf.Infinity))
                {

                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        Debug.DrawRay(transform.position + verticalOffset, direction.normalized * hit.distance, Color.yellow);

                        offsetToApply = offsetToApply + verticalOffset;
                        lastKnownPosition = player.position;
                    }
                }
                direction = player.position - transform.position + verticalOffset;
                if (Physics.Raycast(transform.position - verticalOffset, direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        Debug.DrawRay(transform.position - verticalOffset, direction.normalized * hit.distance, Color.yellow);
                        if(offsetToApply.y == 0) //checking if a vertical offset is already there or not
                            offsetToApply = offsetToApply - verticalOffset;
                        lastKnownPosition = player.position;
                    }
                }

                GoForTargetInAir(offsetToApply, lateralSpeedDecrease);
            }
            else
            {
                    
                    GoForTarget(offsetToApply, lateralSpeedDecrease);
            }
                if (offsetToApply == Vector3.zero)
                {
                    Debug.Log("STUCK");
                    if(!flying)
                    {
                        switchMovement();
                    }
                    _rb.AddForce(Vector3.up * stuckRecoveringSpeed);
                }

            }
        }
        if (flying)
        {
            GoForTargetInAir(lastKnownPosition, 1);
        }
        else
        {
            GoForTarget(lastKnownPosition, 1);
        }
        Vector3 LookAtPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(LookAtPosition);
    }

    public void GoForTarget(Vector3 target, float speedMultiplier)
    {
        Vector3 planeTarget = new Vector3(target.x, 0, target.z);
        Vector3 planePosition = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(planeTarget, planePosition);
        if (distance > chaseDistance)
        {
            Vector3 direction = (planeTarget - planePosition).normalized;
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(direction * speed * speedMultiplier);
        }
        else if (distance < minDistance)
        {
            Vector3 directionF = (planeTarget - planePosition).normalized * (-1);
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(directionF * speed * speedMultiplier);
        }
    }

    public void GoForTargetInAir(Vector3 target, float speedMultiplier)
    {
        float distance = Vector3.Distance(target, transform.position);
        if (distance > chaseDistance)
        {
            Vector3 direction = (target - transform.position).normalized;
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(direction * speed * speedMultiplier);
        }
        else if (distance < minDistance)
        {
            Vector3 directionF = (target - transform.position).normalized * (-1);
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(directionF * speed * speedMultiplier);
        }
    }

    public void switchMovement()
    {
        flying = !flying;
        if(flying)
        {
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }
    }
}
