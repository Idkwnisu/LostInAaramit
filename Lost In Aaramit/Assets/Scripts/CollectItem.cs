using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public float speed = 100;

    public AudioClip clip;

    public float efxVolume;

    // Use this for initialization
    void Start()
    {
       if (PlayerPrefs.GetInt("" + gameObject.GetInstanceID()) == 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,speed*Time.deltaTime,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectiblesManager.instance.addPoint();
            AudioManager.instance.PlaySingle(clip, efxVolume);
            PlayerPrefs.SetInt("" + gameObject.GetInstanceID(), 1);
            Destroy(gameObject);
        }
    }
}
