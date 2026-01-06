using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	[StructLayout(0)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		// Token: 0x06000116 RID: 278
		[MethodImpl(4096)]
		private unsafe static extern IntPtr Create(UploadHandlerRaw self, byte* data, int dataLength);

		// Token: 0x06000117 RID: 279 RVA: 0x000050DC File Offset: 0x000032DC
		public UploadHandlerRaw(byte[] data)
			: this((data == null || data.Length == 0) ? default(NativeArray<byte>) : new NativeArray<byte>(data, Allocator.Persistent), true)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000510C File Offset: 0x0000330C
		public unsafe UploadHandlerRaw(NativeArray<byte> data, bool transferOwnership)
		{
			bool flag = !data.IsCreated || data.Length == 0;
			if (flag)
			{
				this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
			}
			else
			{
				if (transferOwnership)
				{
					this.m_Payload = data;
				}
				this.m_Ptr = UploadHandlerRaw.Create(this, (byte*)data.GetUnsafeReadOnlyPtr<byte>(), data.Length);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005178 File Offset: 0x00003378
		public unsafe UploadHandlerRaw(NativeArray<byte>.ReadOnly data)
		{
			bool flag = !data.IsCreated || data.Length == 0;
			if (flag)
			{
				this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
			}
			else
			{
				bool flag2 = data.Length == 0;
				if (flag2)
				{
					this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
				}
				else
				{
					this.m_Ptr = UploadHandlerRaw.Create(this, (byte*)data.GetUnsafeReadOnlyPtr<byte>(), data.Length);
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000051F4 File Offset: 0x000033F4
		internal override byte[] GetData()
		{
			bool isCreated = this.m_Payload.IsCreated;
			byte[] array;
			if (isCreated)
			{
				array = this.m_Payload.ToArray();
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005224 File Offset: 0x00003424
		public override void Dispose()
		{
			bool isCreated = this.m_Payload.IsCreated;
			if (isCreated)
			{
				this.m_Payload.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x0400005D RID: 93
		private NativeArray<byte> m_Payload;
	}
}
