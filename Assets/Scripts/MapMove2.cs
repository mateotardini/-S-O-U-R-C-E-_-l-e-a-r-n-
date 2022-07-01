using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove2 : MonoBehaviour {

	public Transform target, target2;
	public float speed, startspeed, secondspeed;
	private Vector3 start, end;

	void Start () {
		speed = startspeed;
		if (target != null && target2 != null) {
			target.parent = null;
			target2.parent = null;
			start = target.position;
			end = target2.position;
		}
	}

	void FixedUpdate () {
		if (target != null) {
			float fixedspeed = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, fixedspeed);
		}

		if (transform.position == target.position) {
			target.position = target2.position;
			speed = secondspeed;
		}
		if (transform.position == target2.position) {
			target.position = start;
			speed = startspeed;
		}
			
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("MovingObject"))
			speed = 0;

	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.CompareTag ("MovingObject"))
			speed = startspeed;
	}
}