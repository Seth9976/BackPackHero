using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001E RID: 30
	public struct FixedArray3<T> : IEnumerable, IEnumerable<T> where T : class
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000058AC File Offset: 0x00003AAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000028 RID: 40
		public T this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._0;
				case 1:
					return this._1;
				case 2:
					return this._2;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this._0 = value;
					break;
				case 1:
					this._1 = value;
					break;
				case 2:
					this._2 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005944 File Offset: 0x00003B44
		public bool Contains(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005980 File Offset: 0x00003B80
		public int IndexOf(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000059BC File Offset: 0x00003BBC
		public void Clear()
		{
			this._0 = (this._1 = (this._2 = (T)((object)null)));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000059E8 File Offset: 0x00003BE8
		public void Clear(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					this[i] = (T)((object)null);
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005A2C File Offset: 0x00003C2C
		private IEnumerable<T> Enumerate()
		{
			for (int i = 0; i < 3; i++)
			{
				yield return this[i];
			}
			yield break;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005A54 File Offset: 0x00003C54
		public IEnumerator<T> GetEnumerator()
		{
			return this.Enumerate().GetEnumerator();
		}

		// Token: 0x0400004D RID: 77
		public T _0;

		// Token: 0x0400004E RID: 78
		public T _1;

		// Token: 0x0400004F RID: 79
		public T _2;
	}
}
