using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

    private Scene sceneToDestroy;
    public Image canvasImage;
    public Sprite menuImage;
    public Sprite creditsImage;

    public GameObject newGame;
    public GameObject loadGame;
    public GameObject credits;
    public GameObject exit;
    public GameObject back;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        back.active = false;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectScene(string scene)
    {
        if (scene == "Exit")
        {
            Application.Quit();
        }
        else if (scene == "Credits") 
        {
            credits.active = false;
            newGame.active = false;
            loadGame.active = false;
            exit.active = false;
            back.active = true;
            canvasImage.transform.GetComponent<Image>().sprite = creditsImage;
        }
        else if (scene == "Back")
        {
            credits.active = true;
            newGame.active = true;
            loadGame.active = true;
            exit.active = true;
            back.active = false;
            canvasImage.transform.GetComponent<Image>().sprite = menuImage;
        }
        else if (scene == "Load Game")
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
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

        else
        {
            Cursor.visible = false;
        }
    }
}
