using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {

	private Rigidbody2D cuerpo;
	private BoxCollider2D boxcollider;
	private Vector3 startposition;
	private Quaternion startrotation;
	private PlayerController player;
	public GameObject Effect;

	void Start () {
		cuerpo = GetComponent<Rigidbody2D> ();
		boxcollider = GetComponent<BoxCollider2D> ();
		startposition = transform.position;
		startrotation = transform.localRotation;
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
	}
		
	void Update(){
		if (player.deadplayer == true) {
			Invoke ("Reset", 0.35f);
		}
	}

	void ChangeBondyType () {
		cuerpo.bodyType = RigidbodyType2D.Dynamic;
		Effect.SetActive (true);
	}
		

	void Reset(){
		//Application.LoadLevel(Application.loadedLevel);
		transform.position = startposition;
		transform.localRotation = startrotation;
		Effect.SetActive (false);
		cuerpo.bodyType = RigidbodyType2D.Static;
		boxcollider.isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.transform.CompareTag ("Player"))
			Invoke ("ChangeBondyType", 0.23f);
		if (col.transform.CompareTag ("Fall"))
			boxcollider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.transform.CompareTag ("Player"))
			Invoke ("ChangeBondyType", 0.23f);
		if (col.transform.CompareTag ("Fall")||col.transform.name == "fallingfloor" || col.transform.CompareTag("Water"))
			boxcollider.isTrigger = true;
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.transform.name == "fallingfloor"  || col.transform.CompareTag ("Water"))
			boxcollider.isTrigger = false;
	}

}
