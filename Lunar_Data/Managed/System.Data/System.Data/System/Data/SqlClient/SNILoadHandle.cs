using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200021A RID: 538
	internal sealed class SNILoadHandle : SafeHandle
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x0007DC90 File Offset: 0x0007BE90
		private SNILoadHandle()
			: base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._sniStatus = SNINativeMethodWrapper.SNIInitialize();
				uint num = 0U;
				if (this._sniStatus == 0U)
				{
					SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE, ref num);
				}
				this._encryptionOption = ((num == 0U) ? EncryptionOptions.NOT_SUP : EncryptionOptions.OFF);
				this.handle = (IntPtr)1;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0007DD20 File Offset: 0x0007BF20
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0007DD32 File Offset: 0x0007BF32
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				if (this._sniStatus == 0U)
				{
					LocalDBAPI.ReleaseDLLHandles();
					SNINativeMethodWrapper.SNITerminate();
				}
				this.handle = IntPtr.Zero;
			}
			return true;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0007DD65 File Offset: 0x0007BF65
		public uint Status
		{
			get
			{
				return this._sniStatus;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0007DD6D File Offset: 0x0007BF6D
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0007DD78 File Offset: 0x0007BF78
		private static void ReadDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.ReadAsyncCallback<IntPtr>(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0007DDB8 File Offset: 0x0007BFB8
		private static void WriteDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.WriteAsyncCallback<IntPtr>(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x0400120F RID: 4623
		internal static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x04001210 RID: 4624
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate ReadAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.ReadDispatcher);

		// Token: 0x04001211 RID: 4625
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate WriteAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.WriteDispatcher);

		// Token: 0x04001212 RID: 4626
		private readonly uint _sniStatus = uint.MaxValue;

		// Token: 0x04001213 RID: 4627
		private readonly EncryptionOptions _encryptionOption;
	}
}
