using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class EventLocksDungeonMovement : MonoBehaviour
{
	// Token: 0x06000176 RID: 374 RVA: 0x00009B46 File Offset: 0x00007D46
	private void Awake()
	{
		EventLocksDungeonMovement.instances.Add(this);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00009B53 File Offset: 0x00007D53
	private void OnDestroy()
	{
		EventLocksDungeonMovement.instances.Remove(this);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00009B64 File Offset: 0x00007D64
	public static bool IsLocked()
	{
		using (List<EventLocksDungeonMovement>.Enumerator enumerator = EventLocksDungeonMovement.instances.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.gameObject.activeSelf)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00009BC4 File Offset: 0x00007DC4
	private void Start()
	{
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00009BC6 File Offset: 0x00007DC6
	private void Update()
	{
	}

	// Token: 0x040000F3 RID: 243
	private static List<EventLocksDungeonMovement> instances = new List<EventLocksDungeonMovement>();
}
