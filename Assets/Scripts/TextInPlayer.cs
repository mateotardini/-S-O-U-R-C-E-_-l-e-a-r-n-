using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInPlayer : MonoBehaviour {

	public GameObject text;
	public Animator animator;
	public static TextInPlayer textinplayer;
	public float time;
	public int usless;

	void Awake(){
		usless = PlayerPrefs.GetInt ("Tutorial");
	}

	IEnumerator Tutorial(){
		text.SetActive (true);
		yield return new WaitForSeconds (time);
		animator.SetBool ("TextGone",true);
		yield return new WaitForSeconds (1f);
		text.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.CompareTag ("Player") && usless == 0) {
			StartCoroutine (Tutorial ());
			usless = 1;
		}
	}
}
