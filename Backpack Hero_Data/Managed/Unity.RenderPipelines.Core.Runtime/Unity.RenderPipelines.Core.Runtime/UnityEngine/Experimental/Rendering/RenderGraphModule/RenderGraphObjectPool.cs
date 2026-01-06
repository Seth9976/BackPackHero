using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000028 RID: 40
	public sealed class RenderGraphObjectPool
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00009F9F File Offset: 0x0000819F
		internal RenderGraphObjectPool()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009FC8 File Offset: 0x000081C8
		public T[] GetTempArray<T>(int size)
		{
			Stack<object> stack;
			if (!this.m_ArrayPool.TryGetValue(new ValueTuple<Type, int>(typeof(T), size), out stack))
			{
				stack = new Stack<object>();
				this.m_ArrayPool.Add(new ValueTuple<Type, int>(typeof(T), size), stack);
			}
			T[] array = ((stack.Count > 0) ? ((T[])stack.Pop()) : new T[size]);
			this.m_AllocatedArrays.Add(new ValueTuple<object, ValueTuple<Type, int>>(array, new ValueTuple<Type, int>(typeof(T), size)));
			return array;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A058 File Offset: 0x00008258
		public MaterialPropertyBlock GetTempMaterialPropertyBlock()
		{
			MaterialPropertyBlock materialPropertyBlock = RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Get();
			materialPropertyBlock.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Add(materialPropertyBlock);
			return materialPropertyBlock;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A084 File Offset: 0x00008284
		internal void ReleaseAllTempAlloc()
		{
			foreach (ValueTuple<object, ValueTuple<Type, int>> valueTuple in this.m_AllocatedArrays)
			{
				Stack<object> stack;
				this.m_ArrayPool.TryGetValue(valueTuple.Item2, out stack);
				stack.Push(valueTuple.Item1);
			}
			this.m_AllocatedArrays.Clear();
			foreach (MaterialPropertyBlock materialPropertyBlock in this.m_AllocatedMaterialPropertyBlocks)
			{
				RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Release(materialPropertyBlock);
			}
			this.m_AllocatedMaterialPropertyBlocks.Clear();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A150 File Offset: 0x00008350
		internal T Get<T>() where T : new()
		{
			return RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Get();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A15C File Offset: 0x0000835C
		internal void Release<T>(T value) where T : new()
		{
			RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Release(value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A169 File Offset: 0x00008369
		internal void Cleanup()
		{
			this.m_AllocatedArrays.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Clear();
			this.m_ArrayPool.Clear();
			RenderGraphObjectPool.SharedObjectPoolBase.ClearAll();
		}

		// Token: 0x04000114 RID: 276
		private Dictionary<ValueTuple<Type, int>, Stack<object>> m_ArrayPool = new Dictionary<ValueTuple<Type, int>, Stack<object>>();

		// Token: 0x04000115 RID: 277
		private List<ValueTuple<object, ValueTuple<Type, int>>> m_AllocatedArrays = new List<ValueTuple<object, ValueTuple<Type, int>>>();

		// Token: 0x04000116 RID: 278
		private List<MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks = new List<MaterialPropertyBlock>();

		// Token: 0x02000132 RID: 306
		private abstract class SharedObjectPoolBase
		{
			// Token: 0x06000818 RID: 2072
			protected abstract void Clear();

			// Token: 0x06000819 RID: 2073 RVA: 0x00022D1C File Offset: 0x00020F1C
			public static void ClearAll()
			{
				foreach (RenderGraphObjectPool.SharedObjectPoolBase sharedObjectPoolBase in RenderGraphObjectPool.SharedObjectPoolBase.s_AllocatedPools)
				{
					sharedObjectPoolBase.Clear();
				}
			}

			// Token: 0x040004E8 RID: 1256
			protected static List<RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools = new List<RenderGraphObjectPool.SharedObjectPoolBase>();
		}

		// Token: 0x02000133 RID: 307
		private class SharedObjectPool<T> : RenderGraphObjectPool.SharedObjectPoolBase where T : new()
		{
			// Token: 0x0600081C RID: 2076 RVA: 0x00022D80 File Offset: 0x00020F80
			public T Get()
			{
				if (this.m_Pool.Count != 0)
				{
					return this.m_Pool.Pop();
				}
				return new T();
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00022DA0 File Offset: 0x00020FA0
			public void Release(T value)
			{
				this.m_Pool.Push(value);
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x00022DB0 File Offset: 0x00020FB0
			private static RenderGraphObjectPool.SharedObjectPool<T> AllocatePool()
			{
				RenderGraphObjectPool.SharedObjectPool<T> sharedObjectPool = new RenderGraphObjectPool.SharedObjectPool<T>();
				RenderGraphObjectPool.SharedObjectPoolBase.s_AllocatedPools.Add(sharedObjectPool);
				return sharedObjectPool;
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x00022DCF File Offset: 0x00020FCF
			protected override void Clear()
			{
				this.m_Pool.Clear();
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000820 RID: 2080 RVA: 0x00022DDC File Offset: 0x00020FDC
			public static RenderGraphObjectPool.SharedObjectPool<T> sharedPool
			{
				get
				{
					return RenderGraphObjectPool.SharedObjectPool<T>.s_Instance.Value;
				}
			}

			// Token: 0x040004E9 RID: 1257
			private Stack<T> m_Pool = new Stack<T>();

			// Token: 0x040004EA RID: 1258
			private static readonly Lazy<RenderGraphObjectPool.SharedObjectPool<T>> s_Instance = new Lazy<RenderGraphObjectPool.SharedObjectPool<T>>(new Func<RenderGraphObjectPool.SharedObjectPool<T>>(RenderGraphObjectPool.SharedObjectPool<T>.AllocatePool));
		}
	}
}
