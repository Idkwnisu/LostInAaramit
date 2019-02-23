using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour {

    public GameObject Allen;

    public GameObject AM;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PlayerPrefs.GetString("yetEnter").Equals("1")) 
            {
                PlayerPrefs.SetString ("lastLoadedScene", SceneManager.GetActiveScene ().name);
                PlayerPrefs.SetString("yetEnter", "1");
                Allen.GetComponent<PlayerPosition>().position_save();
                //Destroy(AM);
                SceneManager.LoadScene("BossScene", LoadSceneMode.Single);
            }
        }
    }
}
