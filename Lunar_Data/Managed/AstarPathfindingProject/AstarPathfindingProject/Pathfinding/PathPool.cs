using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x020000B6 RID: 182
	public static class PathPool
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		public static void Pool(Path path)
		{
			Dictionary<Type, Stack<Path>> dictionary = PathPool.pool;
			lock (dictionary)
			{
				if (((IPathInternals)path).Pooled)
				{
					throw new ArgumentException("The path is already pooled.");
				}
				Stack<Path> stack;
				if (!PathPool.pool.TryGetValue(path.GetType(), out stack))
				{
					stack = new Stack<Path>();
					PathPool.pool[path.GetType()] = stack;
				}
				((IPathInternals)path).Pooled = true;
				((IPathInternals)path).OnEnterPool();
				stack.Push(path);
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001BA84 File Offset: 0x00019C84
		public static int GetTotalCreated(Type type)
		{
			int num;
			if (PathPool.totalCreated.TryGetValue(type, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		public static int GetSize(Type type)
		{
			Stack<Path> stack;
			if (PathPool.pool.TryGetValue(type, out stack))
			{
				return stack.Count;
			}
			return 0;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		public static T GetPath<T>() where T : Path, new()
		{
			Dictionary<Type, Stack<Path>> dictionary = PathPool.pool;
			T t2;
			lock (dictionary)
			{
				Stack<Path> stack;
				T t;
				if (PathPool.pool.TryGetValue(typeof(T), out stack) && stack.Count > 0)
				{
					t = stack.Pop() as T;
				}
				else
				{
					t = new T();
					if (!PathPool.totalCreated.ContainsKey(typeof(T)))
					{
						PathPool.totalCreated[typeof(T)] = 0;
					}
					Dictionary<Type, int> dictionary2 = PathPool.totalCreated;
					Type typeFromHandle = typeof(T);
					int num = dictionary2[typeFromHandle];
					dictionary2[typeFromHandle] = num + 1;
				}
				t.Pooled = false;
				t.Reset();
				t2 = t;
			}
			return t2;
		}

		// Token: 0x040003D7 RID: 983
		private static readonly Dictionary<Type, Stack<Path>> pool = new Dictionary<Type, Stack<Path>>();

		// Token: 0x040003D8 RID: 984
		private static readonly Dictionary<Type, int> totalCreated = new Dictionary<Type, int>();
	}
}
