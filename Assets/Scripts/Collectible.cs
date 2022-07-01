using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Collectible : MonoBehaviour {


	private Rigidbody2D cuerpo;
	private BoxCollider2D boxcollider;
	private Vector3 startposition;
	private PlayerController player;
	public int ID;
	public GameObject effect, canvas;

	public float floatStrength = 0.75f; 

	void Start () {
		canvas = GameObject.Find ("Canvas");
		cuerpo = GetComponent<Rigidbody2D> ();
		boxcollider = GetComponent<BoxCollider2D> ();
		effect = transform.GetChild (0).gameObject;
		startposition = transform.position;
		}

	void Update () {
		if(cuerpo.velocity.y == 0f)
			transform.position = new Vector3(transform.position.x,startposition.y + ((float)Mathf.Sin(Time.time) * floatStrength),transform.position.z);
	
		if (Data.data.Pickups [ID] == 1)
			StartCoroutine (Pickingup ());		
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.transform.name == "Player") {
			canvas.GetComponent<Boton> ().pickedup = true;
			print ("pickup");
			Data.data.Pickupcount += 1;
			Data.data.Pickups [ID] = 1;
		}
	}
	IEnumerator Pickingup(){
		effect.SetActive (true);
		yield return new WaitForSeconds (0.6f);
		Destroy (this.gameObject);
	}

}
