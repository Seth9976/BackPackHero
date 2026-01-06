using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200074D RID: 1869
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x000D798A File Offset: 0x000D5B8A
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x000D7997 File Offset: 0x000D5B97
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x000D799F File Offset: 0x000D5B9F
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000D79A8 File Offset: 0x000D5BA8
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000D79C0 File Offset: 0x000D5BC0
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						this.Remove(obj2);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x04002211 RID: 8721
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04002212 RID: 8722
		private long _lastGlobalMem;

		// Token: 0x04002213 RID: 8723
		private int _lastHashCount;

		// Token: 0x0200074E RID: 1870
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x06003BF4 RID: 15348 RVA: 0x000D7B0C File Offset: 0x000D5D0C
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x06003BF5 RID: 15349 RVA: 0x000D7B70 File Offset: 0x000D5D70
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x0200074F RID: 1871
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x06003BF7 RID: 15351 RVA: 0x000D7B78 File Offset: 0x000D5D78
			internal EqualityWeakReference(object o)
				: base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x06003BF8 RID: 15352 RVA: 0x000D7B8D File Offset: 0x000D5D8D
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000D7BBC File Offset: 0x000D5DBC
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x04002214 RID: 8724
			private int _hashCode;
		}
	}
}
