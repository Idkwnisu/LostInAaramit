using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    private float _cameraProximity = 1f;
    private bool _isColliding = false;
    private float _maxDistance;

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public bool RotateMiddleMouseButton = true;

    public float RotationsSpeed = 5.0f;

    public float CameraPitchMin = 1.5f;

    public float CameraPitchMax = 6.5f;

    public float minDistance = 0.3f;
    public float approachingSpeed = 0.4f;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
        _maxDistance = Vector3.Distance(transform.position, PlayerTransform.position);
        Cursor.visible = false;
    }



    // LateUpdate is called after Update methods
    void LateUpdate()
    {


            float h = Input.GetAxis("Mouse X") * RotationsSpeed;
            float v = Input.GetAxis("Mouse Y") * RotationsSpeed * -1;

            Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

            Quaternion camTurnAngleY = Quaternion.AngleAxis(v, transform.right);

            Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

            // Limit camera pitch
            if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
            {
                newCameraOffset = camTurnAngle * _cameraOffset;
            }

            _cameraOffset = newCameraOffset;

            if(!_isColliding && _cameraProximity < 1.0f)
            {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            Vector3 direction = _cameraOffset.normalized;
            if (!Physics.Raycast(transform.position, direction, out hit, _maxDistance))
            {
                _cameraProximity = Mathf.Lerp(_cameraProximity, _cameraProximity + approachingSpeed, SmoothFactor);
                if (_cameraProximity > 1)
                    _cameraProximity = 1;
            }
        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset * _cameraProximity;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        _isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isColliding = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_cameraProximity > minDistance)
            _cameraProximity = Mathf.Lerp(_cameraProximity, _cameraProximity - approachingSpeed, SmoothFactor);
    }
}
