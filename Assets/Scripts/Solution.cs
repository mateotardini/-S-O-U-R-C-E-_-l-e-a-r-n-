using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution : MonoBehaviour {
	private PlayerController player;
	public Transform target, target2;
	public GameObject move, move2;
	public Vector3 start;
	public float speed, vel;

	public GameObject solutioned, solution, friction, sprite;

	void Start () {
		friction = solution.transform.GetChild (2).gameObject;
		sprite = transform.GetChild (1).gameObject;
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		start = move.transform.position;
		if (target != null) {
			target.parent = null;
		}
		if (target2 != null) {
			target2.parent = null;
		}
	}
	
	void FixedUpdate () {
		if (target != null && move != null) {
			float fixedspeed = speed * Time.deltaTime;
			move.transform.position = Vector3.MoveTowards (move.transform.position, target.position, fixedspeed);
		}
		if (target2 != null && move2 != null) {
			float fixedspeed = speed * Time.deltaTime;
			move2.transform.position = Vector3.MoveTowards (move2.transform.position, target2.position, fixedspeed);
		}

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == solution && col.gameObject.name != "Key") {
			player.holdingobject = null;
			friction.transform.parent = null;
			sprite.SetActive (false);
			if (move.transform.name == "Teleport" || move.transform.name == "exit")
				move.transform.GetChild(2).gameObject.SetActive(true);
			Destroy (solution, 0.01f);
			solutioned.SetActive (true);
			speed = 5f*vel;
		}
		if (col.gameObject.name == "Key") {
			sprite.SetActive (false);
			solutioned.SetActive (true);
			speed = 5f*vel;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name == "Key") {
			speed = 0f*vel;
		}
	}
}
