using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawner : MonoBehaviour {

    public GameObject treePrefab;
    public float w;
    public float h;
    public float ScalePrefab;
    public float ScaleDifference = 0.2f;
    [Range(0.01f, 1f)]
    public float spaceBetweenPercentage;

    private Mesh _mesh;

	// Use this for initialization
	void Start () {
        _mesh = treePrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        float _meshH = _mesh.bounds.size.x * ScalePrefab;
        float _meshW = _mesh.bounds.size.z * ScalePrefab;
        int dimensionX = (int)Mathf.Floor(w / _meshW);
        int dimensionY = (int)Mathf.Floor(h / _meshH);
        for(int x = 0; x < dimensionX; x++)
        {
            for(int z = 0; z < dimensionY; z++)
            {
                float ScaleMod = Random.Range(1f-ScaleDifference, 1f+ScaleDifference);
                float H = _meshH * ScaleMod;
                float W = _meshW * ScaleMod;
                ScaleMod = ScalePrefab * ScaleMod;
                float randX = Random.Range((-1)*spaceBetweenPercentage*ScaleMod, spaceBetweenPercentage*ScaleMod);
                randX = randX * H + H * spaceBetweenPercentage * x;


                float randZ = Random.Range((-1) * spaceBetweenPercentage*ScaleMod, spaceBetweenPercentage*ScaleMod);
                randZ = randZ * W + W * spaceBetweenPercentage * z;


                Vector3 position = new Vector3(transform.position.x + H * x + randX, transform.position.y, transform.position.z + W * z + randZ);
                GameObject newTree = Instantiate(treePrefab, position, transform.rotation);
                newTree.transform.localScale = new Vector3(ScaleMod, ScaleMod, ScaleMod);
                newTree.transform.parent = gameObject.transform;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
