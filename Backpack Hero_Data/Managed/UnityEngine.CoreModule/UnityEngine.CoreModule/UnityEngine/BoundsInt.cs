using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000110 RID: 272
	[UsedByNativeCode]
	public struct BoundsInt : IEquatable<BoundsInt>, IFormattable
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000091CC File Offset: 0x000073CC
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x000091E9 File Offset: 0x000073E9
		public int x
		{
			get
			{
				return this.m_Position.x;
			}
			set
			{
				this.m_Position.x = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000091FC File Offset: 0x000073FC
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00009219 File Offset: 0x00007419
		public int y
		{
			get
			{
				return this.m_Position.y;
			}
			set
			{
				this.m_Position.y = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000922C File Offset: 0x0000742C
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00009249 File Offset: 0x00007449
		public int z
		{
			get
			{
				return this.m_Position.z;
			}
			set
			{
				this.m_Position.z = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000925C File Offset: 0x0000745C
		public Vector3 center
		{
			get
			{
				return new Vector3((float)this.x + (float)this.m_Size.x / 2f, (float)this.y + (float)this.m_Size.y / 2f, (float)this.z + (float)this.m_Size.z / 2f);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000092C4 File Offset: 0x000074C4
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x000092ED File Offset: 0x000074ED
		public Vector3Int min
		{
			get
			{
				return new Vector3Int(this.xMin, this.yMin, this.zMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
				this.zMin = value.z;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000931C File Offset: 0x0000751C
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00009345 File Offset: 0x00007545
		public Vector3Int max
		{
			get
			{
				return new Vector3Int(this.xMax, this.yMax, this.zMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
				this.zMax = value.z;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00009374 File Offset: 0x00007574
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x000093B0 File Offset: 0x000075B0
		public int xMin
		{
			get
			{
				return Math.Min(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			set
			{
				int xMax = this.xMax;
				this.m_Position.x = value;
				this.m_Size.x = xMax - this.m_Position.x;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000093EC File Offset: 0x000075EC
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00009428 File Offset: 0x00007628
		public int yMin
		{
			get
			{
				return Math.Min(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			set
			{
				int yMax = this.yMax;
				this.m_Position.y = value;
				this.m_Size.y = yMax - this.m_Position.y;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00009464 File Offset: 0x00007664
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x000094A0 File Offset: 0x000076A0
		public int zMin
		{
			get
			{
				return Math.Min(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			set
			{
				int zMax = this.zMax;
				this.m_Position.z = value;
				this.m_Size.z = zMax - this.m_Position.z;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000094DC File Offset: 0x000076DC
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00009515 File Offset: 0x00007715
		public int xMax
		{
			get
			{
				return Math.Max(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			set
			{
				this.m_Size.x = value - this.m_Position.x;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00009534 File Offset: 0x00007734
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000956D File Offset: 0x0000776D
		public int yMax
		{
			get
			{
				return Math.Max(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			set
			{
				this.m_Size.y = value - this.m_Position.y;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000958C File Offset: 0x0000778C
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x000095C5 File Offset: 0x000077C5
		public int zMax
		{
			get
			{
				return Math.Max(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			set
			{
				this.m_Size.z = value - this.m_Position.z;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000095E4 File Offset: 0x000077E4
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x000095FC File Offset: 0x000077FC
		public Vector3Int position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00009608 File Offset: 0x00007808
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x00009620 File Offset: 0x00007820
		public Vector3Int size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000962A File Offset: 0x0000782A
		public BoundsInt(int xMin, int yMin, int zMin, int sizeX, int sizeY, int sizeZ)
		{
			this.m_Position = new Vector3Int(xMin, yMin, zMin);
			this.m_Size = new Vector3Int(sizeX, sizeY, sizeZ);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000964C File Offset: 0x0000784C
		public BoundsInt(Vector3Int position, Vector3Int size)
		{
			this.m_Position = position;
			this.m_Size = size;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000965D File Offset: 0x0000785D
		public void SetMinMax(Vector3Int minPosition, Vector3Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00009670 File Offset: 0x00007870
		public void ClampToBounds(BoundsInt bounds)
		{
			this.position = new Vector3Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin), Math.Max(Math.Min(bounds.zMax, this.position.z), bounds.zMin));
			this.size = new Vector3Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y), Math.Min(bounds.zMax - this.position.z, this.size.z));
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00009784 File Offset: 0x00007984
		public bool Contains(Vector3Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.z >= this.zMin && position.x < this.xMax && position.y < this.yMax && position.z < this.zMax;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000097F4 File Offset: 0x000079F4
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00009810 File Offset: 0x00007A10
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000982C File Offset: 0x00007A2C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("Position: {0}, Size: {1}", new object[]
			{
				this.m_Position.ToString(format, formatProvider),
				this.m_Size.ToString(format, formatProvider)
			});
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00009884 File Offset: 0x00007A84
		public static bool operator ==(BoundsInt lhs, BoundsInt rhs)
		{
			return lhs.m_Position == rhs.m_Position && lhs.m_Size == rhs.m_Size;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static bool operator !=(BoundsInt lhs, BoundsInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000098DC File Offset: 0x00007ADC
		public override bool Equals(object other)
		{
			bool flag = !(other is BoundsInt);
			return !flag && this.Equals((BoundsInt)other);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00009910 File Offset: 0x00007B10
		public bool Equals(BoundsInt other)
		{
			return this.m_Position.Equals(other.m_Position) && this.m_Size.Equals(other.m_Size);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000994C File Offset: 0x00007B4C
		public override int GetHashCode()
		{
			return this.m_Position.GetHashCode() ^ (this.m_Size.GetHashCode() << 2);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00009984 File Offset: 0x00007B84
		public BoundsInt.PositionEnumerator allPositionsWithin
		{
			get
			{
				return new BoundsInt.PositionEnumerator(this.min, this.max);
			}
		}

		// Token: 0x04000386 RID: 902
		private Vector3Int m_Position;

		// Token: 0x04000387 RID: 903
		private Vector3Int m_Size;

		// Token: 0x02000111 RID: 273
		public struct PositionEnumerator : IEnumerator<Vector3Int>, IEnumerator, IDisposable
		{
			// Token: 0x060006CB RID: 1739 RVA: 0x000099A8 File Offset: 0x00007BA8
			public PositionEnumerator(Vector3Int min, Vector3Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x000099D4 File Offset: 0x00007BD4
			public BoundsInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x000099EC File Offset: 0x00007BEC
			public bool MoveNext()
			{
				bool flag = this._current.z >= this._max.z || this._current.y >= this._max.y;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int num = this._current.x;
					this._current.x = num + 1;
					bool flag3 = this._current.x >= this._max.x;
					if (flag3)
					{
						this._current.x = this._min.x;
						bool flag4 = this._current.x >= this._max.x;
						if (flag4)
						{
							return false;
						}
						num = this._current.y;
						this._current.y = num + 1;
						bool flag5 = this._current.y >= this._max.y;
						if (flag5)
						{
							this._current.y = this._min.y;
							num = this._current.z;
							this._current.z = num + 1;
							bool flag6 = this._current.z >= this._max.z;
							if (flag6)
							{
								return false;
							}
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x00009B60 File Offset: 0x00007D60
			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060006CF RID: 1743 RVA: 0x00009B90 File Offset: 0x00007D90
			public Vector3Int Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00009BA8 File Offset: 0x00007DA8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00004557 File Offset: 0x00002757
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000388 RID: 904
			private readonly Vector3Int _min;

			// Token: 0x04000389 RID: 905
			private readonly Vector3Int _max;

			// Token: 0x0400038A RID: 906
			private Vector3Int _current;
		}
	}
}
