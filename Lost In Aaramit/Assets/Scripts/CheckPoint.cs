using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {

    public Transform checkPoint;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            if (this.gameObject.name == "DieFloor_0"){
                SceneManager.LoadScene("GravityFall", LoadSceneMode.Single);
            } else {
                player.GetComponent<PlayerControllerRun>().ControlDisabling();
                player.transform.position = checkPoint.position;
                player.transform.rotation = checkPoint.rotation;
            }
        }
    }
}
