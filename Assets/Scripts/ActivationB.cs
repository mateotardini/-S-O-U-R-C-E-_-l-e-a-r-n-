using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationB : MonoBehaviour {

	public Animator animationboton;
	public GameObject Object;

	IEnumerator Activation(){
		animationboton.SetBool ("Pressed", true);
		yield return new WaitForSeconds (3f);
		Object.SetActive (true);
		yield return new WaitForSeconds (10f);
		this.gameObject.SetActive (false);
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.transform.CompareTag ("Player") || col.transform.CompareTag ("MovingObject")) {
			StartCoroutine (Activation ());
		}
	}
}