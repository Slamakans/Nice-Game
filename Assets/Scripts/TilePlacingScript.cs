using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class TilePlacingScript : MonoBehaviour {
	public Transform TilePrefab;

	public Rect mapRect;

	private int currentId = 0;

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(mapRect.position, mapRect.position + new Vector2(mapRect.width, 0));
		Gizmos.DrawLine(mapRect.position, mapRect.position + new Vector2(0, mapRect.height));
		Gizmos.DrawLine(mapRect.position + new Vector2(mapRect.width, 0), mapRect.position + new Vector2(mapRect.width, mapRect.height));
		Gizmos.DrawLine(mapRect.position + new Vector2(0, mapRect.height), mapRect.position + new Vector2(mapRect.width, mapRect.height));
	}

	public void SetTileId(int id)
	{
		currentId = id;
	}
	
	void Update () {


		mapRect.position = Camera.main.transform.position - new Vector3(mapRect.size.x-4.5f, mapRect.size.y)/2;
		Vector3 mousePos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		if(mapRect.Contains(worldPos))
		{
			if(Input.GetButton("Fire1"))
			{
				var tile = Tile.GetTileAt(worldPos);
				if(tile == null)
				{
					Tile.CreateTile(TilePrefab, worldPos, currentId);
				}
				else
				{
					tile.SetId(currentId);
				}
			}
		}
	}

	void LateUpdate()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(mapRect.Contains(pos))
		{
			var start = Tile.ConvertToTilePos(pos);
			Debug.DrawRay(start, Vector2.right);
			Debug.DrawRay(start, Vector2.up);
			Debug.DrawRay(start + Vector2.right, Vector2.up);
			Debug.DrawRay(start + Vector2.up, Vector2.right);
		}
	}
}
