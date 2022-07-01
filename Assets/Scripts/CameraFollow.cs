using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject follow;
	public Vector2 min, max;
	private Vector2 velocity;
	public float smoothtime;
	public Camera cam;
	public Color color2;

	void FixedUpdate () {
		float posx = Mathf.SmoothDamp (transform.position.x, follow.transform.position.x, ref velocity.x, smoothtime);
		float posy = Mathf.SmoothDamp (transform.position.y, follow.transform.position.y, ref velocity.y, smoothtime);
		transform.position = new Vector3 (Mathf.Clamp (posx, min.x, max.x), transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x,Mathf.Clamp (posy, min.y, max.y), transform.position.z);
	}
}