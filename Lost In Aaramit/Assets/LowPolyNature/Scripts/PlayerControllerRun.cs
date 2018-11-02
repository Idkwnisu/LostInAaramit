using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerRun : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private float Speed;

    private bool isRunning;
    #endregion

    #region Public Members

    public float WalkingSpeed = 5.0f;

    public float RunningSpeed = 7.0f;

    [Range(0.1f, 1.0f)]
    public float SpeedSmooth = 0.3f;

    public float RotationSpeed = 240.0f;

    public float JumpSpeed = 7.0f;

    #endregion

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        Speed = WalkingSpeed;
    }

    void FixedUpdate()
    {
    }

    private bool mIsControlEnabled = true;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsControlEnabled)
        {
            if(Input.GetButton("Run"))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
                _animator.SetBool("run", false);
            }

            if(isRunning)
            {
                if(Speed < RunningSpeed)
                {
                    Speed = Mathf.Lerp(Speed, RunningSpeed, SpeedSmooth);
                }
            }
            else
            {
                if(Speed > WalkingSpeed)
                {
                    Speed = Mathf.Lerp(Speed, WalkingSpeed, SpeedSmooth);
                }
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

            if (_characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                if (Input.GetButton("Jump"))
                {
                    _animator.SetBool("is_in_air", true);
                    _moveDirection.y = JumpSpeed;

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

            _moveDirection.y -= Gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
     /*   Debug.Log("W:" + _animator.GetBool("walk"));

        Debug.Log("R:"+_animator.GetBool("run"));

        Debug.Log("IsRunning:" + isRunning);*/

    }
    
  
}
