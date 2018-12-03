using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerRun : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private Rigidbody _characterController;

    private float Gravity = 1.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private float Speed;

    private bool _isGrounded;

    private bool isRunning;
    #endregion

    #region Public Members

    public Transform feet;

    public float RayLenght = 0.5f;

    public float WalkingForce = 500f;

    public float RunningForce = 700f;

    public float maxWalkingSpeed = 5.0f;

    public float maxRunningSpeed = 7.0f;

    public float RotationSpeed = 240.0f;

    public float JumpSpeed = 7.0f;

    public float FallingGravity = 4.0f;

    [Range(0.01f,0.99f)]
    public float fallingMovement = 0.2f;

    private GameObject currentPlatform;
    private Vector3 _initialPlatformPosition;

    #endregion

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<Rigidbody>();
    }


    private bool mIsControlEnabled = true;

    public void ControlEnabling()
    {
        mIsControlEnabled = true;
    }

    public void ControlDisabling()
    {
        mIsControlEnabled = false;
    }

    // Update is called once per frame
    private void Update()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (_characterController.velocity.y < 0.2)
        {
            Debug.DrawRay(feet.position, transform.TransformDirection(Vector3.down), Color.yellow, 0.0f, true);
            if (Physics.Raycast(feet.position, transform.TransformDirection(Vector3.down), out hit, RayLenght))
            {
                _isGrounded = true;
                
                if (!GameObject.Equals(currentPlatform, hit.transform.gameObject))
                {
                    currentPlatform = hit.transform.gameObject;
                    _initialPlatformPosition = realPosition(hit.transform);
                }


                if (_characterController.velocity.y < -0.05) //is descending
                {
                    if (!mIsControlEnabled)
                    {
                        ControlEnabling();
                        resetSpeed();
                    }
                }
            }
            else
            {
                _isGrounded = false;
            }
        }
        else
        {
            _isGrounded = false;
        }


        if (mIsControlEnabled)
        {
            if (Input.GetButton("Run"))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
                _animator.SetBool("run", false);
            }

            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

            if (_isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                if (isRunning)
                    _moveDirection *= RunningForce;
                else
                    _moveDirection *= WalkingForce;
                
                if (Input.GetButtonDown("Jump"))
                {
                    _animator.SetBool("is_in_air", true);
                    _characterController.AddForce(Vector3.up * JumpSpeed);

                }
                else
                {
                    _animator.SetBool("is_in_air", false);
                    if (isRunning)
                    {
                        _animator.SetBool("run", move.magnitude > 0);
                    }
                    else
                    {
                        _animator.SetBool("walk", move.magnitude > 0);
                    }
                }
            }
            else
            {
                _moveDirection = transform.forward * move.magnitude * WalkingForce * fallingMovement;
            }

            Vector2 horizontalSpeed = new Vector2(_characterController.velocity.x, _characterController.velocity.z);
            if (isRunning == false)
            {
                if (horizontalSpeed.magnitude > maxWalkingSpeed)
                {
                    horizontalSpeed = horizontalSpeed.normalized * maxWalkingSpeed;
                }

            }
            else
            {
                if (horizontalSpeed.magnitude > maxRunningSpeed)
                {
                    horizontalSpeed = horizontalSpeed.normalized * maxRunningSpeed;
                }

            }
            _characterController.velocity = new Vector3(horizontalSpeed.x, _characterController.velocity.y, horizontalSpeed.y);

            _characterController.AddForce(_moveDirection * Time.deltaTime);


            if (_isGrounded)
            {
                if (realPosition(currentPlatform.transform) != _initialPlatformPosition)
                {
                    transform.position += (realPosition(currentPlatform.transform) - _initialPlatformPosition);
                    _initialPlatformPosition = realPosition(currentPlatform.transform);
                }
            }
        }

        if (_characterController.velocity.y < -.5f)
            {
                Gravity = FallingGravity;
            }
            else
            {
                Gravity = 1f;
            }
             _characterController.AddForce(Physics.gravity * Gravity * (_characterController.mass * _characterController.mass));
           
        
     /*   Debug.Log("W:" + _animator.GetBool("walk"));

        Debug.Log("R:"+_animator.GetBool("run"));

        Debug.Log("IsRunning:" + isRunning);*/

    }



    Vector3 realPosition(Transform transform)
    {
        return transform.position;
    }
    
    public void applyForce(Vector3 direction, float force)
    {
        Vector3 versor = direction.normalized;
        _characterController.AddForce(versor * force);
    }

    public void resetSpeed()
    {
        _characterController.velocity = Vector3.zero;   
    }
  
}
