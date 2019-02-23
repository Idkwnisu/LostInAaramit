using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private Scene sceneToDestroy;

    public GameObject resume;
    public GameObject options;
    public GameObject backToMainMenu;
    public GameObject exit;
    public GameObject canvas;
    public GameObject mainButtons;
    public GameObject audioButtons;
    public GameObject player;
    public AudioManager audioManager;

    public Slider musicSlider;
    public Slider soundEffectsSlider;

    private bool soundEffects = true;
    private bool music = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        canvas.active = false;
    }

    public void SelectButton(string button)
    {
        switch (button)
        {
            case "Resume":
                {
                    canvas.active = false;
                    Cursor.visible = false;
                    break;
                }
            case "Options":
                {
                    mainButtons.active = false;
                    audioButtons.active = true;
                    break;
                }
            case "Back To Main Menu":
                {
                    //Debug.Log("" + SceneManager.sceneCount);
                    sceneToDestroy = SceneManager.GetActiveScene();
                    SceneManager.UnloadSceneAsync(sceneToDestroy.buildIndex);
                    //.Log("" + SceneManager.sceneCount);
                    SceneManager.LoadScene("SceneSelectionMenu", LoadSceneMode.Single);
                    //Debug.Log("" + SceneManager.sceneCount);
                    audioManager.DestroyAudioManager();
                    Destroy(gameObject);
                    break;
                }
            case "Exit":
                {
                    Application.Quit();
                    break;
                }
            case "Toggle Music":
                {
                    if (music)
                    {
                        audioManager.musicSource.enabled = false;
                        music = false;
                    }

                    else
                    {
                        audioManager.musicSource.enabled = true;
                        music = true;
                    }

                    break;
                }
            case "Music Slider":
                {
                    audioManager.musicSource.volume += ((musicSlider.value / 100) - audioManager.musicSource.volume);
                    break;
                }
            case "Toggle Sound Effects":
                {
                    if (soundEffects)
                    {
                        audioManager.efxSource.enabled = false;
                        soundEffects = false;
                    }

                    else
                    {
                        audioManager.efxSource.enabled = true;
                        soundEffects = true;
                    }
                        
                    break;
                }
            case "Sound Effects Slider":
                {
                    audioManager.efxVolumeMultiplier = soundEffectsSlider.value / 100 * 2;
                    break;
                }
            case "Back":
                {
                    audioButtons.active = false;
                    mainButtons.active = true;
                    break;
                }
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && canvas.active == false)
        {
            canvas.active = true;
            mainButtons.active = true;
            audioButtons.active = false;
            Cursor.visible = true;
            ControlManager.instance.getCurrentCamera().GetComponent<PlayerFollow>().cameraActive = false;
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (player.GetComponent<PlayerControllerRun>() != null)
                {
                    player.GetComponent<PlayerControllerRun>().resetSpeed();
                    player.GetComponent<PlayerControllerRun>().Interacting();
                }
                else
                {
                    player.GetComponent<PlayerControllerRunNoFreeCamera>().resetSpeed();
                    player.GetComponent<PlayerControllerRunNoFreeCamera>().Interacting();
                }
            }
        }

        else if (Input.GetButtonDown("Cancel") && canvas.active == true)
        {
            canvas.active = false;
            Cursor.visible = false;
            ControlManager.instance.getCurrentCamera().GetComponent<PlayerFollow>().cameraActive = true;
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (player.GetComponent<PlayerControllerRun>() != null)
                {
                    player.GetComponent<PlayerControllerRun>().NonInteracting();
                }
                else
                {
                    player.GetComponent<PlayerControllerRunNoFreeCamera>().NonInteracting();
                }
            }
        }
    }
}
