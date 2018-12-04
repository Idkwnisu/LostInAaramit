using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdjustTrackPitch : MonoBehaviour
{
    public Text InteractText;
    public GameObject rotatorLayout;
    public Camera mainCamera;
    public Camera objectCamera;
    public GameObject Player;
    public AudioClip clip;
    private AudioClip tempClip;
    public float pitch;
    private float tempPitch;

    public GameObject indicator;

    public MusicController musicController;
    private AudioSource Song;

    public AdjustTrackPitch leftController;
    public AdjustTrackPitch rightController;
    private Material tempMaterial;
    private Color tempColor;

    private bool interactable = false;
    private bool interacting = false;

    // Use this for initialization
    void Start()
    {
        InteractText.enabled = false;
        objectCamera.enabled = false;
        rotatorLayout.SetActive(false);
        Song = musicController.Song;
            switch (name)
        {
            case "Cube1":
                clip = musicController.Seg1;
                pitch = musicController.Seg1_pitch;
                break;
            case "Cube2":
                clip = musicController.Seg2;
                pitch = musicController.Seg2_pitch;
                break;
            case "Cube3":
                clip = musicController.Seg3;
                pitch = musicController.Seg3_pitch;
                break;
            case "Cube4":
                clip = musicController.Seg4;
                pitch = musicController.Seg4_pitch;
                break;
            case "Cube5":
                clip = musicController.Seg5;
                pitch = musicController.Seg5_pitch;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable)
        {
            if (Input.GetButtonDown("Interact"))
            {
                //Debug.Log("PREMUTO");

                if (interacting)
                {
                    interacting = false;
                    mainCamera.enabled = true;
                    objectCamera.enabled = false;
                    InteractText.enabled = true;
                    rotatorLayout.SetActive(false);
                    Player.GetComponent<PlayerControllerRun>().ControlEnabling();

                    musicController.PlayerIsInteracting(this, false);
                }
                else
                {
                    interacting = true;
                    mainCamera.enabled = false;
                    objectCamera.enabled = true;
                    InteractText.enabled = false;
                    rotatorLayout.SetActive(true);
                    Player.GetComponent<PlayerControllerRun>().ControlDisabling();

                    musicController.PlayerIsInteracting(this, true);
                }
            }
            if (interacting)
            {
                //Debug.Log("" + clip.name);
                //Debug.Log("" + musicController.returnToFullTrack);

                if (Song.isPlaying == false)
                {
                    Song.pitch = pitch;
                    Song.clip = clip;
                    Song.Play();
                }
                if (Input.GetButtonDown("Vertical") == true)
                {
                    float h = Input.GetAxis("Vertical");
                    if (h > 0)
                    {
                        if (Input.GetButtonDown("Vertical") == true)
                        {
                            Song.pitch += 0.10f;
                            if (Song.pitch > 0.96 && Song.pitch < 1)
                            {
                                Song.pitch = 1.0f;
                                pitch = Song.pitch;
                            }
                            h = 0;
                            Song.pitch = Mathf.Round(Song.pitch * 100f) / 100f;
                            pitch = Song.pitch;
                        }
                    }
                    if (h < 0)
                    {
                        if (Input.GetButtonDown("Vertical") == true)
                        {
                            Song.pitch -= 0.10f;
                            if (Song.pitch > 0.96 && Song.pitch < 1)
                            {
                                Song.pitch = 1.0f;
                            }
                            h = 0;
                            Song.pitch = Mathf.Round(Song.pitch * 100f) / 100f;
                            pitch = Song.pitch;
                        }
                    }


                    else
                    {

                    }
                    //Debug.Log("" + (float)Song.pitch);
                }
                if (Input.GetButtonDown("Horizontal") == true)
                {
                    float h = Input.GetAxis("Horizontal");
                    //Debug.Log("" + (float)Song.pitch);
                    if (h > 0)
                    {
                        if (Input.GetButtonDown("Horizontal") == true)
                        {
                            tempClip = clip;
                            clip = rightController.clip;
                            rightController.clip = tempClip;
                            tempPitch = pitch;
                            pitch = rightController.pitch;
                            rightController.pitch = tempPitch;
                            Song.pitch = pitch;
                            Song.clip = clip;
                            Song.Play();
                            tempMaterial = indicator.GetComponent<Renderer>().material;
                            indicator.GetComponent<Renderer>().material = rightController.indicator.GetComponent<Renderer>().material;
                            rightController.indicator.GetComponent<Renderer>().material = tempMaterial;
                            tempColor = indicator.GetComponent<Light>().color;
                            indicator.GetComponent<Light>().color = rightController.indicator.GetComponent<Light>().color;
                            rightController.indicator.GetComponent<Light>().color = tempColor;
                        }
                        h = 0;
                    }
                    if (h < 0)
                    {
                        if (Input.GetButtonDown("Horizontal") == true)
                        {
                            tempClip = clip;
                            clip = leftController.clip;
                            leftController.clip = tempClip;
                            tempPitch = pitch;
                            pitch = leftController.pitch;
                            leftController.pitch = tempPitch;
                            Song.pitch = pitch;
                            Song.clip = clip;
                            Song.Play();
                            tempMaterial = indicator.GetComponent<Renderer>().material;
                            indicator.GetComponent<Renderer>().material = leftController.indicator.GetComponent<Renderer>().material;
                            leftController.indicator.GetComponent<Renderer>().material = tempMaterial;
                            tempColor = indicator.GetComponent<Light>().color;
                            indicator.GetComponent<Light>().color = leftController.indicator.GetComponent<Light>().color;
                            leftController.indicator.GetComponent<Light>().color = tempColor;
                        }
                        h = 0;
                    }
                }
                else
                {

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractText.enabled = true;
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractText.enabled = false;
            interactable = false;
        }
    }
}
