﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetFeather : MonoBehaviour {

    public MusicController musicController;
    public Text InteractText;
    public bool interactable = false;
    public GameObject feather;

    // Use this for initialization
    void Start () {
        InteractText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (interactable)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("SONO QUI");
                feather.GetComponent<Renderer>().enabled = false;
                Invoke("LoadMainScene", 2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && musicController._win==true)
        {
            InteractText.enabled = true;
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && musicController._win == true)
        {
            InteractText.enabled = false;
            interactable = false;
        }
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("BossScene", LoadSceneMode.Single);
    }
}
