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

    public float RotationsSpeed = 5.0f;

    public float minDistance = 0.3f;
    public float approachingSpeed = 0.4f;

    public float minCamera = -0.4f;
    public float maxCamera = 1.5f;

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

        if (!_isColliding && _cameraProximity < 1.0f)
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

        transform.position = newPos;

        if (h != 0 || v != 0)
        {
            transform.position = newPos;

            transform.RotateAround(PlayerTransform.position, transform.right, v);
            if (transform.rotation.ToEulerAngles().x < minCamera || transform.rotation.ToEulerAngles().x > maxCamera)
            {
                transform.RotateAround(PlayerTransform.position, transform.right, (-1)*v);
            }

            transform.RotateAround(PlayerTransform.position, Vector3.up, h);
            transform.LookAt(PlayerTransform);
            _cameraOffset = (transform.position - PlayerTransform.position)/_cameraProximity;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
             _isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            _isColliding = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_cameraProximity > minDistance)
            _cameraProximity = Mathf.Lerp(_cameraProximity, _cameraProximity - approachingSpeed, SmoothFactor);
    }
}
