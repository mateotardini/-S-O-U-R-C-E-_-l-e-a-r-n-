using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {

	public AudioMixer mastermixer, musicmixter, soundmixer;
	public TextMeshProUGUI tutorialtext;
	public GameObject settingsmenu, levelmenu, loadingscreen, collectiblesscreen, mainmenumusictext;
	private bool settings=false, collectibles = false;
	public int tutorialon;
	private Animator animator;

	void Start(){
		Data.data.Load ();
		tutorialon = PlayerPrefs.GetInt ("Tutorial");
		if (PlayerPrefs.GetInt ("Tutorial") == 0)
			tutorialtext.text = "on";
		else
			tutorialtext.text = "off";
		
		StartCoroutine (MainMenuMusic ());
	}

	public void Play(){
		if (PlayerPrefs.GetInt ("levelAt") == 0) {
			int sceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
			StartCoroutine (LoadAsynchronously (sceneIndex));
		}
		else
			levelmenu.SetActive (true);
	}

	IEnumerator LoadAsynchronously (int sceneIndex){
		AsyncOperation op = SceneManager.LoadSceneAsync (sceneIndex);
		loadingscreen.SetActive (true);

		while (!op.isDone) {
			float progress = Mathf.Clamp01 (op.progress / .9f);
			yield return null;
		}
	}

	IEnumerator MainMenuMusic(){
		animator = mainmenumusictext.GetComponent<Animator> ();
		yield return new WaitForSeconds (0.5f);
		mainmenumusictext.SetActive (true);
		yield return new WaitForSeconds (8f);
		animator.SetBool ("TextGone", true);
		yield return new WaitForSeconds (1f);
		mainmenumusictext.SetActive (false);
		animator = null;
	}

	public void Quit(){
		Application.Quit();
	}

	public void Back(){
		levelmenu.SetActive (false);
	}

	public void Collectibles(){
		if (!collectibles) {
			collectiblesscreen.SetActive (true);
			collectibles = true;
		} else {
			collectiblesscreen.SetActive (false);
			collectibles = false;
		}
	}


	public void Settings(){
		if (!settings) {
			settingsmenu.SetActive (true);
			settings = true;
		} else {
			settingsmenu.SetActive (false);
			settings = false;
		}
	}

	public void SetVolumeMaster (float volume){
		
		mastermixer.SetFloat ("volume", volume);
	}
	public void SetVolumeMusic (float volume){
		
		musicmixter.SetFloat ("music", volume);
	}
	public void SetVolumeSounds(float volume){
		
		soundmixer.SetFloat ("sounds", volume);
	}

	public void Tutorial(){
		if (tutorialon == 0) {
			PlayerPrefs.SetInt ("Tutorial", 1);
			tutorialon = PlayerPrefs.GetInt ("Tutorial");
			tutorialtext.text = "off";
		} else if (tutorialon == 1) {
			PlayerPrefs.SetInt ("Tutorial", 0);
			tutorialon = PlayerPrefs.GetInt ("Tutorial");
			tutorialtext.text = "on";
		}
	}

	public void PerdoCadarioURL() {
		Application.OpenURL("https://open.spotify.com/artist/4SsE2cInrEC9sni3XKSryb?si=qhhWM0hxQqqfwwhEt-fZYg");
	}

	public void BlackYndraURL() {
		Application.OpenURL("https://www.bandlab.com/donkumbia");
	}

	public void Close() {
		Application.Quit();
	}
}
