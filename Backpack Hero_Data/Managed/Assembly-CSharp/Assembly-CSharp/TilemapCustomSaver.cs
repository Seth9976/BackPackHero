using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020001A1 RID: 417
public class TilemapCustomSaver : MonoBehaviour
{
	// Token: 0x0600109B RID: 4251 RVA: 0x0009DE35 File Offset: 0x0009C035
	private void Start()
	{
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0009DE37 File Offset: 0x0009C037
	private void Update()
	{
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0009DE3C File Offset: 0x0009C03C
	private string GetCharFromBase(TileBase tile)
	{
		if (tile == null)
		{
			return "x";
		}
		string text = this.tiles.IndexOf(tile).ToString();
		if (text == "-1")
		{
			return "x";
		}
		return text;
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0009DE84 File Offset: 0x0009C084
	private TileBase GetTileBaseFromChar(string number)
	{
		if (number == "x")
		{
			return null;
		}
		int num = int.Parse(number);
		return this.tiles[num];
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0009DEB4 File Offset: 0x0009C0B4
	public string ConvertToString(out Vector2Int bounds, out Vector2Int origin)
	{
		this.tilemap.CompressBounds();
		string text = "";
		for (int i = this.tilemap.cellBounds.min.x; i < this.tilemap.cellBounds.max.x; i++)
		{
			for (int j = this.tilemap.cellBounds.min.y; j < this.tilemap.cellBounds.max.y; j++)
			{
				text += this.GetCharFromBase(this.tilemap.GetTile(new Vector3Int(i, j, 0)));
			}
		}
		origin = (Vector2Int)this.tilemap.origin;
		bounds = (Vector2Int)this.tilemap.size;
		return text;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0009DFA8 File Offset: 0x0009C1A8
	private void RemoveAllTiles()
	{
		for (int i = this.tilemap.cellBounds.min.x; i < this.tilemap.cellBounds.max.x; i++)
		{
			for (int j = this.tilemap.cellBounds.min.y; j < this.tilemap.cellBounds.max.y; j++)
			{
				this.tilemap.SetTile(new Vector3Int(i, j, 0), null);
			}
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0009E04C File Offset: 0x0009C24C
	public void ConvertFromString(string x, Vector2Int size, Vector2Int origin)
	{
		this.tilemap.origin = (Vector3Int)origin;
		this.tilemap.size = (Vector3Int)size;
		this.RemoveAllTiles();
		int num = 0;
		for (int i = this.tilemap.cellBounds.min.x; i < this.tilemap.cellBounds.max.x; i++)
		{
			for (int j = this.tilemap.cellBounds.min.y; j < this.tilemap.cellBounds.max.y; j++)
			{
				TileBase tileBaseFromChar = this.GetTileBaseFromChar(x[num].ToString());
				this.tilemap.SetTile(new Vector3Int(i, j, 0), tileBaseFromChar);
				num++;
			}
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0009E13C File Offset: 0x0009C33C
	public string ConvertStructuresToString(GameObject buildingParent)
	{
		string text = "";
		foreach (Overworld_Structure overworld_Structure in buildingParent.GetComponentsInChildren<Overworld_Structure>())
		{
			text += ":";
			if (overworld_Structure.transform.localScale.x == -1f)
			{
				text += "-";
			}
			text = text + Item2.GetDisplayName(overworld_Structure.name) + "+";
			text += overworld_Structure.transform.position.ToString();
			ItemStorage component = overworld_Structure.GetComponent<ItemStorage>();
			if (component)
			{
				text += "+";
				foreach (string text2 in component.storedItems)
				{
					text += "~";
					text += text2;
				}
			}
			text += ":";
		}
		Debug.Log("Saving town as: " + text);
		return text;
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0009E268 File Offset: 0x0009C468
	public void ConvertStringToBuildings(GameObject buildingParent, string text)
	{
		if (text == null || text == "")
		{
			return;
		}
		foreach (object obj in buildingParent.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		Debug.Log("Loading town from: " + text);
		string[] array = text.Split(':', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i] == ""))
			{
				string[] array2 = array[i].Split('+', StringSplitOptions.None);
				string text2 = array2[0];
				bool flag = false;
				if (text2.StartsWith("-"))
				{
					text2 = text2.Substring(1);
					flag = true;
				}
				GameObject gameObject = Overworld_BuildingManager.main.GetBuilding(text2);
				if (gameObject == null)
				{
					Debug.LogError("Building not found: " + text2);
				}
				else
				{
					gameObject = Object.Instantiate<GameObject>(gameObject, buildingParent.transform);
					gameObject.transform.position = TilemapCustomSaver.StringToVector3(array2[1]);
					if (flag)
					{
						gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
					}
					ItemStorage component = gameObject.GetComponent<ItemStorage>();
					if (component)
					{
						for (int j = 2; j < array2.Length; j++)
						{
							string[] array3 = array2[j].Split('~', StringSplitOptions.None);
							component.AddStoredItems(array3);
						}
					}
					gameObject.name = text2;
				}
			}
		}
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0009E404 File Offset: 0x0009C604
	public static Vector3 StringToVector3(string sVector)
	{
		if (sVector.StartsWith("(") && sVector.EndsWith(")"))
		{
			sVector = sVector.Substring(1, sVector.Length - 2);
		}
		string[] array = sVector.Split(',', StringSplitOptions.None);
		return new Vector3(float.Parse(array[0], CultureInfo.InvariantCulture), float.Parse(array[1], CultureInfo.InvariantCulture), float.Parse(array[2], CultureInfo.InvariantCulture));
	}

	// Token: 0x04000D87 RID: 3463
	public List<TileBase> tiles = new List<TileBase>();

	// Token: 0x04000D88 RID: 3464
	[SerializeField]
	private Tilemap tilemap;
}
