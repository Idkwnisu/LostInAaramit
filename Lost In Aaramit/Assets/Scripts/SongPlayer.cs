using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SongPlayer: MonoBehaviour
{
    public Text InteractText;
    public GameObject controllerLayout;
    public Camera objectCamera;
    public GameObject Player;
    public AudioClip clip;
    public float pitch;
    public bool simplePlayer;

    public GameObject indicator;
    public GameObject pitchIndicator;

    public MusicController musicController;
    private AudioSource Song;

    public SongPlayer leftController;
    public SongPlayer rightController;

    private bool interactable = false;
    private bool interacting = false;
    public bool correctPitch;
    public bool correctReordering;
    private string targetClipName;
    public DisplaySegment displaySegment;
    private Camera ciao;

    // Use this for initialization
    void Start()
    {
        InteractText.enabled = false;
        objectCamera.enabled = false;
        controllerLayout.SetActive(false);
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
                    ControlManager.instance.getCurrentCamera().enabled = true;
                    objectCamera.enabled = false;
                    InteractText.enabled = true;
                    controllerLayout.SetActive(false);
                    //Player.GetComponent<PlayerControllerRun>().ControlEnabling();
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().NonInteracting();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().NonInteracting();
                    }

                    musicController.PlayerIsInteracting(this, false);
                }
                else
                {
                    interacting = true;
                    ControlManager.instance.getCurrentCamera().enabled = false;
                    objectCamera.enabled = true;
                    InteractText.enabled = false;
                    controllerLayout.SetActive(true);
                    //Player.GetComponent<PlayerControllerRun>().ControlDisabling();
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().resetSpeed();
                        Player.GetComponent<PlayerControllerRun>().Interacting();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().resetSpeed();
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().Interacting();
                    }

                    musicController.PlayerIsInteracting(this, true);
                }
            }
            if (interacting)
            {
                //Debug.Log("" + clip.name);
                //Debug.Log("" + musicController.returnToFullTrack);
                if (Song.isPlaying == false)
                {
                    if (!simplePlayer)
                    {
                        Song.pitch = pitch;
                        Song.clip = clip;
                        Song.Play();
                    }
                }
                if (Input.GetButtonDown("Vertical") == true && simplePlayer == false)
                {
                    float h = Input.GetAxis("Vertical");
                    if (h > 0 && Song.pitch < 4.0f)
                    {
                        if (Input.GetButtonDown("Vertical") == true)
                        {
                            Song.pitch += 0.20f;
                            if (Song.pitch > 0.96 && Song.pitch < 1)
                            {
                                Song.pitch = 1.0f;
                                pitch = Song.pitch;
                            }
                            if (pitchIndicator.transform.localScale.y < 5.0F)
                            {
                                pitchIndicator.transform.localScale += new Vector3(0.0F, 0.1F, 0.0F);
                            }
                        }
                    }
                    if (h < 0 && Song.pitch > 0.2f)
                    {
                        if (Input.GetButtonDown("Vertical") == true)
                        {
                            Song.pitch -= 0.20f;
                            if (Song.pitch > 0.96 && Song.pitch < 1)
                            {
                                Song.pitch = 1.0f;
                            }
                            if (pitchIndicator.transform.localScale.y > 0.2F)
                            {
                                pitchIndicator.transform.localScale -= new Vector3(0.0F, 0.1F, 0.0F);
                            }
                        }
                    }
                    h = 0;
                    Song.pitch = Mathf.Round(Song.pitch * 100f) / 100f;
                    pitch = Song.pitch;
                    CheckCorrectness(0);
                    //Debug.Log("" + (float)Song.pitch);
                }
                if (Input.GetButtonDown("Horizontal") == true && simplePlayer == false)
                {
                    float h = Input.GetAxis("Horizontal");
                    //Debug.Log("" + (float)Song.pitch);
                    if (h > 0)
                    {
                        if (Input.GetButtonDown("Horizontal") == true)
                        {
                            SwapSegments(rightController, clip, pitch, indicator.GetComponentInChildren<SkinnedMeshRenderer>().material, indicator.GetComponentInChildren<Light>().color, pitchIndicator.transform.localScale, correctPitch);
                        }
                    }
                    if (h < 0)
                    {
                        if (Input.GetButtonDown("Horizontal") == true)
                        {
                            SwapSegments(leftController, clip, pitch, indicator.GetComponentInChildren<SkinnedMeshRenderer>().material, indicator.GetComponentInChildren<Light>().color, pitchIndicator.transform.localScale, correctPitch);
                        }
                    }
                    h = 0;
                    CheckCorrectness(2);
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

    public void CheckCorrectness(int correctnessType)
    {
        //correctnessType: 0=justPitch; 1=justReordering; 2=both
        if (pitch < 1.01 && pitch > 0.99 && correctPitch == false && correctnessType != 1)
        {
            musicController.pitchProgress += 1.0f;
            musicController.pitchProgressBar.UpdateBar(musicController.pitchProgress, 5.0f);
            correctPitch = true;
        }
        if ((pitch > 1.01 || pitch < 0.99) && correctPitch == true && correctnessType != 1)
        {
            musicController.pitchProgress -= 1.0f;
            musicController.pitchProgressBar.UpdateBar(musicController.pitchProgress, 5.0f);
            correctPitch = false;
        }

        if (targetClipName == clip.name && correctReordering == false && correctnessType != 0)
        {
            musicController.reorderingProgress += 1.0f;
            musicController.reorderingProgressBar.UpdateBar(musicController.reorderingProgress, 5.0f);
            correctReordering = true;
        }

        if (targetClipName != clip.name && correctReordering == true && correctnessType != 0)
        {
            musicController.reorderingProgress -= 1.0f;
            musicController.reorderingProgressBar.UpdateBar(musicController.reorderingProgress, 5.0f);
            correctReordering = false;
        }

        if (correctPitch && correctReordering)
        {
            indicator.GetComponent<Animator>().enabled = true;
            musicController.playNoteSoundEffect();
        }
        else
        {
            indicator.GetComponent<Animator>().enabled = false;
        }
    }

    public void SwapSegments(SongPlayer controller, AudioClip tempClip, float tempPitch, Material tempMaterial, Color tempColor, Vector3 tempScale, bool tempBool)
    {
        clip = controller.clip;
        controller.clip = tempClip;
        pitch = controller.pitch;
        controller.pitch = tempPitch;
        Song.pitch = pitch;
        Song.clip = clip;
        Song.Play();
        indicator.GetComponentInChildren<SkinnedMeshRenderer>().material = controller.indicator.GetComponentInChildren<SkinnedMeshRenderer>().material;
        pitchIndicator.GetComponent<MeshRenderer>().material = controller.indicator.GetComponentInChildren<SkinnedMeshRenderer>().material;
        controller.indicator.GetComponentInChildren<SkinnedMeshRenderer>().material = tempMaterial;
        controller.pitchIndicator.GetComponent<MeshRenderer>().material = tempMaterial;
        indicator.GetComponentInChildren<Light>().color = controller.indicator.GetComponentInChildren<Light>().color;
        pitchIndicator.GetComponent<Light>().color = controller.indicator.GetComponentInChildren<Light>().color;
        controller.indicator.GetComponentInChildren<Light>().color = tempColor;
        controller.pitchIndicator.GetComponent<Light>().color = tempColor;
        pitchIndicator.transform.localScale = controller.pitchIndicator.transform.localScale;
        controller.pitchIndicator.transform.localScale = tempScale;
        correctPitch = controller.correctPitch;
        controller.correctPitch = tempBool;
        tempBool = correctReordering;
        correctReordering = controller.correctReordering;
        controller.correctReordering = tempBool;
        controller.CheckCorrectness(2);
    }
}
