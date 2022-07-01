using System.Collections;
using UnityEngine;

public class LockScale : MonoBehaviour {

	public AudioSource fallsound;
	private Vector3 startposition;
	private Quaternion startrotation;
	private Vector3 startscale;
	private PlayerController player;
	private Boton botons;
	private Rigidbody2D cuerpo;
	private bool kinematic = true;
	public bool restart=false;

	void Start () {
		startscale = transform.localScale;
		startposition = transform.position;
		startrotation = transform.localRotation;
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		botons = GameObject.Find ("Canvas").GetComponent<Boton> ();
		cuerpo = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if (player.deadplayer || botons.restarted)
			StartCoroutine (Reset());
		if (transform.localScale != startscale)
			transform.localScale = startscale;
	}
		
	IEnumerator Reset(){
		transform.parent = null;
		yield return new WaitForSeconds (0.01f);
		cuerpo.simulated = true;
		cuerpo.mass = 400f;
		cuerpo.constraints = RigidbodyConstraints2D.None;
		cuerpo.constraints = RigidbodyConstraints2D.FreezeRotation;
		cuerpo.bodyType = RigidbodyType2D.Static;
		kinematic = true;
		transform.position = startposition;
		transform.localRotation = startrotation;
	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.CompareTag ("Water")) {
			cuerpo.mass = 1f;
			cuerpo.constraints = RigidbodyConstraints2D.None;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.CompareTag ("Friction"))
			transform.parent = col.transform;
		if (col.transform.CompareTag ("OutofMap")) {
			StartCoroutine (Reset ());
		}
	}

	void OnCollisionEnter2D(Collision2D col){
//		if (cuerpo.velocity.y < 10f && col.transform.tag == "Wall" && col.transform.tag != "Player")
//			fallsound.Play ();
		
		if (col.gameObject.name == "Player" && kinematic) {
			cuerpo.bodyType = RigidbodyType2D.Dynamic;
			kinematic = false;
		}
	}
	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.CompareTag ("MovilWall") || (col.gameObject.CompareTag ("MovilWall") && player.holdingobject == col.gameObject))
			transform.parent = col.transform;
	}

	void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.CompareTag ("MovilWall"))
			transform.parent = null;
	}
}
