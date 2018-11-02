    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialForestSpawner : MonoBehaviour {
    public GameObject treePrefab;
    public float radius;
    public float radiusAugmentation;
    public float ScalePrefab;
    public float ScaleDifference = 0.2f;
    [Range(0.01f, 1f)]
    public float spaceBetweenPercentage = 0.1f;

    [Range(0.01f, 1f)]
    public float randomPercentage = 0.1f;

    public int PIFraction = 16;

    private Mesh _mesh;

    void Start () {
        _mesh = treePrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        //augment radius
        //augment angle according to radius
        //convert to x,y
        float currentRadius = 0f;
        float currentAngle = 0f;
        float _meshH = _mesh.bounds.size.x * ScalePrefab;
        float _meshW = _mesh.bounds.size.z * ScalePrefab;

        float angleAccumulator = 0.0f; //this is gonna accumulate all angles
        float nextAngleToPosition = 0.0f;

        while (currentRadius < radius)
        {

            currentRadius += radiusAugmentation;
            float ScaleMod = Random.Range(1f - ScaleDifference, 1f + ScaleDifference);
            float H = _meshH * ScaleMod;
            float W = _meshW * ScaleMod;
            ScaleMod = ScalePrefab * ScaleMod;
            currentAngle += Mathf.PI / PIFraction;
            angleAccumulator += Mathf.PI / PIFraction;
            Debug.Log(angleAccumulator);

            // full circle/(how many trees there are in a circonference now)
            // full circle/(circonference/sizeOfTree))
            nextAngleToPosition = Mathf.PI * 2 / ((2 * Mathf.PI * currentRadius) / (H * (1 + spaceBetweenPercentage))); 
            //calculate the next angle in which you position the tree
            //not every round has the same amout of trees, but it must be made with the same amount of steps
            //for bigger forest you need to increase the number of steps since this has the limit to position one tree on every step
            if (angleAccumulator >= nextAngleToPosition)
            {
                float randX = Random.Range((-1) * H * randomPercentage, H * randomPercentage);
                float randZ = Random.Range((-1) * W * randomPercentage, W * randomPercentage);
                float x = currentRadius * Mathf.Cos(currentAngle) + randX;
                float z = currentRadius * Mathf.Sin(currentAngle) + randZ;


                Vector3 position = new Vector3(x, transform.position.y, z);
                GameObject newTree = Instantiate(treePrefab, position, transform.rotation);
                newTree.transform.localScale = new Vector3(ScaleMod, ScaleMod, ScaleMod);
                newTree.transform.parent = gameObject.transform;

                angleAccumulator = 0.0f;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
