using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour {
    public Texture2D text;
    public GameObject prefab;
    public float ScalePrefab;
    private Mesh _mesh;
   

    // Use this for initialization
    void Start () {
        _mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        float _meshH = _mesh.bounds.size.x * prefab.transform.localScale.x * ScalePrefab;
        float _meshW = _mesh.bounds.size.z * prefab.transform.localScale.z * ScalePrefab;
        for (int x = 0; x < text.width; x++)
        {
            for (int y = 0; y < text.height; y++)
            {
                if (text.GetPixel(x, y) == Color.black)
                {
                    Vector3 position = new Vector3(transform.position.x + _meshH * x, transform.position.y, transform.position.z + _meshW * y);
                    GameObject cube = Instantiate(prefab, position, transform.rotation);
                    cube.transform.parent = gameObject.transform;
                    cube.transform.localScale = cube.transform.localScale * ScalePrefab;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
