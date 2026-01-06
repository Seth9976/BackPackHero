using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200015D RID: 349
	public class Recursion<T> : IPoolable, IDisposable
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x000281E4 File Offset: 0x000263E4
		protected Recursion()
		{
			this.traversedOrder = new Stack<T>();
			this.traversedCount = new Dictionary<T, int>();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00028202 File Offset: 0x00026402
		public void Enter(T o)
		{
			if (!this.TryEnter(o))
			{
				throw new StackOverflowException(string.Format("Max recursion depth of {0} has been exceeded. Consider increasing '{1}.{2}'.", this.maxDepth, "Recursion", "defaultMaxDepth"));
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00028234 File Offset: 0x00026434
		public bool TryEnter(T o)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			int num;
			if (!this.traversedCount.TryGetValue(o, out num))
			{
				this.traversedOrder.Push(o);
				this.traversedCount.Add(o, 1);
				return true;
			}
			if (num < this.maxDepth)
			{
				this.traversedOrder.Push(o);
				Dictionary<T, int> dictionary = this.traversedCount;
				int num2 = dictionary[o];
				dictionary[o] = num2 + 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000282B4 File Offset: 0x000264B4
		public void Exit(T o)
		{
			if (this.traversedOrder.Count == 0)
			{
				throw new InvalidOperationException("Trying to exit an empty recursion stack.");
			}
			T t = this.traversedOrder.Peek();
			if (!EqualityComparer<T>.Default.Equals(o, t))
			{
				throw new InvalidOperationException(string.Format("Exiting recursion stack in a non-consecutive order:\nProvided: {0} / Expected: {1}", o, t));
			}
			this.traversedOrder.Pop();
			Dictionary<T, int> dictionary = this.traversedCount;
			T t2 = t;
			int num = dictionary[t2];
			dictionary[t2] = num - 1;
			if (num == 0)
			{
				this.traversedCount.Remove(t);
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00028344 File Offset: 0x00026544
		public void Dispose()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			this.Free();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00028360 File Offset: 0x00026560
		protected virtual void Free()
		{
			GenericPool<Recursion<T>>.Free(this);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00028368 File Offset: 0x00026568
		void IPoolable.New()
		{
			this.disposed = false;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00028371 File Offset: 0x00026571
		void IPoolable.Free()
		{
			this.disposed = true;
			this.traversedCount.Clear();
			this.traversedOrder.Clear();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00028390 File Offset: 0x00026590
		public static Recursion<T> New()
		{
			return Recursion<T>.New(Recursion.defaultMaxDepth);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002839C File Offset: 0x0002659C
		public static Recursion<T> New(int maxDepth)
		{
			if (!Recursion.safeMode)
			{
				return null;
			}
			if (maxDepth < 1)
			{
				throw new ArgumentException("Max recursion depth must be at least one.", "maxDepth");
			}
			Recursion<T> recursion = GenericPool<Recursion<T>>.New(() => new Recursion<T>());
			recursion.maxDepth = maxDepth;
			return recursion;
		}

		// Token: 0x04000235 RID: 565
		private readonly Stack<T> traversedOrder;

		// Token: 0x04000236 RID: 566
		private readonly Dictionary<T, int> traversedCount;

		// Token: 0x04000237 RID: 567
		private bool disposed;

		// Token: 0x04000238 RID: 568
		protected int maxDepth;
	}
}
