using UnityEngine;
using System.Collections;

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
    private AudioManager Song;
    public bool display;

    public AudioClip Seg1;
    public AudioClip Seg2;
    public AudioClip Seg3;
    public AudioClip Seg4;
    public AudioClip Seg5;

    private bool pause = false;
    public bool playerIsInteracting = false;


    // Use this for initialization
    void Start()
    {
        segment = 2;
        display = false;
        Song = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //Song = musicController.audioManager;
    }
    	
	// Update is called once per frame
	void Update () 
    {
        if (Song.musicSource.isPlaying == false && display == true && !pause)
        {
            pause = true;
            StartCoroutine(PauseSong());
        }
    }

    IEnumerator PauseSong()
    {
        yield return new WaitForSeconds((float)0.5);
        if (playerIsInteracting)
        {
            switch (segment)
            {
                case 1:
                    square5.image.color = normal.color;
                    square1.image.color = lumen.color;
                    segment = 2;
                    Song.changeMusicSound(Seg1);
                    break;
                case 2:
                    square1.image.color = normal.color;
                    square2.image.color = lumen.color;
                    segment = 3;
                    Song.changeMusicSound(Seg2);
                    break;
                case 3:
                    square2.image.color = normal.color;
                    square3.image.color = lumen.color;
                    segment = 4;
                    Song.changeMusicSound(Seg3);
                    break;
                case 4:
                    square3.image.color = normal.color;
                    square4.image.color = lumen.color;
                    segment = 5;
                    Song.changeMusicSound(Seg4);
                    break;
                case 5:
                    square4.image.color = normal.color;
                    square5.image.color = lumen.color;
                    segment = 1;
                    Song.changeMusicSound(Seg5);
                    break;
                default:
                    break;
            }
        }
        pause = false;
    }
}
