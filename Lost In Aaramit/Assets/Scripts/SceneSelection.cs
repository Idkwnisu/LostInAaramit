using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

    private Scene sceneToDestroy;

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
            //Debug.Log("" + SceneManager.sceneCount);
            sceneToDestroy = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(sceneToDestroy.buildIndex);
            //.Log("" + SceneManager.sceneCount);
            SceneManager.LoadScene("SceneSelectionMenu", LoadSceneMode.Single);
            //Debug.Log("" + SceneManager.sceneCount);
        }

        if (SceneManager.GetActiveScene().name == "SceneSelectionMenu")
        {
            Cursor.visible = true;
        }
    }
}
