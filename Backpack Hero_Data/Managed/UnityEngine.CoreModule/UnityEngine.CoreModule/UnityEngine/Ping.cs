using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001CD RID: 461
	[NativeHeader("Runtime/Export/Networking/Ping.bindings.h")]
	public sealed class Ping
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x00022C11 File Offset: 0x00020E11
		public Ping(string address)
		{
			this.m_Ptr = Ping.Internal_Create(address);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00022C28 File Offset: 0x00020E28
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00022C58 File Offset: 0x00020E58
		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (!flag)
			{
				Ping.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600158F RID: 5519
		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06001590 RID: 5520
		[FreeFunction("CreatePing")]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(string address);

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00022C94 File Offset: 0x00020E94
		public bool isDone
		{
			get
			{
				bool flag = this.m_Ptr == IntPtr.Zero;
				return !flag && this.Internal_IsDone();
			}
		}

		// Token: 0x06001592 RID: 5522
		[NativeName("GetIsDone")]
		[MethodImpl(4096)]
		private extern bool Internal_IsDone();

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001593 RID: 5523
		public extern int time
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001594 RID: 5524
		public extern string ip
		{
			[NativeName("GetIP")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x040007A1 RID: 1953
		internal IntPtr m_Ptr;
	}
}
