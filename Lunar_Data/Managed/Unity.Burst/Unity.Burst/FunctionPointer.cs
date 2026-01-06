using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000014 RID: 20
	public readonly struct FunctionPointer<T> : IFunctionPointer
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000053CD File Offset: 0x000035CD
		public FunctionPointer(IntPtr ptr)
		{
			this._ptr = ptr;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000053D6 File Offset: 0x000035D6
		public IntPtr Value
		{
			get
			{
				return this._ptr;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000053DE File Offset: 0x000035DE
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIsCreated()
		{
			if (!this.IsCreated)
			{
				throw new NullReferenceException("Object reference not set to an instance of an object");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000053F3 File Offset: 0x000035F3
		public T Invoke
		{
			get
			{
				return Marshal.GetDelegateForFunctionPointer<T>(this._ptr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005400 File Offset: 0x00003600
		public bool IsCreated
		{
			get
			{
				return this._ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005412 File Offset: 0x00003612
		IFunctionPointer IFunctionPointer.FromIntPtr(IntPtr ptr)
		{
			return new FunctionPointer<T>(ptr);
		}

		// Token: 0x0400013E RID: 318
		[NativeDisableUnsafePtrRestriction]
		private readonly IntPtr _ptr;
	}
}
