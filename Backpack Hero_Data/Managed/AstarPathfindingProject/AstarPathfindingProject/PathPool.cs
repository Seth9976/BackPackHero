using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000044 RID: 68
	public static class PathPool
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00012174 File Offset: 0x00010374
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

		// Token: 0x0600033A RID: 826 RVA: 0x00012200 File Offset: 0x00010400
		public static int GetTotalCreated(Type type)
		{
			int num;
			if (PathPool.totalCreated.TryGetValue(type, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00012220 File Offset: 0x00010420
		public static int GetSize(Type type)
		{
			Stack<Path> stack;
			if (PathPool.pool.TryGetValue(type, out stack))
			{
				return stack.Count;
			}
			return 0;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00012244 File Offset: 0x00010444
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

		// Token: 0x04000209 RID: 521
		private static readonly Dictionary<Type, Stack<Path>> pool = new Dictionary<Type, Stack<Path>>();

		// Token: 0x0400020A RID: 522
		private static readonly Dictionary<Type, int> totalCreated = new Dictionary<Type, int>();
	}
}
