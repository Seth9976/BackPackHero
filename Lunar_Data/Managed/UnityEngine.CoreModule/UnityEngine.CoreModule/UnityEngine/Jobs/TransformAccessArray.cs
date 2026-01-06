using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x02000285 RID: 645
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x0002CF72 File Offset: 0x0002B172
		public TransformAccessArray(Transform[] transforms, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(transforms.Length, desiredJobCount, out this);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0002CF8D File Offset: 0x0002B18D
		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0002CF99 File Offset: 0x0002B199
		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0002CFCE File Offset: 0x0002B1CE
		public void Dispose()
		{
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0002CFE8 File Offset: 0x0002B1E8
		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			return this.m_TransformArray;
		}

		// Token: 0x170005A0 RID: 1440
		public Transform this[int index]
		{
			get
			{
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
			set
			{
				TransformAccessArray.SetTransform(this.m_TransformArray, index, value);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0002D030 File Offset: 0x0002B230
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0002D04D File Offset: 0x0002B24D
		public int capacity
		{
			get
			{
				return TransformAccessArray.GetCapacity(this.m_TransformArray);
			}
			set
			{
				TransformAccessArray.SetCapacity(this.m_TransformArray, value);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0002D060 File Offset: 0x0002B260
		public int length
		{
			get
			{
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0002D07D File Offset: 0x0002B27D
		public void Add(Transform transform)
		{
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0002D08D File Offset: 0x0002B28D
		public void RemoveAtSwapBack(int index)
		{
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0002D09D File Offset: 0x0002B29D
		public void SetTransforms(Transform[] transforms)
		{
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001C0E RID: 7182
		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		// Token: 0x06001C0F RID: 7183
		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		// Token: 0x06001C10 RID: 7184
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransforms", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void SetTransforms(IntPtr transformArrayIntPtr, Transform[] transforms);

		// Token: 0x06001C11 RID: 7185
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void Add(IntPtr transformArrayIntPtr, Transform transform);

		// Token: 0x06001C12 RID: 7186
		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		// Token: 0x06001C13 RID: 7187
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		// Token: 0x06001C14 RID: 7188
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		// Token: 0x06001C15 RID: 7189
		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		// Token: 0x06001C16 RID: 7190
		[NativeMethod(Name = "TransformAccessArrayBindings::GetCapacity", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern int GetCapacity(IntPtr transformArrayIntPtr);

		// Token: 0x06001C17 RID: 7191
		[NativeMethod(Name = "TransformAccessArrayBindings::SetCapacity", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void SetCapacity(IntPtr transformArrayIntPtr, int capacity);

		// Token: 0x06001C18 RID: 7192
		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern Transform GetTransform(IntPtr transformArrayIntPtr, int index);

		// Token: 0x06001C19 RID: 7193
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern void SetTransform(IntPtr transformArrayIntPtr, int index, Transform transform);

		// Token: 0x04000921 RID: 2337
		private IntPtr m_TransformArray;
	}
}
