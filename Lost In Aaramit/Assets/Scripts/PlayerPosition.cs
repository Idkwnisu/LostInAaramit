using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour {

    private float pX;
    private float pY;
    private float pZ;
    public GameObject Allen;
    public GameObject Kitchi;
    // Use this for initialization

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
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
        Vector3 kitchiOffset = Kitchi.transform.position - this.transform.position;
        if (PlayerPrefs.GetString("Saved").Equals("1"))
        {
            Debug.Log("Cambia Posizione");
            this.transform.position = pos;
            Kitchi.transform.position = pos + kitchiOffset;
        }
    }

    public void position_save()
    {
        Debug.Log("Salva Posizione");
        PlayerPrefs.SetString("Saved", "1");
        Vector3 pos = Allen.transform.position;
        PlayerPrefs.SetFloat("p_x", pos[0]);
        PlayerPrefs.SetFloat("p_y", pos[1]);
        PlayerPrefs.SetFloat("p_z", pos[2]);
    }


    public void position_save(Transform t)
    {
        Debug.Log("Salva Posizione");
        PlayerPrefs.SetString("Saved", "1");
        Vector3 pos = t.position;
        PlayerPrefs.SetFloat("p_x", pos[0]);
        PlayerPrefs.SetFloat("p_y", pos[1]);
        PlayerPrefs.SetFloat("p_z", pos[2]);
    }

   
 }
