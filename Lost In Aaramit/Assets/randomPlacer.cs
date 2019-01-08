using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlacer : MonoBehaviour {
    public Transform center;
    public Transform ray;

    public GameObject father;
    public GameObject[] bases;

    public int num;
	// Use this for initialization
	void Start () {
        float distance = Vector3.Distance(center.position, ray.position);
        for(int i = 0; i < num; i++)
        {
            float randAngle = Random.Range(0, 360);
            float randRadius = Random.Range(0, distance);
            randAngle = randAngle * Mathf.Deg2Rad;
            Vector3 position = new Vector3(center.position.x + Mathf.Sin(randAngle)*randRadius, center.position.y, center.position.z + Mathf.Cos(randAngle)* randRadius);
            GameObject randBase = bases[Random.Range(0, bases.Length)];
            GameObject go = Instantiate(randBase, position, father.transform.rotation);
            go.transform.SetParent(father.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
