using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNabla : MonoBehaviour {

    public bool isFalling = false;

    private GameObject nab;
    public float fallDelay;
    public float reSpawn;
    public bool automaticFall;
    private Vector3 nabPos;
    private Quaternion nabRot;
    private GameObject newNab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && automaticFall)
        {
            Fall();
        }
    }

    public void Fall()
    {
        nab = this.gameObject;
        nabPos = this.gameObject.transform.position;
        nabRot = this.gameObject.transform.rotation;
        StartCoroutine(FallNabla(this.gameObject));
    }

    IEnumerator FallNabla(GameObject nabla)
    {
        yield return new WaitForSeconds(fallDelay);
        nabla.GetComponent<Rigidbody>().isKinematic = false;
        isFalling = true;
        yield return new WaitForSeconds(reSpawn);
		
		Debug.Log("Respawn");
        nab.GetComponent<Rigidbody>().isKinematic = true;
        newNab = Instantiate(nab, nabPos, nabRot) as GameObject;
        Destroy(this.gameObject);

    }


}
