using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleporter : MonoBehaviour {

	public Transform platformPoint;
	public Transform puzzlePoint;

	public Transform bossPoint;
	
	public Transform puzzlePlatformPoint;

	public Transform labyrinthPoint;

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F8))
		{
			player.GetComponent<PlayerPosition>().position_save(platformPoint);
			 PlayerPrefs.SetInt("Plume", 0);
			 PlayerPrefs.SetInt("bossDown", 0);
			 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if(Input.GetKeyDown(KeyCode.F9))
		{
			player.GetComponent<PlayerPosition>().position_save(puzzlePoint);
			PlayerPrefs.SetInt("Plume", 0);
			PlayerPrefs.SetInt("bossDown", 0);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if(Input.GetKeyDown(KeyCode.F10))
		{
			player.GetComponent<PlayerPosition>().position_save(bossPoint);
			 PlayerPrefs.SetInt("Plume", 1);
			 PlayerPrefs.SetInt("bossDown", 0);
			 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if(Input.GetKeyDown(KeyCode.F11))
		{
			player.GetComponent<PlayerPosition>().position_save(puzzlePlatformPoint);
			 PlayerPrefs.SetInt("Plume", 1);
			 PlayerPrefs.SetInt("bossDown", 1);
			 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if(Input.GetKeyDown(KeyCode.F12))
		{
			player.GetComponent<PlayerPosition>().position_save(labyrinthPoint);
			PlayerPrefs.SetInt("Plume", 1);
			PlayerPrefs.SetInt("bossDown", 1);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
