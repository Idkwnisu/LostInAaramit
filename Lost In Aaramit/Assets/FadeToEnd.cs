using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToEnd : MonoBehaviour {
    private bool fading = false;
    public Image image;
    float fadingSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(fading)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b,image.color.a + fadingSpeed * Time.deltaTime);
        }
	}

    public void StartToFade()
    {
        fading = true;
    }
}
