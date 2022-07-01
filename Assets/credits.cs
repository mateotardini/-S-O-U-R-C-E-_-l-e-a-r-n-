using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour {


	void FixedUpdate () {
		this.transform.Translate (Vector3.up * 2f * Time.deltaTime);
	}
}
