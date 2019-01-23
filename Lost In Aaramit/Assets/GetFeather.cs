using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetFeather : MonoBehaviour {

    public MusicController musicController;
    public Text InteractText;
    public bool interactable = false;
    public GameObject feather;

    public GameObject AM;

    // Use this for initialization
    void Start () {
        InteractText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.V))
        {
            Invoke("LoadMainScene", 2); 
            Destroy(AM);
        }

        if (interactable)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("SONO QUI");
                feather.GetComponent<Renderer>().enabled = false;
                Destroy(AM);
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
            string sceneName = PlayerPrefs.GetString("lastLoadedScene");
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
    }
}
