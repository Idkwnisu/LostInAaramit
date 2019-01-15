using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFollow : MonoBehaviour {

    public Transform player;
    public float speed = 300;
    public float retreatSpeed = 500;
    public float chaseDistance = 0.4f;
    public float minDistance = 0.2f;
    public float maxSpeed = 10f;
    [Range(0.01f, 10f)]
    public float lateralSpeedDecrease;
    public float lateralOffset = 0.3f;

    public float FlyRayCheck = 0.3f;
    public float LandRayCheck = 1.0f;
    public float chaseSmooth = 0.2f;
    public int frameSkip = 3;
    public Transform feet;

    public float stuckRecoveringSpeed = 150; //to check in different ambients, works in maze

    public bool flying = false;

    public bool landing = true;

    public float landingDistance = 3.0f;

    private Vector3 lastKnownPosition;
    private Rigidbody _rb;
    private Animator _animator;

    private Vector3 currentOffset;
    private int i = 0;


    PlayerControllerRun playerCR;
    PlayerControllerRunNoFreeCamera playerCRFC;
    PlayerControllerRunJoypad playerCRC;

    private Vector3 oldVelocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        lastKnownPosition = player.position;
        _animator = GetComponent<Animator>();

         playerCR = player.parent.GetComponent<PlayerControllerRun>();
         playerCRFC = player.parent.GetComponent<PlayerControllerRunNoFreeCamera>();
         playerCRC = player.parent.GetComponent<PlayerControllerRunJoypad>();
    }

    // Update is called once per frame
    void Update() {
        i++;
        if (i >= frameSkip)
        {
            bool controlsEnabled = false;
            if (playerCR.enabled)
            {
                controlsEnabled = playerCR.isControlEnabled();
            }
            if (playerCRFC.enabled)
            {
                controlsEnabled = playerCRFC.isControlEnabled();

            }
            if (playerCRC.enabled)
            {
                controlsEnabled = playerCRC.isControlEnabled();
            }

            if (controlsEnabled)
            {
                if (!flying)
                {
                    RaycastHit hitGround;
                    // Does the ray intersect any objects excluding the player layer

                    Debug.DrawRay(feet.position, transform.TransformDirection(Vector3.down) * FlyRayCheck, Color.red, 0.0f, true);
                    if (!Physics.Raycast(feet.position, transform.TransformDirection(Vector3.down), out hitGround, FlyRayCheck))
                    {
                        switchMovement();
                    }
                }
                else
                {
                    if (Vector3.Distance(player.position, transform.position) < landingDistance)
                    {
                        RaycastHit hitGround;
                        // Does the ray intersect any objects excluding the player layer

                        Debug.DrawRay(feet.position, transform.TransformDirection(Vector3.down) * LandRayCheck, Color.blue, 0.0f, true);
                        if (Physics.Raycast(feet.position, transform.TransformDirection(Vector3.down), out hitGround, LandRayCheck))
                        {
                            switchMovement();
                        }
                    }
                }
                Vector3 direction = player.position - transform.position;
                RaycastHit hit;
                Vector3 offset = Vector3.Cross(direction.normalized, Vector3.up) * lateralOffset;
                Vector3 offsetToApply = Vector3.zero;
                // Does the ray intersect any objects excluding the player layer
                Vector3 start = transform.position;
                if (!flying)
                {
                    start = feet.position;
                    //direction = new Vector3(direction.x, 0, direction.y);
                }
                if (Physics.Raycast(start, direction, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(start, direction.normalized * hit.distance, Color.cyan);
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Update");
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
                                offsetToApply = (-1) * offset;
                                lastKnownPosition = player.position;
                            }
                        }

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
                                if (offsetToApply.y == 0) //checking if a vertical offset is already there or not
                                    offsetToApply = offsetToApply - verticalOffset;
                                lastKnownPosition = player.position;
                            }
                        }
                        Debug.DrawRay(transform.position, offsetToApply * 10, Color.red);
                        /* if(offsetToApply.y != 0)
                         {
                             if(!flying)
                             {
                                 Debug.Log("FLY");

                                 switchMovement();
                             }
                         }*/
                        if (flying)
                        {
                            GoForTargetInAir(offsetToApply, lateralSpeedDecrease);
                        }
                        else
                        {
                            GoForTarget(offsetToApply, lateralSpeedDecrease);
                        }
                        // Debug.Log(offsetToApply);

                        if (offsetToApply == Vector3.zero)
                        {
                            //Debug.Log("STUCK");
                            if (!flying)
                            {

                                switchMovement();
                            }
                            _rb.AddForce(Vector3.up * stuckRecoveringSpeed * Time.deltaTime);
                        }

                    }
                }

                if (flying)
                {
                    //GoForTargetInAir(lastKnownPosition, 1);
                    GoForTargetInAir(player.position, 1);
                }
                else
                {
                    // GoForTarget(lastKnownPosition, 1);
                    GoForTarget(player.position, 1);
                }
                Vector3 LookAtPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(LookAtPosition);

                if (_rb.velocity.magnitude < 0.3)
                {

                    if (!flying)
                    {
                        _animator.SetBool("isWalking", false);
                        _animator.SetBool("isFlying", false);
                    }
                    else
                    {
                        _animator.SetBool("isWalking", false);
                        _animator.SetBool("isFlying", true);
                    }
                }
                else
                {
                    if (!flying)
                    {
                        _animator.SetBool("isWalking", true);
                        _animator.SetBool("isFlying", false);
                    }
                    else
                    {
                        _animator.SetBool("isWalking", false);
                        _animator.SetBool("isFlying", true);
                    }
                }
                currentOffset = transform.position - player.position;
            }
            else
            {
                Vector3 planeOffset = new Vector3(currentOffset.x, 0, currentOffset.z);
                _rb.MovePosition(player.transform.position + planeOffset);
                if (!flying)
                {
                    flying = true;
                    _animator.SetBool("isWalking", false);
                    _animator.SetBool("isFlying", true);
                }
            }
            i = 0;
        }
    }

    public void GoForTarget(Vector3 target, float speedMultiplier)
    {
        Vector3 planeTarget = new Vector3(target.x, 0, target.z);
        Vector3 planePosition = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(planeTarget, planePosition);
        float rand = Random.value * chaseSmooth;
        if (distance > chaseDistance + rand)
        {
            Vector3 direction = (planeTarget - planePosition).normalized;
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(direction * speed * speedMultiplier * Time.deltaTime);
        }
        else if (distance < minDistance)
        {
            Vector3 directionF = (planeTarget - planePosition).normalized * (-1);
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(directionF * retreatSpeed * speedMultiplier * Time.deltaTime);
        }
        
    }

    public void GoForTargetInAir(Vector3 target, float speedMultiplier)
    {
        Vector3 planeTarget = new Vector3(target.x, 0, target.z);
        Vector3 planePosition = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(target, transform.position);
        float planeDistance = Vector3.Distance(planeTarget, planePosition);
        float rand = Random.value * chaseSmooth;
        if (distance > chaseDistance + rand) { 
            Vector3 direction = (target - transform.position).normalized;
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(direction * speed * speedMultiplier * Time.deltaTime);
        }
        else if (distance < minDistance)
        {
            Vector3 directionF = (target - transform.position).normalized * (-1);
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(directionF * retreatSpeed * speedMultiplier * Time.deltaTime);
        }
        else if(planeDistance < minDistance)
        {
            Vector3 directionF = (planeTarget - planePosition).normalized * (-1);
            if (_rb.velocity.magnitude < maxSpeed)
                _rb.AddForce(directionF * retreatSpeed * speedMultiplier * Time.deltaTime);
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
