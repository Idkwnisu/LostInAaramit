using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour {

    private float pX;
    private float pY;
    private float pZ;
    public GameObject Allen;
    // Use this for initialization

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("Prefs cleared");
            PlayerPrefs.DeleteAll();
        }
    }

    void Start()
    {
        pX = PlayerPrefs.GetFloat("p_x");
        pY = PlayerPrefs.GetFloat("p_y");
        pZ = PlayerPrefs.GetFloat("p_z");
        Vector3 pos = new Vector3(pX, pY, pZ); 
        if (PlayerPrefs.GetString("Saved").Equals("1"))
        {
            this.transform.position = pos;
        }
    }

    public void position_save()
    {
        Vector3 pos = Allen.transform.position;
        PlayerPrefs.SetFloat("p_x", pos[0]);
        PlayerPrefs.SetFloat("p_y", pos[1]);
        PlayerPrefs.SetFloat("p_z", pos[2]);
    }

   
 }
