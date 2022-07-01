using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Boton : MonoBehaviour {

	private PlayerController player;
	public GameObject[] MusicRightsText;

	public static Boton boton;
	public GameObject pausetext, music, on, off, pickuptext;
	public TextMeshPro textmeshcount;
	public Animator animator, animatorText;
	public AudioSource audiosource;
	private bool paused = false, muted = false;
	public bool restarted = false, pickedup = false;

	void Start(){
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		audiosource = GameObject.FindWithTag("Music").GetComponent<AudioSource> ();
//		animator = text.GetComponent<Animator> ();
	}

	void Update(){
		if (pickedup)
			StartCoroutine (PickingText ());
	}
	///////////////////////////////////////////////////////////////////////////
	public void presspause() {
		Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
		if (!paused) {
			pausetext.SetActive (true);
			paused = true;
		} else {
			pausetext.SetActive (false);
			paused = false;
		}
	}

	public void pressmute(){
		audiosource.mute = !audiosource.mute;
		if (!muted) {
			on.SetActive (false);
			off.SetActive (true);
			muted = true;
		} else {
			on.SetActive (true);
			off.SetActive (false);
			muted = false;
		}
	}

	public void pressrestart() {
		StartCoroutine (Restart ());
	}

	IEnumerator Restart(){
		restarted = true;
		yield return new WaitForSeconds (.1f);
		restarted = false;
	}

	public void pressquit() {
		Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
		SceneManager.LoadScene ("PrincipalMenu");
	}

	///////////////////////////////////////////////////////////////////////////
	public IEnumerator MusicRights(int numrandom)
	{
		animatorText = MusicRightsText[numrandom].GetComponent<Animator>();
		yield return new WaitForSeconds(0.5f);
		MusicRightsText[numrandom].SetActive(true);
		yield return new WaitForSeconds(8f);
		animatorText.SetBool("TextGone", true);
		yield return new WaitForSeconds(1f);
		MusicRightsText[numrandom].SetActive(false);
		animatorText = null;
	}
	IEnumerator PickingText (){
		animator = pickuptext.GetComponent<Animator> ();
		if(Data.data.Pickupcount < 10)
			textmeshcount.text = "0" + Data.data.Pickupcount.ToString () + "/30";
		else 
			textmeshcount.text = Data.data.Pickupcount.ToString () + "/30";
		pickedup = false;
		yield return new WaitForSeconds (0.5f);
		pickuptext.SetActive (true);
		yield return new WaitForSeconds (6f);
		animator.SetBool ("PUTG", true);
		yield return new WaitForSeconds (1f);
		pickuptext.SetActive (false);
	}
	///////////////////////////////////////////////////////////////////////////

	public void pressedgrab(BaseEventData eventData){
		player.pressgrab = true;
	}

	public void notpressedgrab(BaseEventData eventData){
		player.pressgrab = false;
	}

	public void pressedW(BaseEventData eventData){
		player.pressjump = true;
	}

	public void notpressedW(BaseEventData eventData){
		player.pressjump= false;
	}

	public void pressedA(BaseEventData eventData){
		player.pressa = true;
	}

	public void notpressedA(BaseEventData eventData){
		player.pressa = false;
	}

	public void pressedD(BaseEventData eventData){
		player.pressd = true;
	}

	public void notpressedD(BaseEventData eventData){
		player.pressd = false;
	}
}
