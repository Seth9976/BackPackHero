using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x02000142 RID: 322
public class Overworld_FollowPath : MonoBehaviour
{
	// Token: 0x06000C61 RID: 3169 RVA: 0x0007F8FE File Offset: 0x0007DAFE
	private void Start()
	{
		this.tilemap = GameObject.FindGameObjectWithTag("Overworld_Tilemap").GetComponent<Tilemap>();
		this.grid = this.tilemap.GetComponentInParent<Grid>();
		this.animator = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0007F934 File Offset: 0x0007DB34
	private void Update()
	{
		if (this.paths.Count > 0)
		{
			Vector2 vector = this.paths[0];
			if (Vector2.Distance(base.transform.position, vector) > 0.1f)
			{
				base.transform.position = Vector2.MoveTowards(base.transform.position, vector, this.speed * Time.deltaTime);
				this.isMoving = true;
				return;
			}
			this.isMoving = false;
			this.paths.RemoveAt(0);
			if (this.paths.Count > 0)
			{
				vector = this.paths[0];
				this.SetAnimation(vector);
			}
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0007F9EC File Offset: 0x0007DBEC
	public void SetAnimation(Vector2 dest)
	{
		if (dest.y > base.transform.position.y + 0.25f)
		{
			this.animator.Play("move_up");
			return;
		}
		if (dest.y < base.transform.position.y - 0.25f)
		{
			this.animator.Play("move_down");
			return;
		}
		if (dest.x < base.transform.position.x)
		{
			this.animator.Play("move_left");
			return;
		}
		if (dest.x > base.transform.position.x)
		{
			this.animator.Play("move_right");
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0007FAA8 File Offset: 0x0007DCA8
	public void SetNewPath(List<Vector2> newPath)
	{
		this.paths = newPath;
		if (this.paths.Count > 0)
		{
			this.SetAnimation(this.paths[0]);
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0007FAD4 File Offset: 0x0007DCD4
	private int Acceptable(PathFinding.Location location, bool isDest)
	{
		if (Vector2.Distance(location.position, base.transform.position) > 500f)
		{
			return -1;
		}
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(location.position, Vector2.zero))
		{
			if (raycastHit2D.collider.gameObject)
			{
				return -1;
			}
		}
		TileBase tile = this.tilemap.GetTile(this.grid.WorldToCell(location.position));
		if (tile && tile.name.Contains("path"))
		{
			return 1;
		}
		return 5;
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0007FB80 File Offset: 0x0007DD80
	public void FindNewPath(Vector2 dest)
	{
		Vector2 vector = Vector2Int.RoundToInt(base.transform.position);
		dest = Vector2Int.RoundToInt(dest);
		List<Vector2> list = new List<Vector2>();
		if (PathFinding.FindPathRanked(vector, dest, new Func<PathFinding.Location, bool, int>(this.Acceptable), out list, new string[] { "n", "s", "e", "w", "ne", "nw", "se", "sw" }))
		{
			list.Add(dest);
			list.RemoveAt(0);
			this.SetNewPath(list);
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0007FC2B File Offset: 0x0007DE2B
	public bool ReachedDestination()
	{
		return this.paths.Count <= 0;
	}

	// Token: 0x04000A05 RID: 2565
	[SerializeField]
	private List<Vector2> paths = new List<Vector2>();

	// Token: 0x04000A06 RID: 2566
	[SerializeField]
	private float speed = 10f;

	// Token: 0x04000A07 RID: 2567
	[SerializeField]
	private bool isMoving;

	// Token: 0x04000A08 RID: 2568
	private Animator animator;

	// Token: 0x04000A09 RID: 2569
	private Tilemap tilemap;

	// Token: 0x04000A0A RID: 2570
	private Grid grid;
}
