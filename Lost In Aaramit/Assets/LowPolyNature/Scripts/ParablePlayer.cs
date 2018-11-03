using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParablePlayer : MonoBehaviour {

    [HideInInspector]
    public Transform target; //using only horizzontal plan stuff;

    private float VerticalSpeed;

    private float maxVerticalSpeed;

    [HideInInspector]
    public float JumpSpeed;

    [Range(0.01f, 1)]
    public float VerticalDecay;

    private Vector3 HorizontalMovement;
    private float VerticalDepletion;

    [Range(0.01f, 1)]
    public float JumpForce;

    public float targetDistance = 0.5f;

    private bool isJumping = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isJumping)
        {
            /*   transform.position = Vector3.Lerp(transform.position, target.position, JumpSpeed);
               

               ;*/
            /*     Vector3 newPosition = Vector3.Lerp(transform.position, target.position, JumpSpeed);
                  newPosition += new Vector3(0, VerticalSpeed, 0);
              Vector3 diff = newPosition - transform.position;
              transform.position += diff * Time.deltaTime;

              JumpSpeed += JumpSpeed * JumpForce * Time.deltaTime;*/
            transform.position += HorizontalMovement * Time.deltaTime;
            transform.position += new Vector3(0, VerticalSpeed * Time.deltaTime, 0);
            VerticalSpeed -= Mathf.Clamp(VerticalDepletion * Time.deltaTime,(-1)*maxVerticalSpeed,maxVerticalSpeed);

            if(Vector3.Distance(transform.position, target.position) < targetDistance)
            {
                isJumping = false;
                GetComponent<PlayerControllerRun>().EnableControl();
            }

        }
	}

    public void Jump(Transform target, float speed, float JumpSpeed)
    {
        GetComponent<PlayerControllerRun>().DisableControl();
        this.target = target;
        VerticalSpeed = speed;
        maxVerticalSpeed = speed;
        this.JumpSpeed = JumpSpeed;
        HorizontalMovement = (target.position - transform.position) * JumpForce;

        float TimeToComplete = Vector3.Distance(transform.position, target.position) / HorizontalMovement.magnitude;
        VerticalDepletion = VerticalSpeed*2 / TimeToComplete;
        
        isJumping = true;
    }
}
