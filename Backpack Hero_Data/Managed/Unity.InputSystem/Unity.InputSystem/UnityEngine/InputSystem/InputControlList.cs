using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000032 RID: 50
	[DebuggerDisplay("Count = {Count}")]
	public struct InputControlList<TControl> : IList<TControl>, ICollection<TControl>, IEnumerable<TControl>, IEnumerable, IReadOnlyList<TControl>, IReadOnlyCollection<TControl>, IDisposable where TControl : InputControl
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00011495 File Offset: 0x0000F695
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001149D File Offset: 0x0000F69D
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000114BC File Offset: 0x0000F6BC
		public int Capacity
		{
			get
			{
				if (!this.m_Indices.IsCreated)
				{
					return 0;
				}
				return this.m_Indices.Length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Capacity cannot be negative", "value");
				}
				if (value == 0)
				{
					if (this.m_Count != 0)
					{
						this.m_Indices.Dispose();
					}
					this.m_Count = 0;
					return;
				}
				Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
				ArrayHelpers.Resize<ulong>(ref this.m_Indices, value, allocator);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001151C File Offset: 0x0000F71C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		public TControl this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				return InputControlList<TControl>.FromIndex(this.m_Indices[index]);
			}
			set
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				this.m_Indices[index] = InputControlList<TControl>.ToIndex(value);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000115C6 File Offset: 0x0000F7C6
		public InputControlList(Allocator allocator, int initialCapacity = 0)
		{
			this.m_Allocator = allocator;
			this.m_Indices = default(NativeArray<ulong>);
			this.m_Count = 0;
			if (initialCapacity != 0)
			{
				this.Capacity = initialCapacity;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000115EC File Offset: 0x0000F7EC
		public InputControlList(IEnumerable<TControl> values, Allocator allocator = Allocator.Persistent)
		{
			this = new InputControlList<TControl>(allocator, 0);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (TControl tcontrol in values)
			{
				this.Add(tcontrol);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001164C File Offset: 0x0000F84C
		public InputControlList(params TControl[] values)
		{
			this = default(InputControlList<TControl>);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = values.Length;
			this.Capacity = Mathf.Max(num, 10);
			for (int i = 0; i < num; i++)
			{
				this.Add(values[i]);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001169C File Offset: 0x0000F89C
		public unsafe void Resize(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "Size cannot be negative");
			}
			if (this.Capacity < size)
			{
				this.Capacity = size;
			}
			if (size > this.Count)
			{
				UnsafeUtility.MemSet((void*)((byte*)this.m_Indices.GetUnsafePtr<ulong>() + this.Count * 8), byte.MaxValue, (long)(size - this.Count));
			}
			this.m_Count = size;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00011704 File Offset: 0x0000F904
		public void Add(TControl item)
		{
			ulong num = InputControlList<TControl>.ToIndex(item);
			Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
			ArrayHelpers.AppendWithCapacity<ulong>(ref this.m_Indices, ref this.m_Count, num, 10, allocator);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011740 File Offset: 0x0000F940
		public void AddSlice<TList>(TList list, int count = -1, int destinationIndex = -1, int sourceIndex = 0) where TList : IReadOnlyList<TControl>
		{
			if (count < 0)
			{
				count = list.Count;
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (sourceIndex + count > list.Count)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count of {0} elements starting at index {1} exceeds length of list of {2}", count, sourceIndex, list.Count));
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			for (int i = 0; i < count; i++)
			{
				this.m_Indices[destinationIndex + i] = InputControlList<TControl>.ToIndex(list[sourceIndex + i]);
			}
			this.m_Count += count;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011840 File Offset: 0x0000FA40
		public void AddRange(IEnumerable<TControl> list, int count = -1, int destinationIndex = -1)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (count < 0)
			{
				count = list.Count<TControl>();
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			foreach (TControl tcontrol in list)
			{
				this.m_Indices[destinationIndex++] = InputControlList<TControl>.ToIndex(tcontrol);
				this.m_Count++;
				count--;
				if (count == 0)
				{
					break;
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011928 File Offset: 0x0000FB28
		public bool Remove(TControl item)
		{
			if (this.m_Count == 0)
			{
				return false;
			}
			ulong num = InputControlList<TControl>.ToIndex(item);
			for (int i = 0; i < this.m_Count; i++)
			{
				if (this.m_Indices[i] == num)
				{
					ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001197C File Offset: 0x0000FB7C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} elements", index, this.m_Count));
			}
			ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, index);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000119CE File Offset: 0x0000FBCE
		public void CopyTo(TControl[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000119D5 File Offset: 0x0000FBD5
		public int IndexOf(TControl item)
		{
			return this.IndexOf(item, 0, -1);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public unsafe int IndexOf(TControl item, int startIndex, int count = -1)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be negative");
			}
			if (this.m_Count == 0)
			{
				return -1;
			}
			if (count < 0)
			{
				count = Mathf.Max(this.m_Count - startIndex, 0);
			}
			if (startIndex + count > this.m_Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			ulong num = InputControlList<TControl>.ToIndex(item);
			ulong* unsafeReadOnlyPtr = (ulong*)this.m_Indices.GetUnsafeReadOnlyPtr<ulong>();
			for (int i = 0; i < count; i++)
			{
				if (unsafeReadOnlyPtr[startIndex + i] == num)
				{
					return startIndex + i;
				}
			}
			return -1;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011A65 File Offset: 0x0000FC65
		public void Insert(int index, TControl item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public void Clear()
		{
			this.m_Count = 0;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011A75 File Offset: 0x0000FC75
		public bool Contains(TControl item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011A84 File Offset: 0x0000FC84
		public bool Contains(TControl item, int startIndex, int count = -1)
		{
			return this.IndexOf(item, startIndex, count) != -1;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011A98 File Offset: 0x0000FC98
		public void SwapElements(int index1, int index2)
		{
			if (index1 < 0 || index1 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index1");
			}
			if (index2 < 0 || index2 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index2");
			}
			if (index1 != index2)
			{
				this.m_Indices.SwapElements(index1, index2);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		public void Sort<TCompare>(int startIndex, int count, TCompare comparer) where TCompare : IComparer<TControl>
		{
			if (startIndex < 0 || startIndex >= this.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (startIndex + count >= this.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (int i = 1; i < count; i++)
			{
				int num = i;
				while (num > 0 && comparer.Compare(this[num - 1], this[num]) < 0)
				{
					this.SwapElements(num, num - 1);
					num--;
				}
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00011B68 File Offset: 0x0000FD68
		public TControl[] ToArray(bool dispose = false)
		{
			TControl[] array = new TControl[this.m_Count];
			for (int i = 0; i < this.m_Count; i++)
			{
				array[i] = this[i];
			}
			if (dispose)
			{
				this.Dispose();
			}
			return array;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011BAC File Offset: 0x0000FDAC
		internal void AppendTo(ref TControl[] array, ref int count)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				ArrayHelpers.AppendWithCapacity<TControl>(ref array, ref count, this[i], 10);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011BDB File Offset: 0x0000FDDB
		public void Dispose()
		{
			if (this.m_Indices.IsCreated)
			{
				this.m_Indices.Dispose();
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00011BF5 File Offset: 0x0000FDF5
		public IEnumerator<TControl> GetEnumerator()
		{
			return new InputControlList<TControl>.Enumerator(this);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00011C07 File Offset: 0x0000FE07
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00011C10 File Offset: 0x0000FE10
		public override string ToString()
		{
			if (this.Count == 0)
			{
				return "()";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this[i]);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00011C7C File Offset: 0x0000FE7C
		private static ulong ToIndex(TControl control)
		{
			if (control == null)
			{
				return ulong.MaxValue;
			}
			InputDevice device = control.device;
			int deviceId = device.m_DeviceId;
			int num = ((device != control) ? (device.m_ChildrenForEachControl.IndexOfReference(control, -1) + 1) : 0);
			ulong num2 = (ulong)((ulong)((long)deviceId) << 32);
			ulong num3 = (ulong)((long)num);
			return num2 | num3;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00011CD4 File Offset: 0x0000FED4
		private static TControl FromIndex(ulong index)
		{
			if (index == 18446744073709551615UL)
			{
				return default(TControl);
			}
			int num = (int)(index >> 32);
			int num2 = (int)(index & (ulong)(-1));
			InputDevice deviceById = InputSystem.GetDeviceById(num);
			if (deviceById == null)
			{
				return default(TControl);
			}
			if (num2 == 0)
			{
				return (TControl)((object)deviceById);
			}
			return (TControl)((object)deviceById.m_ChildrenForEachControl[num2 - 1]);
		}

		// Token: 0x04000138 RID: 312
		private int m_Count;

		// Token: 0x04000139 RID: 313
		private NativeArray<ulong> m_Indices;

		// Token: 0x0400013A RID: 314
		private readonly Allocator m_Allocator;

		// Token: 0x0400013B RID: 315
		private const ulong kInvalidIndex = 18446744073709551615UL;

		// Token: 0x02000189 RID: 393
		private struct Enumerator : IEnumerator<TControl>, IEnumerator, IDisposable
		{
			// Token: 0x06001381 RID: 4993 RVA: 0x0005A0DF File Offset: 0x000582DF
			public unsafe Enumerator(InputControlList<TControl> list)
			{
				this.m_Count = list.m_Count;
				this.m_Current = -1;
				this.m_Indices = (ulong*)((this.m_Count > 0) ? list.m_Indices.GetUnsafeReadOnlyPtr<ulong>() : null);
			}

			// Token: 0x06001382 RID: 4994 RVA: 0x0005A112 File Offset: 0x00058312
			public bool MoveNext()
			{
				if (this.m_Current >= this.m_Count)
				{
					return false;
				}
				this.m_Current++;
				return this.m_Current != this.m_Count;
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x0005A143 File Offset: 0x00058343
			public void Reset()
			{
				this.m_Current = -1;
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x06001384 RID: 4996 RVA: 0x0005A14C File Offset: 0x0005834C
			public unsafe TControl Current
			{
				get
				{
					if (this.m_Indices == null)
					{
						throw new InvalidOperationException("Enumerator is not valid");
					}
					return InputControlList<TControl>.FromIndex(this.m_Indices[this.m_Current]);
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x06001385 RID: 4997 RVA: 0x0005A179 File Offset: 0x00058379
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x0005A186 File Offset: 0x00058386
			public void Dispose()
			{
			}

			// Token: 0x04000864 RID: 2148
			private unsafe readonly ulong* m_Indices;

			// Token: 0x04000865 RID: 2149
			private readonly int m_Count;

			// Token: 0x04000866 RID: 2150
			private int m_Current;
		}
	}
}
