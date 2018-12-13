using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNabla : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FallNabla(this.gameObject));
        }
    }

    IEnumerator FallNabla(GameObject nabla)
    {
        yield return new WaitForSeconds(5f);
        nabla.GetComponent<Rigidbody>().isKinematic = false;
    }


}
