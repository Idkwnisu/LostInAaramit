using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("SceneSelectionMenu", LoadSceneMode.Single);
        }
    }
}
