using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class ManaFlow : MonoBehaviour
{
	// Token: 0x06000923 RID: 2339 RVA: 0x0005F084 File Offset: 0x0005D284
	public static bool ManaFlowsExist(List<ManaFlow> ignoredManaFlows)
	{
		foreach (ManaFlow manaFlow in ManaFlow.manaFlows)
		{
			if (!ignoredManaFlows.Contains(manaFlow))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0005F0E0 File Offset: 0x0005D2E0
	private void Awake()
	{
		ManaFlow.manaFlows.Add(this);
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0005F0ED File Offset: 0x0005D2ED
	private void OnDestroy()
	{
		ManaFlow.manaFlows.Remove(this);
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0005F0FB File Offset: 0x0005D2FB
	private void Start()
	{
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0005F0FD File Offset: 0x0005D2FD
	private void Update()
	{
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0005F0FF File Offset: 0x0005D2FF
	public IEnumerator FlowManaAnimation(List<Vector2> vecs)
	{
		vecs.RemoveAt(vecs.Count - 1);
		while (vecs.Count > 0)
		{
			Vector2 dest = vecs[vecs.Count - 1];
			while (Vector2.Distance(base.transform.position, dest) > 0.001f)
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, new Vector3(dest.x, dest.y, base.transform.position.z), 10f * Time.deltaTime);
				yield return null;
			}
			base.transform.position = dest;
			List<Item2> list = new List<Item2>();
			Item2.TestAtVectorPublic(list, new List<GridSquare>(), dest);
			if (vecs.Count > 1)
			{
				foreach (Item2 item in list)
				{
					GameFlowManager.main.ConsiderAllEffectsPublicList(Item2.Trigger.ActionTrigger.whenManaFlowsThrough, new List<Item2> { item }, null, null, true, true);
				}
			}
			vecs.RemoveAt(vecs.Count - 1);
			dest = default(Vector2);
		}
		ManaFlow.manaFlows.Remove(this);
		yield return new WaitForSeconds(0.5f);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000741 RID: 1857
	private static List<ManaFlow> manaFlows = new List<ManaFlow>();
}
