using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    public Texture2D image;

    public GameObject target;

    public GameObject prefab;
    public float xDistance;
    public float yDistance;
    public float ScalePrefab;

    private Mesh _mesh;
    // Use this for initialization
    void Start () {
        _mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        float _meshH = _mesh.bounds.size.x * ScalePrefab * prefab.transform.localScale.x;
        float _meshW = _mesh.bounds.size.z * ScalePrefab * prefab.transform.localScale.z;
        Debug.Log(_meshH);


        for (int i = 0; i < image.width; i++)
        {
            for(int z = 0; z < image.height; z++)
            {
                if (image.GetPixel(i, z).grayscale < Color.gray.grayscale)
                {
                    GameObject go = Instantiate(prefab, new Vector3(target.transform.position.x + i * (_meshW+xDistance),target.transform.position.y, target.transform.position.z + z * (_meshH +yDistance)),prefab.transform.rotation);
                    go.transform.localScale = prefab.transform.localScale * ScalePrefab;
                    go.transform.SetParent(target.transform);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
