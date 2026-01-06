using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000014 RID: 20
	public readonly struct FunctionPointer<T> : IFunctionPointer
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000054A1 File Offset: 0x000036A1
		public FunctionPointer(IntPtr ptr)
		{
			this._ptr = ptr;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000054AA File Offset: 0x000036AA
		public IntPtr Value
		{
			get
			{
				return this._ptr;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000054B2 File Offset: 0x000036B2
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIsCreated()
		{
			if (!this.IsCreated)
			{
				throw new NullReferenceException("Object reference not set to an instance of an object");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000054C7 File Offset: 0x000036C7
		public T Invoke
		{
			get
			{
				return Marshal.GetDelegateForFunctionPointer<T>(this._ptr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000054D4 File Offset: 0x000036D4
		public bool IsCreated
		{
			get
			{
				return this._ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000054E6 File Offset: 0x000036E6
		IFunctionPointer IFunctionPointer.FromIntPtr(IntPtr ptr)
		{
			return new FunctionPointer<T>(ptr);
		}

		// Token: 0x0400013B RID: 315
		[NativeDisableUnsafePtrRestriction]
		private readonly IntPtr _ptr;
	}
}
