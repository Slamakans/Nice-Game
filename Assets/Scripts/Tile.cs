using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Tile : MonoBehaviour {
	public Texture2D Tilesheet;

	public int TileSize;

	private static Sprite[] TILE_SPRITES;
	private int TEXTURE_ID;

	private static Dictionary<string, Tile>[] tiles = {
		new Dictionary<string, Tile>(),
		new Dictionary<string, Tile>(),
		new Dictionary<string, Tile>()
	};

	private void UpdateSprite()
	{
		if(TILE_SPRITES == null)
			TILE_SPRITES = Resources.LoadAll<Sprite>("Sprites/Tilesheets/" + Tilesheet.name);

		GetComponent<SpriteRenderer>().sprite = TILE_SPRITES[TEXTURE_ID];
	}

	public void SetId(int id)
	{
		if(TEXTURE_ID == id && GetComponent<SpriteRenderer>().sprite != null) return;
		TEXTURE_ID = id;
		UpdateSprite();
	}

	public void SetPos(Vector2 pos)
	{
		pos = ConvertToTilePos(pos);
		if(GetTileAt(pos) != null)
		{
			var tile = GetTileAt(pos);
			tile.SetId(TEXTURE_ID);
			Destroy(gameObject);
		}
		else
		{
			transform.position = pos;
		}
	}

	public static Vector2 ConvertToTilePos(Vector2 pos)
	{
		return new Vector2(Mathf.Floor(pos.x), Mathf.Floor(pos.y));
	}

	public static Tile GetTileAtLayer(Vector2 pos, int layer)
	{
		pos = ConvertToTilePos(pos);
		Tile tile;
		if(tiles[layer].TryGetValue(ConvertToKey(pos), out tile))
		{
			return tile.GetComponent<Tile>();
		}
		else
		{
			return null;
		}
	}

	public static Tile GetTileAt(Vector2 pos)
	{
		return GetTileAtLayer(pos, 0);
	}

	public static void CreateTileAtLayer(Transform prefab, Vector2 pos, int id, int layer)
	{
		var tile = Instantiate(prefab);
		tile.GetComponent<Tile>().SetId(id);
		tile.GetComponent<Tile>().SetPos(pos);
		tiles[layer].Add(ConvertToKey(pos), tile.GetComponent<Tile>());
	}

	public static void CreateTile(Transform prefab, Vector2 pos, int id)
	{
		CreateTileAtLayer(prefab, pos, id, 0);
	}

	private static string ConvertToKey(Vector2 pos)
	{
		pos = ConvertToTilePos(pos);
		return pos.x + "#" + pos.y;
	}

	void Update()
	{

	}
}