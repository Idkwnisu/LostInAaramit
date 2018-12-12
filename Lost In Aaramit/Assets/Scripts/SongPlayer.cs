using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SongPlayer: MonoBehaviour
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
    public bool simplePlayer;

    public GameObject indicator;
    public GameObject pitchIndicator;

    public MusicController musicController;
    private AudioSource Song;

    public SongPlayer leftController;
    public SongPlayer rightController;
    private Material tempMaterial;
    private Color tempColor;
    private Vector3 tempScale;

    private bool interactable = false;
    private bool interacting = false;
    public bool correctPitch;
    public bool correctReordering;
    private bool tempBool;
    private string targetClipName;

    // Use this for initialization
    void Start()
    {
        InteractText.enabled = false;
        objectCamera.enabled = false;
        rotatorLayout.SetActive(false);
        Song = musicController.Song;
        correctPitch = false;
        correctReordering = false;
            switch (name)
        {
            case "Cube1":
                clip = musicController.Seg1;
                pitch = musicController.Seg1_pitch;
                simplePlayer = false;
                targetClipName = "Seg1";
                break;
            case "Cube2":
                clip = musicController.Seg2;
                pitch = musicController.Seg2_pitch;
                simplePlayer = false;
                targetClipName = "Seg2";
                break;
            case "Cube3":
                clip = musicController.Seg3;
                pitch = musicController.Seg3_pitch;
                simplePlayer = false;
                targetClipName = "Seg3";
                break;
            case "Cube4":
                clip = musicController.Seg4;
                pitch = musicController.Seg4_pitch;
                simplePlayer = false;
                targetClipName = "Seg4";
                break;
            case "Cube5":
                clip = musicController.Seg5;
                pitch = musicController.Seg5_pitch;
                simplePlayer = false;
                targetClipName = "Seg5";
                break;
            case "Cube6":
                pitch = 1.0f;
                simplePlayer = true;
                targetClipName = "None";
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
                    if (simplePlayer == false)
                    {
                        rotatorLayout.SetActive(true);
                    }
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
                if (Input.GetButtonDown("Vertical") == true && simplePlayer == false && musicController.scene != 2)
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
                            pitchIndicator.transform.localScale += new Vector3(0.0F, 0.1F, 0.0F);
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
                            pitchIndicator.transform.localScale -= new Vector3(0.0F, 0.1F, 0.0F);
                        }
                    }
                    h = 0;
                    Song.pitch = Mathf.Round(Song.pitch * 100f) / 100f;
                    pitch = Song.pitch;
                    CheckPitch();
                    //Debug.Log("" + (float)Song.pitch);
                }
                if (Input.GetButtonDown("Horizontal") == true && simplePlayer == false && musicController.scene != 1)
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
                            pitchIndicator.GetComponent<Renderer>().material = rightController.indicator.GetComponent<Renderer>().material;
                            rightController.indicator.GetComponent<Renderer>().material = tempMaterial;
                            rightController.pitchIndicator.GetComponent<Renderer>().material = tempMaterial;
                            tempColor = indicator.GetComponent<Light>().color;
                            indicator.GetComponent<Light>().color = rightController.indicator.GetComponent<Light>().color;
                            pitchIndicator.GetComponent<Light>().color = rightController.indicator.GetComponent<Light>().color;
                            rightController.indicator.GetComponent<Light>().color = tempColor;
                            rightController.pitchIndicator.GetComponent<Light>().color = tempColor;
                            tempScale = pitchIndicator.transform.localScale;
                            pitchIndicator.transform.localScale = rightController.pitchIndicator.transform.localScale;
                            rightController.pitchIndicator.transform.localScale = tempScale;
                            tempBool = correctPitch;
                            correctPitch = rightController.correctPitch;
                            rightController.correctPitch = tempBool;
                            tempBool = correctReordering;
                            correctReordering = rightController.correctReordering;
                            rightController.correctReordering = tempBool;
                            rightController.CheckReordering();
                            if (musicController.scene != 2)
                            {
                                rightController.CheckPitch();
                            }
                        }
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
                            pitchIndicator.GetComponent<Renderer>().material = leftController.indicator.GetComponent<Renderer>().material;
                            leftController.indicator.GetComponent<Renderer>().material = tempMaterial;
                            leftController.pitchIndicator.GetComponent<Renderer>().material = tempMaterial;
                            tempColor = indicator.GetComponent<Light>().color;
                            indicator.GetComponent<Light>().color = leftController.indicator.GetComponent<Light>().color;
                            pitchIndicator.GetComponent<Light>().color = leftController.indicator.GetComponent<Light>().color;
                            leftController.indicator.GetComponent<Light>().color = tempColor;
                            leftController.pitchIndicator.GetComponent<Light>().color = tempColor;
                            tempScale = pitchIndicator.transform.localScale;
                            pitchIndicator.transform.localScale = leftController.pitchIndicator.transform.localScale;
                            leftController.pitchIndicator.transform.localScale = tempScale;
                            tempBool = correctPitch;
                            correctPitch = leftController.correctPitch;
                            leftController.correctPitch = tempBool;
                            tempBool = correctReordering;
                            correctReordering = leftController.correctReordering;
                            leftController.correctReordering = tempBool;
                            leftController.CheckReordering();
                            if (musicController.scene != 2)
                            {
                                leftController.CheckPitch();
                            }
                        }
                    }
                    h = 0;
                    if (musicController.scene != 2)
                    {
                        CheckPitch();
                    }
                    CheckReordering();
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

    public void CheckPitch()
    {
        if (pitch < 1.01 && pitch > 0.99 && correctPitch == false)
        {
            musicController.pitchProgress += 1.0f;
            musicController.pitchProgressBar.UpdateBar(musicController.pitchProgress, 5.0f);
            correctPitch = true;
        }
        if ((pitch > 1.01 || pitch < 0.99) && correctPitch == true)
        {
            musicController.pitchProgress -= 1.0f;
            musicController.pitchProgressBar.UpdateBar(musicController.pitchProgress, 5.0f);
            correctPitch = false;
        }
    }

    public void CheckReordering() {
        if (targetClipName == clip.name && correctReordering == false)
        {
            musicController.reorderingProgress += 1.0f;
            musicController.reorderingProgressBar.UpdateBar(musicController.reorderingProgress, 5.0f);
            correctReordering = true;
        }

        if (targetClipName != clip.name && correctReordering == true)
        {
            musicController.reorderingProgress -= 1.0f;
            musicController.reorderingProgressBar.UpdateBar(musicController.reorderingProgress, 5.0f);
            correctReordering = false;
        }
    }

}
