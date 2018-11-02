using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupThings : MonoBehaviour {
    public Text pickupText;
    public Text placeText;
    public Transform hand;

    private Animator _animator;
    private GameObject pickable;
    private GameObject picked;
    private GameObject readyToPlace;


    private bool pickableObjectNear = false;
    private bool objectToPlaceNear = false;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        pickupText.enabled = false;
        placeText.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetButtonDown("Interact"))
        {
            if (pickableObjectNear && picked == null)
            {

                _animator.SetBool("tr_pickup", true);
            }
       
        else if (picked != null)
        {

                if (objectToPlaceNear)
                {
                   
                    readyToPlace.GetComponent<PlacebleItem>().Place(picked);
                    picked = null;
                }
                else
                {
                    _animator.SetBool("tr_drop", true);
                }
            }
        }
        if(picked != null)
        {
            picked.transform.position = hand.position;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (picked == null)
        {
            if (other.CompareTag("PickableObject"))
            {
                if (other.GetComponent<PickableItem>().pickable)
                {
                    pickupText.enabled = true;
                    pickableObjectNear = true;
                    pickable = other.gameObject;
                }
            }
        }
        else
        {
            if(other.CompareTag("PlaceObject"))
            {
                if(other.GetComponent<PlacebleItem>().tag.Equals(picked.GetComponent<PickableItem>().tag))
                {
                    placeText.enabled = true;
                    objectToPlaceNear = true;
                    readyToPlace = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PickableObject"))
        {
            pickupText.enabled = false;
            pickableObjectNear = false;
            pickable = null;
        }
        if(other.CompareTag("PlaceObject"))
        {
            placeText.enabled = false;
            objectToPlaceNear = false;
            readyToPlace = null;
        }
    }

    public void InteractWithItem() //animation over
    {
        _animator.SetBool("tr_pickup", false);
        picked = pickable.GetComponent<PickableItem>().pick();
        pickupText.enabled = false;
        pickableObjectNear = false;
        pickable = null;
    }

    public void DropCurrentItem()
    {
        picked.GetComponent<DroppableItem>().drop();
        picked = null;
    }

}
