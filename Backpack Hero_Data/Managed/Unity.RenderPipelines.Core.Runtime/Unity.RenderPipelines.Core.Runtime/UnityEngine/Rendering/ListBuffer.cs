using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000057 RID: 87
	public struct ListBuffer<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000EA86 File Offset: 0x0000CC86
		internal unsafe T* BufferPtr
		{
			get
			{
				return this.m_BufferPtr;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		public unsafe int Count
		{
			get
			{
				return *this.m_CountPtr;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000EA97 File Offset: 0x0000CC97
		public int Capacity
		{
			get
			{
				return this.m_Capacity;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000EA9F File Offset: 0x0000CC9F
		public unsafe ListBuffer(T* bufferPtr, int* countPtr, int capacity)
		{
			this.m_BufferPtr = bufferPtr;
			this.m_Capacity = capacity;
			this.m_CountPtr = countPtr;
		}

		// Token: 0x1700004C RID: 76
		public unsafe ref T this[in int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException(string.Format("Expected a value between 0 and {0}, but received {1}.", this.Count, index));
				}
				return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EB07 File Offset: 0x0000CD07
		public unsafe ref T GetUnchecked(in int index)
		{
			return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		public unsafe bool TryAdd(in T value)
		{
			if (this.Count >= this.m_Capacity)
			{
				return false;
			}
			this.m_BufferPtr[(IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			(*this.m_CountPtr)++;
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public unsafe void CopyTo(T* dstBuffer, int startDstIndex, int copyCount)
		{
			UnsafeUtility.MemCpy((void*)(dstBuffer + (IntPtr)startDstIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * copyCount));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public unsafe bool TryCopyTo(ListBuffer<T> other)
		{
			if (other.Count + this.Count >= other.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(other.m_BufferPtr + (IntPtr)other.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * this.Count));
			*other.m_CountPtr += this.Count;
			return true;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public unsafe bool TryCopyFrom(T* srcPtr, int count)
		{
			if (count + this.Count > this.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(this.m_BufferPtr + (IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)srcPtr, (long)(UnsafeUtility.SizeOf<T>() * count));
			*this.m_CountPtr += count;
			return true;
		}

		// Token: 0x040001F3 RID: 499
		private unsafe T* m_BufferPtr;

		// Token: 0x040001F4 RID: 500
		private int m_Capacity;

		// Token: 0x040001F5 RID: 501
		private unsafe int* m_CountPtr;
	}
}
