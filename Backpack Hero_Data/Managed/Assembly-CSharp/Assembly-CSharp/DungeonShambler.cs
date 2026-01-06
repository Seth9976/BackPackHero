using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class DungeonShambler : MonoBehaviour
{
	// Token: 0x06000559 RID: 1369 RVA: 0x0003498C File Offset: 0x00032B8C
	private void Start()
	{
		this.dungeonPlayer = DungeonPlayer.main;
		this.destination = base.transform.position;
		if (!this.asleep)
		{
			this.PlayAwakeAnimation();
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.CompareTag("DestroyOnLoad"))
			{
				Object.Destroy(transform.gameObject);
			}
		}
		Object.Instantiate<GameObject>(this.effectPrefab, base.transform.position, Quaternion.identity, base.transform);
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00034A48 File Offset: 0x00032C48
	public void ConsiderWaking()
	{
		this.start = base.transform.position;
		if (PathFinding.FindPath(this.start, this.dungeonPlayer.transform.position, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpaceWakeShambler), out this.outPath, null))
		{
			base.GetComponent<SpriteRenderer>().sprite = this.awakeSprite;
			SoundManager.main.PlaySFX("wheelSFX");
			this.asleep = false;
			this.PlayAwakeAnimation();
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00034AD0 File Offset: 0x00032CD0
	private void PlayAwakeAnimation()
	{
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		if (componentInChildren)
		{
			componentInChildren.Play("shamblerAwake", 0, 0f);
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00034AFD File Offset: 0x00032CFD
	private void Update()
	{
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00034B00 File Offset: 0x00032D00
	private bool AcceptableSpaceWakeShambler(PathFinding.Location location, bool isDest)
	{
		if (location.path.Count >= 2)
		{
			Vector2 vector = location.position - location.path[location.path.Count - 1];
			for (int i = 1; i < location.path.Count; i++)
			{
				if (location.path[i] - location.path[i - 1] != vector)
				{
					return false;
				}
			}
		}
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(location.position);
		for (int j = 0; j < objectsAtVector.Length; j++)
		{
			DungeonRoom component = objectsAtVector[j].GetComponent<DungeonRoom>();
			if (component)
			{
				return location.path.Count <= 0 || ((location.position.x <= location.path[location.path.Count - 1].x || component.left) && (location.position.x >= location.path[location.path.Count - 1].x || component.right) && (location.position.y <= location.path[location.path.Count - 1].y || component.down) && (location.position.y >= location.path[location.path.Count - 1].y || component.up));
			}
		}
		return false;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00034C9C File Offset: 0x00032E9C
	private bool AcceptableSpaceShambler(PathFinding.Location location, bool isDest)
	{
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(location.position);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			DungeonRoom component = objectsAtVector[i].GetComponent<DungeonRoom>();
			if (component)
			{
				return location.path.Count <= 0 || ((location.position.x <= location.path[location.path.Count - 1].x || component.left) && (location.position.x >= location.path[location.path.Count - 1].x || component.right) && (location.position.y <= location.path[location.path.Count - 1].y || component.down) && (location.position.y >= location.path[location.path.Count - 1].y || component.up));
			}
		}
		return false;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00034DC0 File Offset: 0x00032FC0
	public void SetPath()
	{
		base.transform.localPosition = new Vector3(0f, 0f, -1.1f);
		this.start = base.transform.position;
		bool flag = PathFinding.FindPath(this.start, this.dungeonPlayer.transform.position, new Func<PathFinding.Location, bool, bool>(this.AcceptableSpaceShambler), out this.outPath, null);
		if (flag && this.outPath.Count >= 2)
		{
			this.destination = this.outPath[1];
			this.active = true;
			return;
		}
		if (flag && this.outPath.Count == 1)
		{
			this.destination = this.dungeonPlayer.transform.position;
			this.active = true;
			return;
		}
		this.destination = base.transform.position;
		this.active = false;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00034EB2 File Offset: 0x000330B2
	public IEnumerator Move()
	{
		if (!this.active)
		{
			yield break;
		}
		base.transform.localPosition = new Vector3(0f, 0f, -1.1f);
		this.SetNewRoom(this.destination);
		this.destination = new Vector3(0f, 0f, -1.1f);
		while (Vector2.Distance(base.transform.localPosition, this.destination) > 0.1f)
		{
			yield return null;
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.destination, 10f * Time.deltaTime);
		}
		base.transform.localPosition = this.destination;
		base.transform.localPosition = new Vector3(0f, 0f, -1.1f);
		yield break;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00034EC4 File Offset: 0x000330C4
	public void SetNewRoom(Vector2 destination)
	{
		GameObject[] objectsAtVector = PathFinding.GetObjectsAtVector(destination);
		for (int i = 0; i < objectsAtVector.Length; i++)
		{
			DungeonRoom component = objectsAtVector[i].GetComponent<DungeonRoom>();
			if (component)
			{
				base.transform.SetParent(component.transform);
			}
		}
	}

	// Token: 0x04000410 RID: 1040
	[SerializeField]
	private Sprite awakeSprite;

	// Token: 0x04000411 RID: 1041
	public Vector2 start;

	// Token: 0x04000412 RID: 1042
	public Vector2 destination;

	// Token: 0x04000413 RID: 1043
	public bool active;

	// Token: 0x04000414 RID: 1044
	private const float currentSpeed = 10f;

	// Token: 0x04000415 RID: 1045
	private DungeonPlayer dungeonPlayer;

	// Token: 0x04000416 RID: 1046
	public bool asleep = true;

	// Token: 0x04000417 RID: 1047
	public List<Vector2> outPath = new List<Vector2>();

	// Token: 0x04000418 RID: 1048
	[SerializeField]
	private GameObject effectPrefab;
}
