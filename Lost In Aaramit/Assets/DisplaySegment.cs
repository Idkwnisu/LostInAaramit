using UnityEngine;

public class DisplaySegment : MonoBehaviour
{
    public int segment;

    public UnityEngine.UI.Button square1;
    public UnityEngine.UI.Button square2;
    public UnityEngine.UI.Button square3;
    public UnityEngine.UI.Button square4;
    public UnityEngine.UI.Button square5;

    public Material lumen;
    public Material normal;

    public MusicController musicController;
    private AudioSource Song;
    public bool display;

    public AudioClip Seg1;
    public AudioClip Seg2;
    public AudioClip Seg3;
    public AudioClip Seg4;
    public AudioClip Seg5;


    // Use this for initialization
    void Start()
    {
        segment = 2;
        display = false;
        Song = musicController.Song;
    }
    	
	// Update is called once per frame
	void Update () 
    {
        if (Song.isPlaying == false && display == true)
        {
            switch (segment)
            {
                case 1:
                    square5.image.color = Color.black;
                    square1.image.color = Color.white;
                    segment = 2;
                    Song.clip = Seg1;
                    break;
                case 2:
                    square1.image.color = Color.black;
                    square2.image.color = Color.white;
                    segment = 3;
                    Song.clip = Seg2;
                    break;
                case 3:
                    square2.image.color = Color.black;
                    square3.image.color = Color.white;
                    segment = 4;
                    Song.clip = Seg3;
                    break;
                case 4:
                    square3.image.color = Color.black;
                    square4.image.color = Color.white;
                    segment = 5;
                    Song.clip = Seg4;
                    break;
                case 5:
                    square4.image.color = Color.black;
                    square5.image.color = Color.white;
                    segment = 1;
                    Song.clip = Seg5;
                    break;
            }
            Song.Play();
        }
    }
}
