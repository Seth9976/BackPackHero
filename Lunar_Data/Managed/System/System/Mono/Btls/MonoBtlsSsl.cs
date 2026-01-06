using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000E5 RID: 229
	internal class MonoBtlsSsl : MonoBtlsObject
	{
		// Token: 0x060004D6 RID: 1238
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_destroy(IntPtr handle);

		// Token: 0x060004D7 RID: 1239
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_new(IntPtr handle);

		// Token: 0x060004D8 RID: 1240
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060004D9 RID: 1241
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_private_key(IntPtr handle, IntPtr key);

		// Token: 0x060004DA RID: 1242
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_add_chain_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060004DB RID: 1243
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_accept(IntPtr handle);

		// Token: 0x060004DC RID: 1244
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_connect(IntPtr handle);

		// Token: 0x060004DD RID: 1245
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_handshake(IntPtr handle);

		// Token: 0x060004DE RID: 1246
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_close(IntPtr handle);

		// Token: 0x060004DF RID: 1247
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_shutdown(IntPtr handle);

		// Token: 0x060004E0 RID: 1248
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_quiet_shutdown(IntPtr handle, int mode);

		// Token: 0x060004E1 RID: 1249
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_bio(IntPtr handle, IntPtr bio);

		// Token: 0x060004E2 RID: 1250
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_read(IntPtr handle, IntPtr data, int len);

		// Token: 0x060004E3 RID: 1251
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_write(IntPtr handle, IntPtr data, int len);

		// Token: 0x060004E4 RID: 1252
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_error(IntPtr handle, int ret_code);

		// Token: 0x060004E5 RID: 1253
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_version(IntPtr handle);

		// Token: 0x060004E6 RID: 1254
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_min_version(IntPtr handle, int version);

		// Token: 0x060004E7 RID: 1255
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_max_version(IntPtr handle, int version);

		// Token: 0x060004E8 RID: 1256
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_cipher(IntPtr handle);

		// Token: 0x060004E9 RID: 1257
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_ciphers(IntPtr handle, out IntPtr data);

		// Token: 0x060004EA RID: 1258
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_peer_certificate(IntPtr handle);

		// Token: 0x060004EB RID: 1259
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_cipher_list(IntPtr handle, IntPtr str);

		// Token: 0x060004EC RID: 1260
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_print_errors_cb(IntPtr func, IntPtr ctx);

		// Token: 0x060004ED RID: 1261
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_verify_param(IntPtr handle, IntPtr param);

		// Token: 0x060004EE RID: 1262
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_server_name(IntPtr handle, IntPtr name);

		// Token: 0x060004EF RID: 1263
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_server_name(IntPtr handle);

		// Token: 0x060004F0 RID: 1264
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_renegotiate_mode(IntPtr handle, int mode);

		// Token: 0x060004F1 RID: 1265
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_renegotiate_pending(IntPtr handle);

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		private static MonoBtlsSsl.BoringSslHandle Create_internal(MonoBtlsSslCtx ctx)
		{
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_new(ctx.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsSsl.BoringSslHandle(intPtr);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000E8F2 File Offset: 0x0000CAF2
		public MonoBtlsSsl(MonoBtlsSslCtx ctx)
			: base(MonoBtlsSsl.Create_internal(ctx))
		{
			this.printErrorsFunc = new MonoBtlsSsl.PrintErrorsCallbackFunc(MonoBtlsSsl.PrintErrorsCallback);
			this.printErrorsFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSsl.PrintErrorsCallbackFunc>(this.printErrorsFunc);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000E923 File Offset: 0x0000CB23
		internal new MonoBtlsSsl.BoringSslHandle Handle
		{
			get
			{
				return (MonoBtlsSsl.BoringSslHandle)base.Handle;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000E930 File Offset: 0x0000CB30
		public void SetBio(MonoBtlsBio bio)
		{
			base.CheckThrow();
			this.bio = bio;
			MonoBtlsSsl.mono_btls_ssl_set_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000E95C File Offset: 0x0000CB5C
		private Exception ThrowError([CallerMemberName] string callerName = null)
		{
			string text;
			try
			{
				if (callerName == null)
				{
					callerName = base.GetType().Name;
				}
				text = this.GetErrors();
			}
			catch
			{
				text = null;
			}
			if (text != null)
			{
				throw new MonoBtlsException("{0} failed: {1}.", new object[] { callerName, text });
			}
			throw new MonoBtlsException("{0} failed.", new object[] { callerName });
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		private MonoBtlsSslError GetError(int ret_code)
		{
			base.CheckThrow();
			this.bio.CheckLastError("GetError");
			return (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), ret_code);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000E9F1 File Offset: 0x0000CBF1
		public void SetCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetCertificate");
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000EA23 File Offset: 0x0000CC23
		public void SetPrivateKey(MonoBtlsKey key)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_private_key(this.Handle.DangerousGetHandle(), key.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetPrivateKey");
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000EA55 File Offset: 0x0000CC55
		public void AddIntermediateCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_add_chain_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("AddIntermediateCertificate");
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public MonoBtlsSslError Accept()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_accept(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public MonoBtlsSslError Connect()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_connect(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public MonoBtlsSslError Handshake()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_handshake(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		[MonoPInvokeCallback(typeof(MonoBtlsSsl.PrintErrorsCallbackFunc))]
		private static int PrintErrorsCallback(IntPtr str, IntPtr len, IntPtr ctx)
		{
			StringBuilder stringBuilder = (StringBuilder)GCHandle.FromIntPtr(ctx).Target;
			int num;
			try
			{
				string text = Marshal.PtrToStringAnsi(str, (int)len);
				stringBuilder.Append(text);
				num = 1;
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		public string GetErrors()
		{
			StringBuilder stringBuilder = new StringBuilder();
			GCHandle gchandle = GCHandle.Alloc(stringBuilder);
			string text;
			try
			{
				MonoBtlsSsl.mono_btls_ssl_print_errors_cb(this.printErrorsFuncPtr, GCHandle.ToIntPtr(gchandle));
				text = stringBuilder.ToString();
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return text;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public void PrintErrors()
		{
			string errors = this.GetErrors();
			if (string.IsNullOrEmpty(errors))
			{
				return;
			}
			Console.Error.WriteLine(errors);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public MonoBtlsSslError Read(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_read(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num > 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError error = this.GetError(num);
			if (num == 0 && error == MonoBtlsSslError.Syscall)
			{
				dataSize = 0;
				return MonoBtlsSslError.None;
			}
			dataSize = 0;
			return error;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public MonoBtlsSslError Write(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_write(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num >= 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError monoBtlsSslError = (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), num);
			dataSize = 0;
			return monoBtlsSslError;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public int GetVersion()
		{
			base.CheckThrow();
			return MonoBtlsSsl.mono_btls_ssl_get_version(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public void SetMinVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_min_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public void SetMaxVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_max_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public int GetCipher()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_get_cipher(this.Handle.DangerousGetHandle());
			base.CheckError(num > 0, "GetCipher");
			return num;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public short[] GetCiphers()
		{
			base.CheckThrow();
			IntPtr intPtr;
			int num = MonoBtlsSsl.mono_btls_ssl_get_ciphers(this.Handle.DangerousGetHandle(), out intPtr);
			base.CheckError(num > 0, "GetCiphers");
			short[] array2;
			try
			{
				short[] array = new short[num];
				Marshal.Copy(intPtr, array, 0, num);
				array2 = array;
			}
			finally
			{
				base.FreeDataPtr(intPtr);
			}
			return array2;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		public void SetCipherList(string str)
		{
			base.CheckThrow();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(str);
				int num = MonoBtlsSsl.mono_btls_ssl_set_cipher_list(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetCipherList");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public MonoBtlsX509 GetPeerCertificate()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_peer_certificate(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_set_verify_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(num, "SetVerifyParam");
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public void SetServerName(string name)
		{
			base.CheckThrow();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsSsl.mono_btls_ssl_set_server_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetServerName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000EE90 File Offset: 0x0000D090
		public string GetServerName()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_server_name(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000EEC9 File Offset: 0x0000D0C9
		public void Shutdown()
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_shutdown(this.Handle.DangerousGetHandle()) < 0)
			{
				throw this.ThrowError("Shutdown");
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public void SetQuietShutdown()
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_quiet_shutdown(this.Handle.DangerousGetHandle(), 1);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000EF09 File Offset: 0x0000D109
		protected override void Close()
		{
			if (!this.Handle.IsInvalid)
			{
				MonoBtlsSsl.mono_btls_ssl_close(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000EF28 File Offset: 0x0000D128
		public void SetRenegotiateMode(MonoBtlsSslRenegotiateMode mode)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_renegotiate_mode(this.Handle.DangerousGetHandle(), (int)mode);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000EF41 File Offset: 0x0000D141
		public bool RenegotiatePending()
		{
			return MonoBtlsSsl.mono_btls_ssl_renegotiate_pending(this.Handle.DangerousGetHandle()) != 0;
		}

		// Token: 0x040003B2 RID: 946
		private MonoBtlsBio bio;

		// Token: 0x040003B3 RID: 947
		private MonoBtlsSsl.PrintErrorsCallbackFunc printErrorsFunc;

		// Token: 0x040003B4 RID: 948
		private IntPtr printErrorsFuncPtr;

		// Token: 0x020000E6 RID: 230
		internal class BoringSslHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000512 RID: 1298 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
			public BoringSslHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0000EF56 File Offset: 0x0000D156
			protected override bool ReleaseHandle()
			{
				MonoBtlsSsl.mono_btls_ssl_destroy(this.handle);
				this.handle = IntPtr.Zero;
				return true;
			}
		}

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x06000515 RID: 1301
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int PrintErrorsCallbackFunc(IntPtr str, IntPtr len, IntPtr ctx);
	}
}
