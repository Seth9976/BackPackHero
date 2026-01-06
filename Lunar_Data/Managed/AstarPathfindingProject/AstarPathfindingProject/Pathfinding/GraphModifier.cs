using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000069 RID: 105
	[ExecuteInEditMode]
	public abstract class GraphModifier : VersionedMonoBehaviour
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00012160 File Offset: 0x00010360
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

		// Token: 0x0600039D RID: 925 RVA: 0x000121B0 File Offset: 0x000103B0
		public static void FindAllModifiers()
		{
			GraphModifier[] array = UnityCompatibility.FindObjectsByTypeSorted<GraphModifier>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled && array[i].next == null)
				{
					array[i].enabled = false;
					array[i].enabled = true;
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00012200 File Offset: 0x00010400
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			try
			{
				GraphModifier graphModifier = GraphModifier.root;
				if (type <= GraphModifier.EventType.PostUpdate)
				{
					switch (type)
					{
					case GraphModifier.EventType.PostScan:
						while (graphModifier != null)
						{
							graphModifier.OnPostScan();
							graphModifier = graphModifier.next;
						}
						break;
					case GraphModifier.EventType.PreScan:
						while (graphModifier != null)
						{
							graphModifier.OnPreScan();
							graphModifier = graphModifier.next;
						}
						break;
					case (GraphModifier.EventType)3:
						break;
					case GraphModifier.EventType.LatePostScan:
						while (graphModifier != null)
						{
							graphModifier.OnLatePostScan();
							graphModifier = graphModifier.next;
						}
						break;
					default:
						if (type != GraphModifier.EventType.PreUpdate)
						{
							if (type == GraphModifier.EventType.PostUpdate)
							{
								while (graphModifier != null)
								{
									graphModifier.OnGraphsPostUpdate();
									graphModifier = graphModifier.next;
								}
							}
						}
						else
						{
							while (graphModifier != null)
							{
								graphModifier.OnGraphsPreUpdate();
								graphModifier = graphModifier.next;
							}
						}
						break;
					}
				}
				else if (type != GraphModifier.EventType.PostCacheLoad)
				{
					if (type != GraphModifier.EventType.PostUpdateBeforeAreaRecalculation)
					{
						if (type == GraphModifier.EventType.PostGraphLoad)
						{
							while (graphModifier != null)
							{
								graphModifier.OnPostGraphLoad();
								graphModifier = graphModifier.next;
							}
						}
					}
					else
					{
						while (graphModifier != null)
						{
							graphModifier.OnGraphsPostUpdateBeforeAreaRecalculation();
							graphModifier = graphModifier.next;
						}
					}
				}
				else
				{
					while (graphModifier != null)
					{
						graphModifier.OnPostCacheLoad();
						graphModifier = graphModifier.next;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001235C File Offset: 0x0001055C
		protected virtual void OnEnable()
		{
			this.RemoveFromLinkedList();
			this.AddToLinkedList();
			this.ConfigureUniqueID();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012370 File Offset: 0x00010570
		protected virtual void OnDisable()
		{
			this.RemoveFromLinkedList();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00012378 File Offset: 0x00010578
		protected override void Awake()
		{
			base.Awake();
			this.ConfigureUniqueID();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012388 File Offset: 0x00010588
		private void ConfigureUniqueID()
		{
			GraphModifier graphModifier;
			if (GraphModifier.usedIDs.TryGetValue(this.uniqueID, out graphModifier) && graphModifier != this)
			{
				this.Reset();
			}
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000123C9 File Offset: 0x000105C9
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

		// Token: 0x060003A4 RID: 932 RVA: 0x000123FC File Offset: 0x000105FC
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

		// Token: 0x060003A5 RID: 933 RVA: 0x00012487 File Offset: 0x00010687
		protected virtual void OnDestroy()
		{
			GraphModifier.usedIDs.Remove(this.uniqueID);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnPostScan()
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnPreScan()
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnPostGraphLoad()
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void OnGraphsPostUpdateBeforeAreaRecalculation()
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001249C File Offset: 0x0001069C
		protected override void Reset()
		{
			base.Reset();
			ulong num = (ulong)((long)Random.Range(0, int.MaxValue));
			ulong num2 = (ulong)((ulong)((long)Random.Range(0, int.MaxValue)) << 32);
			this.uniqueID = num | num2;
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x04000260 RID: 608
		private static GraphModifier root;

		// Token: 0x04000261 RID: 609
		private GraphModifier prev;

		// Token: 0x04000262 RID: 610
		private GraphModifier next;

		// Token: 0x04000263 RID: 611
		[SerializeField]
		[HideInInspector]
		protected ulong uniqueID;

		// Token: 0x04000264 RID: 612
		protected static Dictionary<ulong, GraphModifier> usedIDs = new Dictionary<ulong, GraphModifier>();

		// Token: 0x0200006A RID: 106
		public enum EventType
		{
			// Token: 0x04000266 RID: 614
			PostScan = 1,
			// Token: 0x04000267 RID: 615
			PreScan,
			// Token: 0x04000268 RID: 616
			LatePostScan = 4,
			// Token: 0x04000269 RID: 617
			PreUpdate = 8,
			// Token: 0x0400026A RID: 618
			PostUpdate = 16,
			// Token: 0x0400026B RID: 619
			PostCacheLoad = 32,
			// Token: 0x0400026C RID: 620
			PostUpdateBeforeAreaRecalculation = 64,
			// Token: 0x0400026D RID: 621
			PostGraphLoad = 128
		}
	}
}
