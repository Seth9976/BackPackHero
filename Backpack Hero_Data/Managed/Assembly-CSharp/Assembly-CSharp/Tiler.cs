using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class Tiler : MonoBehaviour
{
	// Token: 0x06000EF4 RID: 3828 RVA: 0x000940B6 File Offset: 0x000922B6
	private void Awake()
	{
		Tiler.tilers.Add(this);
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x000940C3 File Offset: 0x000922C3
	private void OnDestroy()
	{
		Tiler.tilers.Remove(this);
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000940D4 File Offset: 0x000922D4
	private static bool TilerTileHere(Vector2 position, Transform thisTile)
	{
		for (int i = 0; i < Tiler.tilers.Count; i++)
		{
			Tiler tiler = Tiler.tilers[i];
			if (!(tiler.transform == thisTile))
			{
				if (tiler == null)
				{
					Tiler.tilers.RemoveAt(i);
					i--;
				}
				else if (Vector2.Distance(tiler.transform.localPosition, position) <= 0.5f)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0009414C File Offset: 0x0009234C
	private static int GetTileNumber(bool up, bool down, bool left, bool right)
	{
		int num = 15;
		if (up && down && left && right)
		{
			num = 14;
		}
		else if (up && left && right)
		{
			num = 13;
		}
		else if (up && down && left)
		{
			num = 12;
		}
		else if (down && left && right)
		{
			num = 11;
		}
		else if (up && down && right)
		{
			num = 10;
		}
		else if (left && up)
		{
			num = 9;
		}
		else if (left && down)
		{
			num = 8;
		}
		else if (right && down)
		{
			num = 7;
		}
		else if (right && up)
		{
			num = 6;
		}
		else if (right && left)
		{
			num = 5;
		}
		else if (up && down)
		{
			num = 4;
		}
		else if (left)
		{
			num = 3;
		}
		else if (right)
		{
			num = 2;
		}
		else if (down)
		{
			num = 1;
		}
		else if (up)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x000941F0 File Offset: 0x000923F0
	public void SetTile()
	{
		bool flag = Tiler.TilerTileHere(base.transform.localPosition + Vector3.left, base.transform);
		bool flag2 = Tiler.TilerTileHere(base.transform.localPosition + Vector3.right, base.transform);
		bool flag3 = Tiler.TilerTileHere(base.transform.localPosition + Vector3.up, base.transform);
		bool flag4 = Tiler.TilerTileHere(base.transform.localPosition + Vector3.down, base.transform);
		int tileNumber = Tiler.GetTileNumber(flag3, flag4, flag, flag2);
		this.SetTile(tileNumber);
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000942AC File Offset: 0x000924AC
	public void SetTile(int num)
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if (this.sprites.Length != 0 && component)
		{
			component.sprite = this.sprites[Mathf.Min(num, this.sprites.Length - 1)];
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x000942F0 File Offset: 0x000924F0
	public static void FadeAllTilers(Tiler.Type type)
	{
		foreach (Tiler tiler in Tiler.tilers)
		{
			if (type == Tiler.Type.Unspecified || tiler.type == type)
			{
				Animator component = tiler.GetComponent<Animator>();
				if (component)
				{
					component.Play("fadeOutDelete");
				}
			}
		}
	}

	// Token: 0x04000C24 RID: 3108
	public static List<Tiler> tilers = new List<Tiler>();

	// Token: 0x04000C25 RID: 3109
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x04000C26 RID: 3110
	public Tiler.Type type;

	// Token: 0x02000446 RID: 1094
	public enum Type
	{
		// Token: 0x04001985 RID: 6533
		Unspecified,
		// Token: 0x04001986 RID: 6534
		MovementSpace,
		// Token: 0x04001987 RID: 6535
		SpecialReorganizationArea
	}
}
