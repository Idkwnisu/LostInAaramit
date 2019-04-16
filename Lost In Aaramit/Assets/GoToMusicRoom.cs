using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToMusicRoom : MonoBehaviour {

	public GameObject Allen;

    public GameObject AM;

    public Transform backPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!(PlayerPrefs.GetInt("Plume") == 1))
            {
                PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
                Allen.GetComponent<PlayerPosition>().position_save(backPoint.transform);
                //Destroy(AM);
                SceneManager.LoadScene("MusicPuzzleNewVersion", LoadSceneMode.Single);
            }
        }
    }
}
