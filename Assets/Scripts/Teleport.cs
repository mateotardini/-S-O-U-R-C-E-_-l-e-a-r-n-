using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	private PlayerController player;
	private Vector3 startposition;

	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		startposition = player.transform.position;
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.name == "Player") {
			player.transform.position = startposition;
		}

	}
}
