using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPuzzle : MonoBehaviour {

    public MusicController musicController;
    public AudioClip village;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        LoadMainScene();
    }
    private void LoadMainScene()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        musicController.audioManager.changeMusicSound(village);
        musicController.audioManager.musicSource.loop = true;
        SceneManager.LoadScene(sceneName);
    }
}
