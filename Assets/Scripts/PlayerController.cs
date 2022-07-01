using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	private float checkRadius = 0.3f, doubleTapTimeL, doubleTapTimeR, startmove;
	public float mov, maxmov = 50f, WaitTime, jumpforce, fallforce, wallslidespeed, fallspeed, wallcheckdistance;

	private bool jump, doublejump, falling = false, doubleTapR, doubleTapL, candash, facingright, canmove;
	private bool isGrounded, isTouchingWall, isWallSliding, isTouchingObject, isJumping;
	public bool holding = false, playerdashing, deadplayer = false, isHoldingObject=false;

	//Botoms
	public bool pressjump = false, pressgrab = false, pressa = false, pressd = false;

	public AudioSource jumpsound, footsound, deadsound;
	public Animator animator;
	public Camera cam;
	public Color color1;

	private Vector3 startposition;
	public GameObject holdingobject = null, deatheffect;
	public Transform feetPos, wallCheck;
	public LayerMask whatIsGround, whatIsObject;
	private Rigidbody2D cuerpo;


	void Start () {
		startmove = mov;
		startposition = transform.position;
		cuerpo = GetComponent<Rigidbody2D> ();
		StartCoroutine (StartPause());
	}

	void FixedUpdate () {
		Checks ();
		MoveObject ();
		Move ();
		Jump ();
		Fall ();
		WallSliding ();

	}

	IEnumerator Animation(){
		animator.SetBool ("IsJumping", true);
		yield return new WaitForSeconds (.4f);
		animator.SetBool ("IsJumping", false);
	}

	IEnumerator StartPause(){
		cuerpo.constraints = RigidbodyConstraints2D.FreezeAll;
		canmove = false;
		yield return new WaitForSeconds (1.1f);
		canmove = true;
		cuerpo.constraints = RigidbodyConstraints2D.None;
		cuerpo.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	//////////////////////////////////////////////////////////////////////////////////////////

	void Checks (){
		isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

		if (facingright)
		{
			isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(transform.localScale.x, 0f), wallcheckdistance, whatIsGround);
			Debug.DrawRay(wallCheck.position, new Vector2(transform.localScale.x, 0f) , Color.green);
		}
		else if (!facingright)
		{
			isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(transform.localScale.x, 0f), -wallcheckdistance, whatIsGround);
			Debug.DrawRay(wallCheck.position, new Vector2(-transform.localScale.x, 0f), Color.green);
		}
		if (isGrounded)
			candash = true;
	}
	/////////////////////////////////////////////////////////////////////////////////////////

	void Jump(){
		if ((Input.GetKeyDown (KeyCode.W)|| pressjump) && (isGrounded || isWallSliding) && !isHoldingObject) {
			jump = true;
			doublejump = true;
			if (jump) {
				cuerpo.AddForce (Vector2.up * jumpforce, ForceMode2D.Impulse);
				StartCoroutine (Animation());
				jumpsound.Play();
				jump = false;
				pressjump = false;
			}
		}

		if ((Input.GetKeyDown (KeyCode.W)|| pressjump)&& !isGrounded && !isHoldingObject) {
			if (doublejump) {
				cuerpo.velocity =new Vector2(0f, 0f);
				StartCoroutine (Animation());
				cuerpo.AddForce (Vector2.up * jumpforce, ForceMode2D.Impulse);
				jumpsound.Play();
				doublejump = false;
				pressjump = false;
			}
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////

	void Move(){
		
		if ((Input.GetKey (KeyCode.A) || pressa) && canmove) {
			this.transform.Translate (Vector3.left * mov * Time.deltaTime);
			if (!isTouchingObject)
				facingright = false;
		}

		if ((Input.GetKey (KeyCode.D) || pressd) && canmove) {
			this.transform.Translate (Vector3.right * mov * Time.deltaTime);
			if (!isTouchingObject)
				facingright = true;
		}
			
		float limitemovimiento = Mathf.Clamp (cuerpo.velocity.y, -fallspeed, maxmov);
		cuerpo.velocity = new Vector2 (0f ,limitemovimiento);
	}
	/////////////////////////////////////////////////////////////////////////////////////////

	void Dash(){

		if (Input.GetKeyDown (KeyCode.D) && doubleTapR && !isGrounded) {
			if (Time.time - doubleTapTimeR < 0.6f) {
				cuerpo.AddForce (Vector2.right * 320, ForceMode2D.Impulse);
				cuerpo.AddForce (Vector2.up * 10, ForceMode2D.Impulse);
				doubleTapTimeR = 0f;
			} 
			doubleTapR = false;
			candash = false;
		}

		if (Input.GetKeyDown (KeyCode.D) && !doubleTapR && candash) {
			doubleTapR = true;
			doubleTapTimeR = Time.time;
		}

		if (Input.GetKeyDown (KeyCode.A) && doubleTapL && !isGrounded) {
			if (Time.time - doubleTapTimeL < 0.6f) {
				cuerpo.AddForce (Vector2.left * 320, ForceMode2D.Impulse);
				cuerpo.AddForce (Vector2.up * 10, ForceMode2D.Impulse);
				doubleTapTimeL = 0f;
			}
			doubleTapL = false;
			candash = false;
		}

		if (Input.GetKeyDown (KeyCode.A) && !doubleTapL && candash) {
			doubleTapL = true;
			doubleTapTimeL = Time.time;
		}

	}
	//////////////////////////////////////////////////////////////////////////////////////////

	void Fall(){
		if (Input.GetKey (KeyCode.S) && isGrounded == false) {
			this.transform.Translate (Vector3.down * fallforce * Time.deltaTime);
			falling = true;
		} else if (isGrounded)
			falling = false;
	}


	/////////////////////////////////////////////////////////////////////////////////////////

	void MoveObject(){

		//Verifico que el raycast "choque" con algo, si su tag es movingobject, esty tocando un objeto y puedo agarrar aquel que este tocando.
		//Si esta mirando hacia la derecha
		if (facingright) {
			RaycastHit2D hit = Physics2D.Raycast (wallCheck.position, transform.localScale, 0.9f, whatIsGround);
			if (hit.collider != null) {
				if (hit.collider.CompareTag ("MovingObject")) {
					isTouchingObject = true;
					holdingobject = hit.collider.gameObject;
				}
			} else if (hit.collider == null) {
				mov = startmove;
				isTouchingObject = false;
				isHoldingObject = false;
			}
		}
		//Si esta mirando hacia la izquierda
		else if (!facingright) {
			RaycastHit2D hit = Physics2D.Raycast (wallCheck.position, new Vector3(-1f,0f,0f) , 0.9f, whatIsGround);
			if (hit.collider != null) {
				if (hit.collider.CompareTag ("MovingObject")) {
					isTouchingObject = true;
					holdingobject = hit.collider.gameObject;
				}
			}else if (hit.collider == null) {
				mov = startmove;
				isTouchingObject = false;
				isHoldingObject = false;
			}
		}

		if (holdingobject != null) {
			if ((Input.GetKey (KeyCode.Space)|| pressgrab) && isTouchingObject) {
				isHoldingObject = true;
				holdingobject.transform.parent = this.transform;
				mov = 5f;
			} else {
				mov = startmove;
				holdingobject.transform.parent = null;
				holdingobject = null;
				isHoldingObject = false;
			}
		}
	}
			
	/////////////////////////////////////////////////////////////////////////////////////////

	private void WallSliding (){
		if ((isTouchingWall) && cuerpo.velocity.y < 0 && isGrounded == false)
			isWallSliding = true;
		else
			isWallSliding = false;	

		if (isWallSliding) {
			cuerpo.velocity = new Vector2 (cuerpo.velocity.x, wallslidespeed);
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////

	IEnumerator Respawn() {
		animator.SetBool ("IsJumping", false);
		animator.SetBool ("IsDead", true);
		deatheffect.SetActive (true);
		deadplayer = true;
		deadsound.Play();
		yield return new WaitForSeconds (0.4f);
		animator.SetBool ("IsDead", false);
		yield return new WaitForSeconds (0.1f);
		if(cam != null) 
			cam.backgroundColor = color1;
		deatheffect.SetActive (false);
		transform.position = startposition;
		StartCoroutine (StartPause ());
		deadplayer = false;
	}

	/////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D (Collision2D col){
//		if ((col.transform.CompareTag ("DestroyGround")) && falling == true)
//			Destroy (col.gameObject, 0.05f);
		if (col.transform.CompareTag ("MovilWall"))
			this.transform.parent = col.transform;
	}

	void OnCollisionStay2D (Collision2D col){
		if (col.transform.CompareTag ("MovilWall"))
			this.transform.parent = col.transform;
	}
		
	void OnCollisionExit2D (Collision2D col){
		if (col.transform.CompareTag ("MovilWall"))
			transform.parent = null;
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.transform.CompareTag ("Friction"))
			this.transform.parent = col.transform;

		if (col.gameObject.CompareTag("OutofMap") || col.gameObject.CompareTag("Water")) {
			StartCoroutine (Respawn ());
		}
		if (col.gameObject.CompareTag ("Checkpoint"))
			startposition = transform.position;
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.transform.CompareTag ("Friction"))
			transform.parent = null;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(wallCheck.position, transform.localScale);

	}

	}