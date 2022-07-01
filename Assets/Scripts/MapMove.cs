using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour {

	public Transform target;
	public float speed, startspeed;
	private Vector3 start, end;

	void Start () {
		speed = startspeed;
		if (target != null) {
			target.parent = null;
			start = transform.position;
			end = target.position;
		}
	}

	void FixedUpdate () {
		if (target != null) {
			float fixedspeed = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, fixedspeed);
		}

		if (transform.position == target.position)
			target.position = (target.position == start) ? end : start;
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