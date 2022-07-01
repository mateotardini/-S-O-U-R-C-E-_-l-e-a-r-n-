using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusic : MonoBehaviour {

	public int numrandom, prevnum;
	public Animator animator;
	public AudioSource audiosource;
	public AudioClip[] MusicArray;
	public Boton script;

	private static GameMusic instance = null;
	public static GameMusic Instance {
		get {return instance;}}

	void Start(){
		prevnum = 7;
		RandomMusic ();
		script = GameObject.Find("Canvas").GetComponent<Boton>();
		StartCoroutine (script.MusicRights (numrandom));
	}

	void Awake (){
		audiosource = GetComponent<AudioSource> ();

		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		if (SceneManager.GetActiveScene().name == "PrincipalMenu")
			Destroy(this.gameObject);
		if (!audiosource.isPlaying) {
			RandomMusic ();
			script = GameObject.Find("Canvas").GetComponent<Boton>();
			StartCoroutine (script.MusicRights (numrandom));
		}
	}

	////////////////////////////////////////////////////////////////////////////
	/// Randoms the music.
	void RandomMusic(){
		numrandom = Random.Range (0, MusicArray.Length);
		if(numrandom != prevnum){ 
			audiosource.clip = MusicArray [numrandom];
			audiosource.PlayOneShot (audiosource.clip);
			prevnum = numrandom;
		}
	}

	///////////////////////////////////////////////////////////////////////////
	/// Active the objects with the name and artist of the song
}