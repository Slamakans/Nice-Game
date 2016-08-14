using UnityEngine;
using System.Collections;


public class TilePlacingScript : MonoBehaviour {
	public Transform TilePrefab;

	private int currentId = 0;

	void Update () {
			Vector3 mousePos = Input.mousePosition;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		Debug.Log(Tile.GetTileAt(worldPos));
		if(Input.GetButton("Fire1"))
		{
			if(Tile.GetTileAt(worldPos) == null)
			{
				Tile.CreateTile(TilePrefab, worldPos, currentId);
			}
			else
			{
				Tile.GetTileAt(worldPos).SetId(currentId);
			}
		}
	}

	void LateUpdate()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var start = Tile.ConvertToTilePos(pos);
		Debug.DrawRay(start, Vector2.right);
		Debug.DrawRay(start, Vector2.up);
		Debug.DrawRay(start + Vector2.right, Vector2.up);
		Debug.DrawRay(start + Vector2.up, Vector2.right);
	}
}
