using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Profiling;

namespace Pathfinding.Util
{
	// Token: 0x02000270 RID: 624
	[HelpURL("https://arongranberg.com/astar/documentation/stable/batchedevents.html")]
	public class BatchedEvents : VersionedMonoBehaviour
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x0005BD06 File Offset: 0x00059F06
		private void OnEnable()
		{
			if (BatchedEvents.instance == null)
			{
				BatchedEvents.instance = this;
			}
			BatchedEvents.instance != this;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0005BD27 File Offset: 0x00059F27
		private static void CreateInstance()
		{
			GameObject gameObject = new GameObject("Batch Helper");
			gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
			BatchedEvents.instance = gameObject.AddComponent<BatchedEvents>();
			Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0005BD4C File Offset: 0x00059F4C
		public static T Find<T, K>(K key, Func<T, K, bool> predicate) where T : class, IEntityIndex
		{
			Type typeFromHandle = typeof(T);
			for (int i = 0; i < BatchedEvents.data.Length; i++)
			{
				if (BatchedEvents.data[i].type == typeFromHandle)
				{
					T[] array = BatchedEvents.data[i].objects as T[];
					for (int j = 0; j < BatchedEvents.data[i].objectCount; j++)
					{
						if (predicate(array[j], key))
						{
							return array[j];
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0005BDE4 File Offset: 0x00059FE4
		public static void Remove<T>(T obj) where T : IEntityIndex
		{
			int num = obj.EntityIndex;
			if (num == 0)
			{
				return;
			}
			int num2 = ((num & 1069547520) >> 22) - 1;
			num &= -1069547521;
			if (BatchedEvents.isIterating && BatchedEvents.isIteratingOverTypeIndex == num2)
			{
				throw new Exception("Cannot add or remove entities during an event (Update/LateUpdate/...) that this helper initiated");
			}
			BatchedEvents.data[num2].Remove(num);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0005BE44 File Offset: 0x0005A044
		public static int GetComponents<T>(BatchedEvents.Event eventTypes, out TransformAccessArray transforms, out T[] components) where T : Component, IEntityIndex
		{
			if (BatchedEvents.instance == null)
			{
				BatchedEvents.CreateInstance();
			}
			int num = (int)(eventTypes * (BatchedEvents.Event)12582917);
			if (BatchedEvents.isIterating && BatchedEvents.isIteratingOverTypeIndex == num)
			{
				throw new Exception("Cannot add or remove entities during an event (Update/LateUpdate/...) that this helper initiated");
			}
			Type typeFromHandle = typeof(T);
			for (int i = 0; i < BatchedEvents.data.Length; i++)
			{
				if (BatchedEvents.data[i].type == typeFromHandle && BatchedEvents.data[i].variant == num)
				{
					transforms = BatchedEvents.data[i].transforms;
					components = BatchedEvents.data[i].objects as T[];
					return BatchedEvents.data[i].objectCount;
				}
			}
			transforms = default(TransformAccessArray);
			components = null;
			return 0;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0005BF16 File Offset: 0x0005A116
		public static bool Has<T>(T obj) where T : IEntityIndex
		{
			return obj.EntityIndex != 0;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0005BF28 File Offset: 0x0005A128
		public static void Add<T>(T obj, BatchedEvents.Event eventTypes, Action<T[], int> action, int archetypeVariant = 0) where T : Component, IEntityIndex
		{
			BatchedEvents.Add<T>(obj, eventTypes, null, action, archetypeVariant);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0005BF34 File Offset: 0x0005A134
		public static void Add<T>(T obj, BatchedEvents.Event eventTypes, Action<T[], int, TransformAccessArray, BatchedEvents.Event> action, int archetypeVariant = 0) where T : Component, IEntityIndex
		{
			BatchedEvents.Add<T>(obj, eventTypes, action, null, archetypeVariant);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0005BF40 File Offset: 0x0005A140
		private static void Add<T>(T obj, BatchedEvents.Event eventTypes, Action<T[], int, TransformAccessArray, BatchedEvents.Event> action1, Action<T[], int> action2, int archetypeVariant = 0) where T : Component, IEntityIndex
		{
			if (obj.EntityIndex != 0)
			{
				throw new ArgumentException("This object is already registered. Call Remove before adding the object again.");
			}
			if (BatchedEvents.instance == null)
			{
				BatchedEvents.CreateInstance();
			}
			archetypeVariant = (int)(eventTypes * (BatchedEvents.Event)12582917);
			if (BatchedEvents.isIterating && BatchedEvents.isIteratingOverTypeIndex == archetypeVariant)
			{
				throw new Exception("Cannot add or remove entities during an event (Update/LateUpdate/...) that this helper initiated");
			}
			Type type = obj.GetType();
			for (int i = 0; i < BatchedEvents.data.Length; i++)
			{
				if (BatchedEvents.data[i].type == type && BatchedEvents.data[i].variant == archetypeVariant)
				{
					BatchedEvents.data[i].Add(obj);
					return;
				}
			}
			Memory.Realloc<BatchedEvents.Archetype>(ref BatchedEvents.data, BatchedEvents.data.Length + 1);
			Action<T[], int, TransformAccessArray, BatchedEvents.Event> ac1 = action1;
			Action<T[], int> ac2 = action2;
			Action<object[], int, TransformAccessArray, BatchedEvents.Event> action3 = delegate(object[] objs, int count, TransformAccessArray tr, BatchedEvents.Event ev)
			{
				ac1((T[])objs, count, tr, ev);
			};
			Action<object[], int, TransformAccessArray, BatchedEvents.Event> action4 = delegate(object[] objs, int count, TransformAccessArray tr, BatchedEvents.Event ev)
			{
				ac2((T[])objs, count);
			};
			BatchedEvents.data[BatchedEvents.data.Length - 1] = new BatchedEvents.Archetype
			{
				type = type,
				events = eventTypes,
				variant = archetypeVariant,
				archetypeIndex = BatchedEvents.data.Length - 1 + 1,
				action = ((ac1 != null) ? action3 : action4),
				sampler = CustomSampler.Create(type.Name, false)
			};
			BatchedEvents.data[BatchedEvents.data.Length - 1].Add(obj);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0005C0CC File Offset: 0x0005A2CC
		private void Process(BatchedEvents.Event eventType, Type typeFilter)
		{
			try
			{
				BatchedEvents.isIterating = true;
				for (int i = 0; i < BatchedEvents.data.Length; i++)
				{
					ref BatchedEvents.Archetype ptr = ref BatchedEvents.data[i];
					if (ptr.objectCount > 0 && (ptr.events & eventType) != BatchedEvents.Event.None && (typeFilter == null || typeFilter == ptr.type))
					{
						BatchedEvents.isIteratingOverTypeIndex = ptr.variant;
						try
						{
							ptr.action(ptr.objects, ptr.objectCount, ptr.transforms, eventType);
						}
						finally
						{
						}
					}
				}
			}
			finally
			{
				BatchedEvents.isIterating = false;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0005C178 File Offset: 0x0005A378
		public static void ProcessEvent<T>(BatchedEvents.Event eventType)
		{
			BatchedEvents batchedEvents = BatchedEvents.instance;
			if (batchedEvents == null)
			{
				return;
			}
			batchedEvents.Process(eventType, typeof(T));
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0005C194 File Offset: 0x0005A394
		private void Update()
		{
			this.Process(BatchedEvents.Event.Update, null);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0005C19E File Offset: 0x0005A39E
		private void LateUpdate()
		{
			this.Process(BatchedEvents.Event.LateUpdate, null);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
		private void FixedUpdate()
		{
			this.Process(BatchedEvents.Event.FixedUpdate, null);
		}

		// Token: 0x04000B13 RID: 2835
		private const int ArchetypeOffset = 22;

		// Token: 0x04000B14 RID: 2836
		private const int ArchetypeMask = 1069547520;

		// Token: 0x04000B15 RID: 2837
		private static BatchedEvents.Archetype[] data = new BatchedEvents.Archetype[0];

		// Token: 0x04000B16 RID: 2838
		private static BatchedEvents instance;

		// Token: 0x04000B17 RID: 2839
		private static int isIteratingOverTypeIndex = -1;

		// Token: 0x04000B18 RID: 2840
		private static bool isIterating = false;

		// Token: 0x02000271 RID: 625
		[Flags]
		public enum Event
		{
			// Token: 0x04000B1A RID: 2842
			Update = 1,
			// Token: 0x04000B1B RID: 2843
			LateUpdate = 2,
			// Token: 0x04000B1C RID: 2844
			FixedUpdate = 4,
			// Token: 0x04000B1D RID: 2845
			Custom = 8,
			// Token: 0x04000B1E RID: 2846
			None = 0
		}

		// Token: 0x02000272 RID: 626
		private struct Archetype
		{
			// Token: 0x06000EED RID: 3821 RVA: 0x0005C1CC File Offset: 0x0005A3CC
			public void Add(Component obj)
			{
				this.objectCount++;
				if (this.objects == null)
				{
					this.objects = (object[])Array.CreateInstance(this.type, math.ceilpow2(this.objectCount));
				}
				if (this.objectCount > this.objects.Length)
				{
					Array array = Array.CreateInstance(this.type, math.ceilpow2(this.objectCount));
					this.objects.CopyTo(array, 0);
					this.objects = (object[])array;
				}
				this.objects[this.objectCount - 1] = obj;
				if (!this.transforms.isCreated)
				{
					this.transforms = new TransformAccessArray(16, -1);
				}
				this.transforms.Add(obj.transform);
				((IEntityIndex)obj).EntityIndex = (this.archetypeIndex << 22) | (this.objectCount - 1);
			}

			// Token: 0x06000EEE RID: 3822 RVA: 0x0005C2AC File Offset: 0x0005A4AC
			public void Remove(int index)
			{
				this.objectCount--;
				((IEntityIndex)this.objects[this.objectCount]).EntityIndex = (this.archetypeIndex << 22) | index;
				((IEntityIndex)this.objects[index]).EntityIndex = 0;
				this.objects[index] = this.objects[this.objectCount];
				this.objects[this.objectCount] = null;
				this.transforms.RemoveAtSwapBack(index);
				if (this.objectCount == 0)
				{
					this.transforms.Dispose();
				}
			}

			// Token: 0x04000B1F RID: 2847
			public object[] objects;

			// Token: 0x04000B20 RID: 2848
			public int objectCount;

			// Token: 0x04000B21 RID: 2849
			public Type type;

			// Token: 0x04000B22 RID: 2850
			public TransformAccessArray transforms;

			// Token: 0x04000B23 RID: 2851
			public int variant;

			// Token: 0x04000B24 RID: 2852
			public int archetypeIndex;

			// Token: 0x04000B25 RID: 2853
			public BatchedEvents.Event events;

			// Token: 0x04000B26 RID: 2854
			public Action<object[], int, TransformAccessArray, BatchedEvents.Event> action;

			// Token: 0x04000B27 RID: 2855
			public CustomSampler sampler;
		}
	}
}
