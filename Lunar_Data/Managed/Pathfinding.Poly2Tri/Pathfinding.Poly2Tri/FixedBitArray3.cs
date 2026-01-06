using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.Poly2Tri
{
	// Token: 0x0200001F RID: 31
	public struct FixedBitArray3 : IEnumerable, IEnumerable<bool>
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00005A64 File Offset: 0x00003C64
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000029 RID: 41
		public bool this[int index]
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

		// Token: 0x060000ED RID: 237 RVA: 0x00005AFC File Offset: 0x00003CFC
		public bool Contains(bool value)
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

		// Token: 0x060000EE RID: 238 RVA: 0x00005B2C File Offset: 0x00003D2C
		public int IndexOf(bool value)
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

		// Token: 0x060000EF RID: 239 RVA: 0x00005B5C File Offset: 0x00003D5C
		public void Clear()
		{
			this._0 = (this._1 = (this._2 = false));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005B84 File Offset: 0x00003D84
		public void Clear(bool value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					this[i] = false;
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private IEnumerable<bool> Enumerate()
		{
			for (int i = 0; i < 3; i++)
			{
				yield return this[i];
			}
			yield break;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public IEnumerator<bool> GetEnumerator()
		{
			return this.Enumerate().GetEnumerator();
		}

		// Token: 0x04000050 RID: 80
		public bool _0;

		// Token: 0x04000051 RID: 81
		public bool _1;

		// Token: 0x04000052 RID: 82
		public bool _2;
	}
}
