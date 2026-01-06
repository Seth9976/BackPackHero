using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012D RID: 301
	internal struct InlinedArray<TValue> : IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004EF7D File Offset: 0x0004D17D
		public int Capacity
		{
			get
			{
				TValue[] array = this.additionalValues;
				if (array == null)
				{
					return 1;
				}
				return array.Length + 1;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0004EF8F File Offset: 0x0004D18F
		public InlinedArray(TValue value)
		{
			this.length = 1;
			this.firstValue = value;
			this.additionalValues = null;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004EFA6 File Offset: 0x0004D1A6
		public InlinedArray(TValue firstValue, params TValue[] additionalValues)
		{
			this.length = 1 + additionalValues.Length;
			this.firstValue = firstValue;
			this.additionalValues = additionalValues;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
		public InlinedArray(IEnumerable<TValue> values)
		{
			this = default(InlinedArray<TValue>);
			this.length = values.Count<TValue>();
			if (this.length > 1)
			{
				this.additionalValues = new TValue[this.length - 1];
			}
			else
			{
				this.additionalValues = null;
			}
			int num = 0;
			foreach (TValue tvalue in values)
			{
				if (num == 0)
				{
					this.firstValue = tvalue;
				}
				else
				{
					this.additionalValues[num - 1] = tvalue;
				}
				num++;
			}
		}

		// Token: 0x170004B3 RID: 1203
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					return this.firstValue;
				}
				return this.additionalValues[index - 1];
			}
			set
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index == 0)
				{
					this.firstValue = value;
					return;
				}
				this.additionalValues[index - 1] = value;
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004F0C6 File Offset: 0x0004D2C6
		public void Clear()
		{
			this.length = 0;
			this.firstValue = default(TValue);
			this.additionalValues = null;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0004F0E4 File Offset: 0x0004D2E4
		public void ClearWithCapacity()
		{
			this.firstValue = default(TValue);
			for (int i = 0; i < this.length - 1; i++)
			{
				this.additionalValues[i] = default(TValue);
			}
			this.length = 0;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004F12C File Offset: 0x0004D32C
		public InlinedArray<TValue> Clone()
		{
			return new InlinedArray<TValue>
			{
				length = this.length,
				firstValue = this.firstValue,
				additionalValues = ((this.additionalValues != null) ? ArrayHelpers.Copy<TValue>(this.additionalValues) : null)
			};
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0004F17C File Offset: 0x0004D37C
		public void SetLength(int size)
		{
			if (size < this.length)
			{
				for (int i = size; i < this.length; i++)
				{
					this[i] = default(TValue);
				}
			}
			this.length = size;
			if (size > 1 && (this.additionalValues == null || this.additionalValues.Length < size - 1))
			{
				Array.Resize<TValue>(ref this.additionalValues, size - 1);
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0004F1E1 File Offset: 0x0004D3E1
		public TValue[] ToArray()
		{
			return ArrayHelpers.Join<TValue>(this.firstValue, this.additionalValues);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
		public TOther[] ToArray<TOther>(Func<TValue, TOther> mapFunction)
		{
			if (this.length == 0)
			{
				return null;
			}
			TOther[] array = new TOther[this.length];
			for (int i = 0; i < this.length; i++)
			{
				array[i] = mapFunction(this[i]);
			}
			return array;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0004F240 File Offset: 0x0004D440
		public int IndexOf(TValue value)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			if (this.length > 0)
			{
				if (@default.Equals(this.firstValue, value))
				{
					return 0;
				}
				if (this.additionalValues != null)
				{
					for (int i = 0; i < this.length - 1; i++)
					{
						if (@default.Equals(this.additionalValues[i], value))
						{
							return i + 1;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
		public int Append(TValue value)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else if (this.additionalValues == null)
			{
				this.additionalValues = new TValue[1];
				this.additionalValues[0] = value;
			}
			else
			{
				Array.Resize<TValue>(ref this.additionalValues, this.length);
				this.additionalValues[this.length - 1] = value;
			}
			int num = this.length;
			this.length++;
			return num;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004F320 File Offset: 0x0004D520
		public int AppendWithCapacity(TValue value, int capacityIncrement = 10)
		{
			if (this.length == 0)
			{
				this.firstValue = value;
			}
			else
			{
				int num = this.length - 1;
				ArrayHelpers.AppendWithCapacity<TValue>(ref this.additionalValues, ref num, value, capacityIncrement);
			}
			int num2 = this.length;
			this.length++;
			return num2;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004F36C File Offset: 0x0004D56C
		public void AssignWithCapacity(InlinedArray<TValue> values)
		{
			if (this.Capacity < values.length && values.length > 1)
			{
				this.additionalValues = new TValue[values.length - 1];
			}
			this.length = values.length;
			if (this.length > 0)
			{
				this.firstValue = values.firstValue;
			}
			if (this.length > 1)
			{
				Array.Copy(values.additionalValues, this.additionalValues, this.length - 1);
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004F3E8 File Offset: 0x0004D5E8
		public void Append(IEnumerable<TValue> values)
		{
			foreach (TValue tvalue in values)
			{
				this.Append(tvalue);
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004F434 File Offset: 0x0004D634
		public void Remove(TValue value)
		{
			if (this.length < 1)
			{
				return;
			}
			if (EqualityComparer<TValue>.Default.Equals(this.firstValue, value))
			{
				this.RemoveAt(0);
				return;
			}
			if (this.additionalValues != null)
			{
				for (int i = 0; i < this.length - 1; i++)
				{
					if (EqualityComparer<TValue>.Default.Equals(this.additionalValues[i], value))
					{
						this.RemoveAt(i + 1);
						return;
					}
				}
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0004F4A4 File Offset: 0x0004D6A4
		public void RemoveAtWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.length == 1)
				{
					this.firstValue = default(TValue);
				}
				else if (this.length == 2)
				{
					this.firstValue = this.additionalValues[0];
					this.additionalValues[0] = default(TValue);
				}
				else
				{
					this.firstValue = this.additionalValues[0];
					int num = this.length - 1;
					this.additionalValues.EraseAtWithCapacity(ref num, 0);
				}
			}
			else
			{
				int num2 = this.length - 1;
				this.additionalValues.EraseAtWithCapacity(ref num2, index - 1);
			}
			this.length--;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0004F568 File Offset: 0x0004D768
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				if (this.additionalValues != null)
				{
					this.firstValue = this.additionalValues[0];
					if (this.additionalValues.Length == 1)
					{
						this.additionalValues = null;
					}
					else
					{
						Array.Copy(this.additionalValues, 1, this.additionalValues, 0, this.additionalValues.Length - 1);
						Array.Resize<TValue>(ref this.additionalValues, this.additionalValues.Length - 1);
					}
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				int num = this.length - 1;
				if (num == 1)
				{
					this.additionalValues = null;
				}
				else if (index == this.length - 1)
				{
					Array.Resize<TValue>(ref this.additionalValues, num - 1);
				}
				else
				{
					TValue[] array = new TValue[num - 1];
					if (index >= 2)
					{
						Array.Copy(this.additionalValues, 0, array, 0, index - 1);
					}
					Array.Copy(this.additionalValues, index + 1 - 1, array, index - 1, this.length - index - 1);
					this.additionalValues = array;
				}
			}
			this.length--;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004F688 File Offset: 0x0004D888
		public void RemoveAtByMovingTailWithCapacity(int index)
		{
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = this.length - 1;
			if (index == 0)
			{
				if (this.length > 1)
				{
					this.firstValue = this.additionalValues[num - 1];
					this.additionalValues[num - 1] = default(TValue);
				}
				else
				{
					this.firstValue = default(TValue);
				}
			}
			else
			{
				ArrayHelpers.EraseAtByMovingTail<TValue>(this.additionalValues, ref num, index - 1);
			}
			this.length--;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0004F71C File Offset: 0x0004D91C
		public bool RemoveByMovingTailWithCapacity(TValue value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return false;
			}
			this.RemoveAtByMovingTailWithCapacity(num);
			return true;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0004F740 File Offset: 0x0004D940
		public bool Contains(TValue value, IEqualityComparer<TValue> comparer)
		{
			for (int i = 0; i < this.length; i++)
			{
				if (comparer.Equals(this[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004F774 File Offset: 0x0004D974
		public void Merge(InlinedArray<TValue> other)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < other.length; i++)
			{
				TValue tvalue = other[i];
				if (!this.Contains(tvalue, @default))
				{
					this.Append(tvalue);
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004F7B4 File Offset: 0x0004D9B4
		public IEnumerator<TValue> GetEnumerator()
		{
			return new InlinedArray<TValue>.Enumerator
			{
				array = this,
				index = -1
			};
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004F7E4 File Offset: 0x0004D9E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040006B8 RID: 1720
		public int length;

		// Token: 0x040006B9 RID: 1721
		public TValue firstValue;

		// Token: 0x040006BA RID: 1722
		public TValue[] additionalValues;

		// Token: 0x02000238 RID: 568
		private struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06001591 RID: 5521 RVA: 0x00062D42 File Offset: 0x00060F42
			public bool MoveNext()
			{
				if (this.index >= this.array.length)
				{
					return false;
				}
				this.index++;
				return this.index < this.array.length;
			}

			// Token: 0x06001592 RID: 5522 RVA: 0x00062D7A File Offset: 0x00060F7A
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x00062D83 File Offset: 0x00060F83
			public TValue Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001594 RID: 5524 RVA: 0x00062D96 File Offset: 0x00060F96
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00062DA3 File Offset: 0x00060FA3
			public void Dispose()
			{
			}

			// Token: 0x04000BF6 RID: 3062
			public InlinedArray<TValue> array;

			// Token: 0x04000BF7 RID: 3063
			public int index;
		}
	}
}
