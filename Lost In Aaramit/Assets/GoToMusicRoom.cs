using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToMusicRoom : MonoBehaviour {

	public GameObject Allen;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PlayerPrefs.GetString("yetEnter").Equals("1"))
            {
                PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
                PlayerPrefs.SetString("Saved", "1");
                PlayerPrefs.SetString("yetEnter", "1");
                Allen.GetComponent<PlayerPosition>().position_save();
                SceneManager.LoadScene("NOMESCENAPUZZLE", LoadSceneMode.Single);
            }
        }
    }
}
