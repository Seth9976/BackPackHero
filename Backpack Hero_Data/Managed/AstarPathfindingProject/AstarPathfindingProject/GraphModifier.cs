using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200003A RID: 58
	[ExecuteInEditMode]
	public abstract class GraphModifier : VersionedMonoBehaviour
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		protected static List<T> GetModifiersOfType<T>() where T : GraphModifier
		{
			GraphModifier graphModifier = GraphModifier.root;
			List<T> list = new List<T>();
			while (graphModifier != null)
			{
				T t = graphModifier as T;
				if (t != null)
				{
					list.Add(t);
				}
				graphModifier = graphModifier.next;
			}
			return list;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public static void FindAllModifiers()
		{
			GraphModifier[] array = Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled)
				{
					array[i].OnEnable();
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			GraphModifier graphModifier = GraphModifier.root;
			if (type <= GraphModifier.EventType.PreUpdate)
			{
				switch (type)
				{
				case GraphModifier.EventType.PostScan:
					while (graphModifier != null)
					{
						graphModifier.OnPostScan();
						graphModifier = graphModifier.next;
					}
					return;
				case GraphModifier.EventType.PreScan:
					while (graphModifier != null)
					{
						graphModifier.OnPreScan();
						graphModifier = graphModifier.next;
					}
					return;
				case (GraphModifier.EventType)3:
					break;
				case GraphModifier.EventType.LatePostScan:
					while (graphModifier != null)
					{
						graphModifier.OnLatePostScan();
						graphModifier = graphModifier.next;
					}
					return;
				default:
					if (type != GraphModifier.EventType.PreUpdate)
					{
						return;
					}
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPreUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
			}
			else
			{
				if (type == GraphModifier.EventType.PostUpdate)
				{
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPostUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
				if (type != GraphModifier.EventType.PostCacheLoad)
				{
					return;
				}
				while (graphModifier != null)
				{
					graphModifier.OnPostCacheLoad();
					graphModifier = graphModifier.next;
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000EB3B File Offset: 0x0000CD3B
		protected virtual void OnEnable()
		{
			this.RemoveFromLinkedList();
			this.AddToLinkedList();
			this.ConfigureUniqueID();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000EB4F File Offset: 0x0000CD4F
		protected virtual void OnDisable()
		{
			this.RemoveFromLinkedList();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000EB57 File Offset: 0x0000CD57
		protected override void Awake()
		{
			base.Awake();
			this.ConfigureUniqueID();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000EB68 File Offset: 0x0000CD68
		private void ConfigureUniqueID()
		{
			GraphModifier graphModifier;
			if (GraphModifier.usedIDs.TryGetValue(this.uniqueID, out graphModifier) && graphModifier != this)
			{
				this.Reset();
			}
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		private void AddToLinkedList()
		{
			if (GraphModifier.root == null)
			{
				GraphModifier.root = this;
				return;
			}
			this.next = GraphModifier.root;
			GraphModifier.root.prev = this;
			GraphModifier.root = this;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		private void RemoveFromLinkedList()
		{
			if (GraphModifier.root == this)
			{
				GraphModifier.root = this.next;
				if (GraphModifier.root != null)
				{
					GraphModifier.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000EC67 File Offset: 0x0000CE67
		protected virtual void OnDestroy()
		{
			GraphModifier.usedIDs.Remove(this.uniqueID);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000EC7A File Offset: 0x0000CE7A
		public virtual void OnPostScan()
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public virtual void OnPreScan()
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000EC82 File Offset: 0x0000CE82
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000EC88 File Offset: 0x0000CE88
		protected override void Reset()
		{
			base.Reset();
			ulong num = (ulong)((long)Random.Range(0, int.MaxValue));
			ulong num2 = (ulong)((ulong)((long)Random.Range(0, int.MaxValue)) << 32);
			this.uniqueID = num | num2;
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x040001BB RID: 443
		private static GraphModifier root;

		// Token: 0x040001BC RID: 444
		private GraphModifier prev;

		// Token: 0x040001BD RID: 445
		private GraphModifier next;

		// Token: 0x040001BE RID: 446
		[SerializeField]
		[HideInInspector]
		protected ulong uniqueID;

		// Token: 0x040001BF RID: 447
		protected static Dictionary<ulong, GraphModifier> usedIDs = new Dictionary<ulong, GraphModifier>();

		// Token: 0x02000100 RID: 256
		public enum EventType
		{
			// Token: 0x0400064F RID: 1615
			PostScan = 1,
			// Token: 0x04000650 RID: 1616
			PreScan,
			// Token: 0x04000651 RID: 1617
			LatePostScan = 4,
			// Token: 0x04000652 RID: 1618
			PreUpdate = 8,
			// Token: 0x04000653 RID: 1619
			PostUpdate = 16,
			// Token: 0x04000654 RID: 1620
			PostCacheLoad = 32
		}
	}
}
