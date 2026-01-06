using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027D RID: 637
	internal struct StyleDataRef<T> : IEquatable<StyleDataRef<T>> where T : struct, IEquatable<T>, IStyleDataGroup<T>
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00057BBA File Offset: 0x00055DBA
		public int refCount
		{
			get
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				return (@ref != null) ? @ref.refCount : 0;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00057BCE File Offset: 0x00055DCE
		public uint id
		{
			get
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				return (@ref != null) ? @ref.id : 0U;
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00057BE4 File Offset: 0x00055DE4
		public StyleDataRef<T> Acquire()
		{
			this.m_Ref.Acquire();
			return this;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00057C08 File Offset: 0x00055E08
		public void Release()
		{
			this.m_Ref.Release();
			this.m_Ref = null;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00057C20 File Offset: 0x00055E20
		public void CopyFrom(StyleDataRef<T> other)
		{
			bool flag = this.m_Ref.refCount == 1;
			if (flag)
			{
				this.m_Ref.value.CopyFrom(ref other.m_Ref.value);
			}
			else
			{
				this.m_Ref.Release();
				this.m_Ref = other.m_Ref;
				this.m_Ref.Acquire();
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00057C8C File Offset: 0x00055E8C
		public readonly ref T Read()
		{
			return ref this.m_Ref.value;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00057C9C File Offset: 0x00055E9C
		public ref T Write()
		{
			bool flag = this.m_Ref.refCount == 1;
			ref T ptr;
			if (flag)
			{
				ptr = ref this.m_Ref.value;
			}
			else
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				this.m_Ref = this.m_Ref.Copy();
				@ref.Release();
				ptr = ref this.m_Ref.value;
			}
			return ref ptr;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00057CF8 File Offset: 0x00055EF8
		public static StyleDataRef<T> Create()
		{
			return new StyleDataRef<T>
			{
				m_Ref = new StyleDataRef<T>.RefCounted()
			};
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00057D20 File Offset: 0x00055F20
		public override int GetHashCode()
		{
			return (this.m_Ref != null) ? this.m_Ref.value.GetHashCode() : 0;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00057D54 File Offset: 0x00055F54
		public static bool operator ==(StyleDataRef<T> lhs, StyleDataRef<T> rhs)
		{
			return lhs.m_Ref == rhs.m_Ref || lhs.m_Ref.value.Equals(rhs.m_Ref.value);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00057D98 File Offset: 0x00055F98
		public static bool operator !=(StyleDataRef<T> lhs, StyleDataRef<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00057DB4 File Offset: 0x00055FB4
		public bool Equals(StyleDataRef<T> other)
		{
			return other == this;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00057DD4 File Offset: 0x00055FD4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleDataRef<T>)
			{
				StyleDataRef<T> styleDataRef = (StyleDataRef<T>)obj;
				flag = this.Equals(styleDataRef);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040008FD RID: 2301
		private StyleDataRef<T>.RefCounted m_Ref;

		// Token: 0x0200027E RID: 638
		private class RefCounted
		{
			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x0600149D RID: 5277 RVA: 0x00057DFF File Offset: 0x00055FFF
			public int refCount
			{
				get
				{
					return this.m_RefCount;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x0600149E RID: 5278 RVA: 0x00057E07 File Offset: 0x00056007
			public uint id
			{
				get
				{
					return this.m_Id;
				}
			}

			// Token: 0x0600149F RID: 5279 RVA: 0x00057E0F File Offset: 0x0005600F
			public RefCounted()
			{
				this.m_RefCount = 1;
				this.m_Id = (StyleDataRef<T>.RefCounted.m_NextId += 1U);
			}

			// Token: 0x060014A0 RID: 5280 RVA: 0x00057E33 File Offset: 0x00056033
			public void Acquire()
			{
				this.m_RefCount++;
			}

			// Token: 0x060014A1 RID: 5281 RVA: 0x00057E43 File Offset: 0x00056043
			public void Release()
			{
				this.m_RefCount--;
			}

			// Token: 0x060014A2 RID: 5282 RVA: 0x00057E54 File Offset: 0x00056054
			public StyleDataRef<T>.RefCounted Copy()
			{
				return new StyleDataRef<T>.RefCounted
				{
					value = this.value.Copy()
				};
			}

			// Token: 0x040008FE RID: 2302
			private static uint m_NextId = 1U;

			// Token: 0x040008FF RID: 2303
			private int m_RefCount;

			// Token: 0x04000900 RID: 2304
			private readonly uint m_Id;

			// Token: 0x04000901 RID: 2305
			public T value;
		}
	}
}
