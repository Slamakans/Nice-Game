using UnityEngine;
using System.Collections;

public class TileSelectionScript : MonoBehaviour {
	public float slotSize = 24f;

	void LateUpdate () {
		if(Input.GetButtonDown("Fire1"))
		{
			var mousePos = Input.mousePosition;
			var panel = transform.Find("SelectionPanel").GetComponent<RectTransform>();

			Vector2 localPos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, mousePos, Camera.main, out localPos);
			Vector3[] corners = new Vector3[4];
			panel.GetLocalCorners(corners);

			if(localPos.x > corners[0].x && localPos.x < corners[2].x - slotSize/3 && localPos.y < corners[1].y && localPos.y > corners[3].y + slotSize/3)
			{
				var selection = panel.transform.Find("Selection").GetComponent<RectTransform>();
				Debug.Log(localPos);
				Vector2 converted = new Vector2(localPos.x - (localPos.x % slotSize), localPos.y - (localPos.y % slotSize));
				selection.localPosition = new Vector2(1 + converted.x, -1 + converted.y);
				int id = (int) (Mathf.Floor(converted.x + Mathf.Abs(converted.y*((corners[2].x - (corners[2].x % slotSize))/slotSize)))/slotSize);
				GameObject.FindGameObjectWithTag("TilePlacer").GetComponent<TilePlacingScript>().SetTileId(id);
			}
		}
	}
}