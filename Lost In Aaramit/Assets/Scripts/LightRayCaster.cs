using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRayCaster : MonoBehaviour {
    public GameObject ray;

    private GameObject _spawnedRay;

    private GameObject _hitted = null;

    public bool active = true;

    Collider m_Collider;
    // Use this for initialization
    void Start () {
        m_Collider = GetComponent<Collider>();
        _spawnedRay = Instantiate(ray, transform);
        _spawnedRay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (active)
        {
            //you may need to update the ray only when something is moving
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                _spawnedRay.SetActive(true);
                _spawnedRay.transform.localScale = new Vector3(1, hit.distance / 2, 1);
                _spawnedRay.transform.position = transform.position + transform.forward * m_Collider.bounds.size.y * _spawnedRay.transform.localScale.y;

                if (!GameObject.ReferenceEquals(_hitted,hit.transform.gameObject))
                {
                    if (_hitted != null)
                    {
                        if (_hitted.GetComponent<LightTouch>() != null)
                        {
                            
                            _hitted.GetComponent<LightTouch>().lightGone();
                        }
                    }

                    _hitted = hit.transform.gameObject;

                    if (_hitted.GetComponent<LightTouch>() != null)
                    {
                        Debug.Log("Activate");
                        _hitted.GetComponent<LightTouch>().lightHitted();
                    }

                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            }
            else
            {
                _spawnedRay.SetActive(false);
                if (_hitted != null)
                {
                    if(_hitted.GetComponent<LightTouch>() != null)
                        _hitted.GetComponent<LightTouch>().lightGone();
                    _hitted = null;
                }
            }
        }

    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        _spawnedRay.SetActive(false);
        if (_hitted != null)
            if (_hitted.GetComponent<LightTouch>() != null)
            {
                _hitted.GetComponent<LightTouch>().lightGone();
                _hitted = null;
            }
        active = false;
    }
}
