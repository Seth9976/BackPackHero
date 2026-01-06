using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h")]
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	[StructLayout(0)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06001635 RID: 5685
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001636 RID: 5686
		public extern bool isDone
		{
			[NativeMethod("IsDone")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001637 RID: 5687
		public extern float progress
		{
			[NativeMethod("GetProgress")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001638 RID: 5688
		// (set) Token: 0x06001639 RID: 5689
		public extern int priority
		{
			[NativeMethod("GetPriority")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPriority")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600163A RID: 5690
		// (set) Token: 0x0600163B RID: 5691
		public extern bool allowSceneActivation
		{
			[NativeMethod("GetAllowSceneActivation")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetAllowSceneActivation")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00023808 File Offset: 0x00021A08
		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00023840 File Offset: 0x00021A40
		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback.Invoke(this);
				this.m_completeCallback = null;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600163E RID: 5694 RVA: 0x00023874 File Offset: 0x00021A74
		// (remove) Token: 0x0600163F RID: 5695 RVA: 0x000238B1 File Offset: 0x00021AB1
		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value.Invoke(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		// Token: 0x040007C6 RID: 1990
		internal IntPtr m_Ptr;

		// Token: 0x040007C7 RID: 1991
		private Action<AsyncOperation> m_completeCallback;
	}
}
