using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerchanges : MonoBehaviour {

	private PlayerController player;

	void Start(){
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
	}

//	void Update(){
//		if (player.deadplayer) {
//			player.mov = 10f;
///			player.fallspeed = 50f;
//		}
//	}


	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.name == "Player") {
			player.mov = 30f;
			player.fallspeed = 30f;
			print ("Do");
		}
	}
}
