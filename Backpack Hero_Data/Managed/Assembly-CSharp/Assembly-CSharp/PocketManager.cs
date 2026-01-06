using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class PocketManager : MonoBehaviour
{
	// Token: 0x0600095E RID: 2398 RVA: 0x00060A51 File Offset: 0x0005EC51
	private void Awake()
	{
		if (PocketManager.main != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		PocketManager.main = this;
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00060A72 File Offset: 0x0005EC72
	private void OnDestroy()
	{
		if (PocketManager.main == this)
		{
			PocketManager.main = null;
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00060A87 File Offset: 0x0005EC87
	private void Start()
	{
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00060A89 File Offset: 0x0005EC89
	private void Update()
	{
		if (GameManager.main.player.characterName != Character.CharacterName.Satchel)
		{
			return;
		}
		if (DigitalCursor.main.GetInputDown("CharacterAction"))
		{
			this.NextPocket();
		}
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00060AB8 File Offset: 0x0005ECB8
	private void NextPocket()
	{
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(DigitalCursor.main.transform.position, Vector2.zero))
		{
			GridSquare component = raycastHit2D.collider.gameObject.GetComponent<GridSquare>();
			if (component)
			{
				PocketManager.Pocket pocket = PocketManager.GetPocket(component);
				if (pocket != null)
				{
					int num = this.pockets.IndexOf(pocket) + 1;
					int count = this.pockets.Count;
					return;
				}
			}
		}
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00060B3D File Offset: 0x0005ED3D
	public int GetNumOfPockets()
	{
		return this.pockets.Count;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00060B4A File Offset: 0x0005ED4A
	public static int GetSpacesInPocket(PocketManager.Pocket pocket)
	{
		return pocket.grids.Count;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00060B58 File Offset: 0x0005ED58
	public static PocketManager.Pocket GetPocket(GridSquare gridSquare)
	{
		foreach (PocketManager.Pocket pocket in PocketManager.main.pockets)
		{
			if (pocket.grids.Contains(gridSquare))
			{
				return pocket;
			}
		}
		return null;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00060BC0 File Offset: 0x0005EDC0
	public static List<PocketManager.Pocket> GetPocketsFromItem(Item2 item)
	{
		List<GridSquare> list = new List<GridSquare>();
		item.FindItemsAndGridsinArea(new List<Item2>(), list, new List<Item2.Area> { Item2.Area.myPlaySpace }, Item2.AreaDistance.all, null, null, null, true, false, true);
		List<PocketManager.Pocket> list2 = new List<PocketManager.Pocket>();
		foreach (GridSquare gridSquare in list)
		{
			PocketManager.Pocket pocket = PocketManager.GetPocket(gridSquare);
			if (pocket != null && !list2.Contains(pocket))
			{
				list2.Add(pocket);
			}
		}
		return list2;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00060C4C File Offset: 0x0005EE4C
	public void DeterminePockets()
	{
		if (this.pocketSearchCoroutine != null)
		{
			base.StopCoroutine(this.pocketSearchCoroutine);
		}
		this.pocketSearchCoroutine = base.StartCoroutine(this.PocketInternal());
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00060C74 File Offset: 0x0005EE74
	private bool GridSpaceHere(PathFinding.Location location, bool isDest)
	{
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			if (gridSquare && gridSquare.gameObject && Vector2.Distance(gridSquare.transform.position, location.position) <= 0.5f)
			{
				GameObject gameObject = gridSquare.gameObject;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00060D04 File Offset: 0x0005EF04
	private IEnumerator PocketInternal()
	{
		ItemPouch.CloseAllPouches();
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.pockets = new List<PocketManager.Pocket>();
		int num = 0;
		foreach (GridSquare gridSquare in GridSquare.allGrids)
		{
			if (this.FindPocketForThisGrid(gridSquare) == null)
			{
				List<PathFinding.Location> list;
				PathFinding.FindAvailableSpaces(gridSquare.transform.position, 10, new Func<PathFinding.Location, bool, bool>(this.GridSpaceHere), out list, null);
				PocketManager.Pocket pocket = new PocketManager.Pocket();
				pocket.number = num;
				num++;
				foreach (PathFinding.Location location in list)
				{
					foreach (GridSquare gridSquare2 in GridSquare.allGrids)
					{
						if (Vector2.Distance(gridSquare2.transform.position, location.position) < 0.5f)
						{
							pocket.grids.Add(gridSquare2);
						}
					}
				}
				if (pocket.grids.Count != 0)
				{
					this.pockets.Add(pocket);
				}
			}
		}
		this.pocketSearchCoroutine = null;
		yield break;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00060D14 File Offset: 0x0005EF14
	public PocketManager.Pocket FindPocketForThisGrid(GridSquare gridSquare)
	{
		foreach (PocketManager.Pocket pocket in this.pockets)
		{
			if (pocket.grids.Contains(gridSquare))
			{
				return pocket;
			}
		}
		return null;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00060D78 File Offset: 0x0005EF78
	public List<Vector2> GetVector2s(PocketManager.Pocket pocket)
	{
		List<Vector2> list = new List<Vector2>();
		foreach (GridSquare gridSquare in pocket.grids)
		{
			if (gridSquare)
			{
				list.Add(gridSquare.transform.position);
			}
		}
		return list;
	}

	// Token: 0x0400077A RID: 1914
	public static PocketManager main;

	// Token: 0x0400077B RID: 1915
	private Coroutine pocketSearchCoroutine;

	// Token: 0x0400077C RID: 1916
	[SerializeField]
	private List<PocketManager.Pocket> pockets = new List<PocketManager.Pocket>();

	// Token: 0x02000385 RID: 901
	[Serializable]
	public class Pocket
	{
		// Token: 0x0400153F RID: 5439
		public List<GridSquare> grids = new List<GridSquare>();

		// Token: 0x04001540 RID: 5440
		public int number;
	}
}
