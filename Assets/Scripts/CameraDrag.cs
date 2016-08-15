using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour {
	public float DragSpeed = 100f;
	private Vector3 dragOrigin;
	void Update () {
		if(Input.GetButtonDown("Fire2"))
		{
			dragOrigin = Input.mousePosition;
			return;
		}

		if(Input.GetButton("Fire2"))
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * -DragSpeed, pos.y * -DragSpeed, 0);
			transform.Translate(move, Space.World);
			dragOrigin = Input.mousePosition;
		}
	}
}
