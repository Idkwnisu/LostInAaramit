using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{

    public Transform checkPoint;
    public GameObject player;
    public AudioClip playerDie;
    public float efxVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name == "DieFloor_0")
            {
                SceneManager.LoadScene("GravityFall", LoadSceneMode.Single);
            }
            else
            {
                StartCoroutine(ReSpawn());
            }
        }
    }

    IEnumerator ReSpawn()
    {
        AudioManager.instance.PlaySingle(playerDie, efxVolume);
        yield return new WaitForSeconds(0.2f);
        player.transform.position = checkPoint.position;
        player.transform.rotation = checkPoint.rotation;
    }
}
