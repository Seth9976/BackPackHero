using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class ConnectionManager : MonoBehaviour
{
	// Token: 0x0600008F RID: 143 RVA: 0x000055F3 File Offset: 0x000037F3
	private void Awake()
	{
		if (ConnectionManager.main == null)
		{
			ConnectionManager.main = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00005614 File Offset: 0x00003814
	private void OnDestroy()
	{
		if (ConnectionManager.main == this)
		{
			ConnectionManager.main = null;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000562C File Offset: 0x0000382C
	public List<ConnectionManager.ManaNetwork> FindManaNetworks(Item2 item)
	{
		List<ConnectionManager.ManaNetwork> list = new List<ConnectionManager.ManaNetwork>();
		foreach (ConnectionManager.ManaNetwork manaNetwork in this.manaNetworks)
		{
			using (List<Item2>.Enumerator enumerator2 = manaNetwork.connectedObjects.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == item)
					{
						list.Add(manaNetwork);
						break;
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000056CC File Offset: 0x000038CC
	public void DrawLine(List<Vector2> vecs, bool reverse, ItemMovement im)
	{
		this.showingLinesOfIM = im;
		GameObject gameObject = Object.Instantiate<GameObject>(this.linePrefab, base.transform);
		LineRenderer component = gameObject.GetComponent<LineRenderer>();
		if (reverse)
		{
			vecs.Reverse();
		}
		component.positionCount = vecs.Count;
		for (int i = 0; i < vecs.Count; i++)
		{
			component.SetPosition(i, vecs[i]);
		}
		this.lines.Add(gameObject);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00005740 File Offset: 0x00003940
	public void RemoveAllLines()
	{
		this.showingLinesOfIM = null;
		foreach (GameObject gameObject in this.lines)
		{
			Object.Destroy(gameObject);
		}
		this.lines.Clear();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000057A4 File Offset: 0x000039A4
	public void ConsiderReplacingLines()
	{
		ItemMovement itemMovement = this.showingLinesOfIM;
		if (this.showingLinesOfIM != null)
		{
			this.RemoveAllLines();
			itemMovement.ResetHighlights();
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000057D4 File Offset: 0x000039D4
	public List<ManaStone> FindManaStonesForItem(Item2 item, int powerToAddOrRemove)
	{
		List<ConnectionManager.ManaNetwork> list = this.FindManaNetworks(item);
		List<ManaStone> list2 = new List<ManaStone>();
		foreach (ConnectionManager.ManaNetwork manaNetwork in list)
		{
			foreach (ManaStone manaStone in manaNetwork.connectedManaStones)
			{
				if (manaStone && manaStone.item && !manaStone.item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
				{
					list2.Add(manaStone);
				}
			}
		}
		list2 = ItemMovement.SortByHighestLeftGridPosition(list2.ConvertAll<ItemMovement>((ManaStone x) => x.itemMovement)).ConvertAll<ManaStone>((ItemMovement x) => x.GetComponent<ManaStone>());
		int num = 0;
		List<ManaStone> list3 = new List<ManaStone>();
		while (powerToAddOrRemove != 0 && list2.Count > 0)
		{
			ManaStone manaStone2 = list2[0];
			if (powerToAddOrRemove > 0 && manaStone2.currentPower + num >= manaStone2.maxPower)
			{
				list2.Remove(manaStone2);
				num = 0;
			}
			else if (powerToAddOrRemove < 0 && manaStone2.currentPower - num <= 0)
			{
				list2.Remove(manaStone2);
				num = 0;
			}
			else if (powerToAddOrRemove < 0)
			{
				if (!list3.Contains(manaStone2))
				{
					list3.Add(manaStone2);
				}
				num++;
				powerToAddOrRemove++;
			}
			else
			{
				if (!list3.Contains(manaStone2))
				{
					list3.Add(manaStone2);
				}
				num++;
				powerToAddOrRemove--;
			}
		}
		return list3;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00005984 File Offset: 0x00003B84
	public void ChangePowerInManaNetwork(Item2 item, int powerToAddOrRemove)
	{
		List<ConnectionManager.ManaNetwork> list = this.FindManaNetworks(item);
		List<ManaStone> list2 = new List<ManaStone>();
		foreach (ConnectionManager.ManaNetwork manaNetwork in list)
		{
			foreach (ManaStone manaStone in manaNetwork.connectedManaStones)
			{
				if (manaStone && manaStone.item && !manaStone.item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
				{
					list2.Add(manaStone);
				}
			}
		}
		list2 = ItemMovement.SortByHighestLeftGridPosition(list2.ConvertAll<ItemMovement>((ManaStone x) => x.itemMovement)).ConvertAll<ManaStone>((ItemMovement x) => x.GetComponent<ManaStone>());
		ManaStone manaStone2 = null;
		while (powerToAddOrRemove != 0 && list2.Count > 0)
		{
			ManaStone manaStone3 = list2[0];
			if (powerToAddOrRemove > 0 && manaStone3.currentPower == manaStone3.maxPower)
			{
				list2.Remove(manaStone3);
			}
			else if (powerToAddOrRemove < 0 && manaStone3.currentPower == 0)
			{
				list2.Remove(manaStone3);
			}
			else
			{
				if (manaStone2 != manaStone3)
				{
					manaStone2 = manaStone3;
					if (powerToAddOrRemove > 0)
					{
						GameFlowManager.main.FlowMana(item, manaStone3.transform, true);
					}
					else
					{
						GameFlowManager.main.FlowMana(item, manaStone3.transform, false);
					}
				}
				if (powerToAddOrRemove < 0)
				{
					manaStone3.currentPower--;
					powerToAddOrRemove++;
				}
				else
				{
					manaStone3.currentPower++;
					powerToAddOrRemove--;
				}
			}
		}
		this.ConsiderReplacingLines();
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00005B54 File Offset: 0x00003D54
	public int SumAvailableMana(Item2 item)
	{
		List<ConnectionManager.ManaNetwork> list = this.FindManaNetworks(item);
		return this.SumAvailableMana(list);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00005B70 File Offset: 0x00003D70
	public int SumAvailableMana(List<ConnectionManager.ManaNetwork> manaNetworks)
	{
		int num = 0;
		foreach (ConnectionManager.ManaNetwork manaNetwork in manaNetworks)
		{
			foreach (ManaStone manaStone in manaNetwork.connectedManaStones)
			{
				if (manaStone && manaStone.item && !manaStone.item.CheckForStatusEffect(Item2.ItemStatusEffect.Type.disabled))
				{
					num += manaStone.currentPower;
				}
			}
		}
		return num;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00005C20 File Offset: 0x00003E20
	public ConnectionManager.ManaNetwork FindNetworkForManaStone(ManaStone manaStone)
	{
		foreach (ConnectionManager.ManaNetwork manaNetwork in this.manaNetworks)
		{
			using (List<ManaStone>.Enumerator enumerator2 = manaNetwork.connectedManaStones.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == manaStone)
					{
						return manaNetwork;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00005CB8 File Offset: 0x00003EB8
	public void FindManaNetworks()
	{
		if (this.findManaNetworksRoutine != null)
		{
			base.StopCoroutine(this.findManaNetworksRoutine);
		}
		this.findManaNetworksRoutine = base.StartCoroutine(this.FindManaNetworksRoutine());
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00005CE0 File Offset: 0x00003EE0
	private IEnumerator FindManaNetworksRoutine()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.manaNetworks.Clear();
		List<ManaStone> list = new List<ManaStone>();
		using (List<Item2>.Enumerator enumerator = Item2.allItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Item2 item = enumerator.Current;
				if (Item2.ShareItemTypes(item.itemType, new List<Item2.ItemType> { Item2.ItemType.ManaStone }) && item.itemMovement && item.itemMovement.inGrid && !item.itemMovement.inPouch)
				{
					ManaStone component = item.GetComponent<ManaStone>();
					list.Add(component);
				}
			}
			goto IL_01B7;
		}
		IL_00F8:
		ConnectionManager.ManaNetwork manaNetwork = this.FindNetworkForManaStone(list[0]);
		if (!manaNetwork)
		{
			manaNetwork = new ConnectionManager.ManaNetwork();
			this.manaNetworks.Add(manaNetwork);
			foreach (PathFinding.TransformAndPath transformAndPath in GameFlowManager.main.FindAllItemsOfTypeConnected(list[0].GetComponent<Item2>()))
			{
				Item2 component2 = transformAndPath.t.GetComponent<Item2>();
				if (component2.itemType.Contains(Item2.ItemType.ManaStone))
				{
					ManaStone component3 = component2.GetComponent<ManaStone>();
					if (component3)
					{
						manaNetwork.connectedManaStones.Add(component3);
					}
				}
				manaNetwork.connectedObjects.Add(component2);
			}
		}
		list.RemoveAt(0);
		IL_01B7:
		if (list.Count <= 0)
		{
			yield break;
		}
		goto IL_00F8;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00005CF0 File Offset: 0x00003EF0
	private static Transform AcceptableItem(PathFinding.Location location)
	{
		foreach (GameObject gameObject in GridObject.GetItemsAtPosition(location.position).ConvertAll<GameObject>((GridObject x) => x.gameObject))
		{
			Item2 componentInParent = gameObject.GetComponentInParent<Item2>();
			ItemMovement componentInParent2 = gameObject.GetComponentInParent<ItemMovement>();
			if (componentInParent && componentInParent2 && componentInParent2.inGrid && Item2.ShareItemTypes(componentInParent.itemType, ConnectionManager.validTypes))
			{
				return componentInParent.transform;
			}
		}
		return null;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00005DA8 File Offset: 0x00003FA8
	public static List<PathFinding.TransformAndPath> FindAllItemsOfTypeConnected(Item2 activeItem, List<Item2.ItemType> types)
	{
		ConnectionManager.validTypes = types;
		return ConnectionManager.FindAllItemsOfTypeConnected(activeItem);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00005DB8 File Offset: 0x00003FB8
	private static List<PathFinding.TransformAndPath> FindAllItemsOfTypeConnected(Item2 activeItem)
	{
		List<PathFinding.TransformAndPath> list = new List<PathFinding.TransformAndPath>();
		List<Vector2> list2 = activeItem.GetComponent<ItemMovement>().FindAllColliders();
		if (list2.Count > 0)
		{
			foreach (PathFinding.TransformAndPath transformAndPath in PathFinding.SearchForItems(activeItem, list2[0], 30, new Func<PathFinding.Location, Transform>(ConnectionManager.AcceptableItem), new Func<PathFinding.Location, Transform>(ConnectionManager.AcceptableItem), new string[] { "n", "s", "e", "w" }))
			{
				bool flag = false;
				using (List<PathFinding.TransformAndPath>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.t == transformAndPath.t)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(transformAndPath);
				}
			}
		}
		return list;
	}

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private GameObject linePrefab;

	// Token: 0x04000050 RID: 80
	private List<GameObject> lines = new List<GameObject>();

	// Token: 0x04000051 RID: 81
	public static ConnectionManager main;

	// Token: 0x04000052 RID: 82
	private ItemMovement showingLinesOfIM;

	// Token: 0x04000053 RID: 83
	private static List<Item2.ItemType> validTypes;

	// Token: 0x04000054 RID: 84
	[SerializeField]
	private List<ConnectionManager.ManaNetwork> manaNetworks = new List<ConnectionManager.ManaNetwork>();

	// Token: 0x04000055 RID: 85
	private Coroutine findManaNetworksRoutine;

	// Token: 0x02000247 RID: 583
	[Serializable]
	public class ManaNetwork
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x000AE8BB File Offset: 0x000ACABB
		public static implicit operator bool(ConnectionManager.ManaNetwork manaNetwork)
		{
			return manaNetwork != null;
		}

		// Token: 0x04000EBF RID: 3775
		public List<ManaStone> connectedManaStones = new List<ManaStone>();

		// Token: 0x04000EC0 RID: 3776
		public List<Item2> connectedObjects = new List<Item2>();
	}
}
