using UnityEngine;

public class CameraColorChange : MonoBehaviour
{
	public Color color1, color2, color3;
	public Camera cam;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("ColorChange"))
		{
			cam.backgroundColor = color2;
			color3 = color2;
			color2 = color1;
			color1 = color3;
		}
	}
}
