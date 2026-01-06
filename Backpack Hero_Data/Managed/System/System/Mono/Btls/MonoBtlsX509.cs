using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x020000F1 RID: 241
	internal class MonoBtlsX509 : MonoBtlsObject
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000F8D2 File Offset: 0x0000DAD2
		internal new MonoBtlsX509.BoringX509Handle Handle
		{
			get
			{
				return (MonoBtlsX509.BoringX509Handle)base.Handle;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		internal MonoBtlsX509(MonoBtlsX509.BoringX509Handle handle)
			: base(handle)
		{
		}

		// Token: 0x06000553 RID: 1363
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_up_ref(IntPtr handle);

		// Token: 0x06000554 RID: 1364
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_from_data(IntPtr data, int len, MonoBtlsX509Format format);

		// Token: 0x06000555 RID: 1365
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_get_subject_name(IntPtr handle);

		// Token: 0x06000556 RID: 1366
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_get_issuer_name(IntPtr handle);

		// Token: 0x06000557 RID: 1367
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_subject_name_string(IntPtr handle, IntPtr buffer, int size);

		// Token: 0x06000558 RID: 1368
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_issuer_name_string(IntPtr handle, IntPtr buffer, int size);

		// Token: 0x06000559 RID: 1369
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_raw_data(IntPtr handle, IntPtr bio, MonoBtlsX509Format format);

		// Token: 0x0600055A RID: 1370
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_cmp(IntPtr a, IntPtr b);

		// Token: 0x0600055B RID: 1371
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_hash(IntPtr handle, out IntPtr data);

		// Token: 0x0600055C RID: 1372
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_get_not_before(IntPtr handle);

		// Token: 0x0600055D RID: 1373
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_get_not_after(IntPtr handle);

		// Token: 0x0600055E RID: 1374
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_public_key(IntPtr handle, IntPtr bio);

		// Token: 0x0600055F RID: 1375
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_serial_number(IntPtr handle, IntPtr data, int size, int mono_style);

		// Token: 0x06000560 RID: 1376
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_version(IntPtr handle);

		// Token: 0x06000561 RID: 1377
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_signature_algorithm(IntPtr handle, IntPtr buffer, int size);

		// Token: 0x06000562 RID: 1378
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_public_key_asn1(IntPtr handle, IntPtr oid, int oid_size, out IntPtr data, out int size);

		// Token: 0x06000563 RID: 1379
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_public_key_parameters(IntPtr handle, IntPtr oid, int oid_size, out IntPtr data, out int size);

		// Token: 0x06000564 RID: 1380
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_get_pubkey(IntPtr handle);

		// Token: 0x06000565 RID: 1381
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_subject_key_identifier(IntPtr handle, out IntPtr data, out int size);

		// Token: 0x06000566 RID: 1382
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_print(IntPtr handle, IntPtr bio);

		// Token: 0x06000567 RID: 1383
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_free(IntPtr handle);

		// Token: 0x06000568 RID: 1384
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_dup(IntPtr handle);

		// Token: 0x06000569 RID: 1385
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_add_trust_object(IntPtr handle, MonoBtlsX509Purpose purpose);

		// Token: 0x0600056A RID: 1386
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_add_reject_object(IntPtr handle, MonoBtlsX509Purpose purpose);

		// Token: 0x0600056B RID: 1387
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_add_explicit_trust(IntPtr handle, MonoBtlsX509TrustKind kind);

		// Token: 0x0600056C RID: 1388 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		internal MonoBtlsX509 Copy()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000F920 File Offset: 0x0000DB20
		internal MonoBtlsX509 Duplicate()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_dup(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Duplicate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000F960 File Offset: 0x0000DB60
		public static MonoBtlsX509 LoadFromData(byte[] buffer, MonoBtlsX509Format format)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(buffer.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			MonoBtlsX509 monoBtlsX;
			try
			{
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
				IntPtr intPtr2 = MonoBtlsX509.mono_btls_x509_from_data(intPtr, buffer.Length, format);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new MonoBtlsException("Failed to read certificate from data.");
				}
				monoBtlsX = new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr2));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return monoBtlsX;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		public MonoBtlsX509Name GetSubjectName()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_get_subject_name(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetSubjectName");
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		public string GetSubjectNameString()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string text;
			try
			{
				int num = MonoBtlsX509.mono_btls_x509_get_subject_name_string(this.Handle.DangerousGetHandle(), intPtr, 4096);
				base.CheckError(num, "GetSubjectNameString");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public long GetSubjectNameHash()
		{
			base.CheckThrow();
			long hash;
			using (MonoBtlsX509Name subjectName = this.GetSubjectName())
			{
				hash = subjectName.GetHash();
			}
			return hash;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		public MonoBtlsX509Name GetIssuerName()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_get_issuer_name(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetIssuerName");
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public string GetIssuerNameString()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string text;
			try
			{
				int num = MonoBtlsX509.mono_btls_x509_get_issuer_name_string(this.Handle.DangerousGetHandle(), intPtr, 4096);
				base.CheckError(num, "GetIssuerNameString");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public byte[] GetRawData(MonoBtlsX509Format format)
		{
			byte[] data;
			using (MonoBtlsBioMemory monoBtlsBioMemory = new MonoBtlsBioMemory())
			{
				int num = MonoBtlsX509.mono_btls_x509_get_raw_data(this.Handle.DangerousGetHandle(), monoBtlsBioMemory.Handle.DangerousGetHandle(), format);
				base.CheckError(num, "GetRawData");
				data = monoBtlsBioMemory.GetData();
			}
			return data;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		public void GetRawData(MonoBtlsBio bio, MonoBtlsX509Format format)
		{
			base.CheckThrow();
			int num = MonoBtlsX509.mono_btls_x509_get_raw_data(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle(), format);
			base.CheckError(num, "GetRawData");
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public static int Compare(MonoBtlsX509 a, MonoBtlsX509 b)
		{
			return MonoBtlsX509.mono_btls_x509_cmp(a.Handle.DangerousGetHandle(), b.Handle.DangerousGetHandle());
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000FC10 File Offset: 0x0000DE10
		public byte[] GetCertHash()
		{
			IntPtr intPtr;
			int num = MonoBtlsX509.mono_btls_x509_get_hash(this.Handle.DangerousGetHandle(), out intPtr);
			base.CheckError(num > 0, "GetCertHash");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000FC50 File Offset: 0x0000DE50
		public DateTime GetNotBefore()
		{
			long num = MonoBtlsX509.mono_btls_x509_get_not_before(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public DateTime GetNotAfter()
		{
			long num = MonoBtlsX509.mono_btls_x509_get_not_after(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public byte[] GetPublicKeyData()
		{
			byte[] data;
			using (MonoBtlsBioMemory monoBtlsBioMemory = new MonoBtlsBioMemory())
			{
				int num = MonoBtlsX509.mono_btls_x509_get_public_key(this.Handle.DangerousGetHandle(), monoBtlsBioMemory.Handle.DangerousGetHandle());
				base.CheckError(num > 0, "GetPublicKeyData");
				data = monoBtlsBioMemory.GetData();
			}
			return data;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000FD24 File Offset: 0x0000DF24
		public byte[] GetSerialNumber(bool mono_style)
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			byte[] array2;
			try
			{
				int num2 = MonoBtlsX509.mono_btls_x509_get_serial_number(this.Handle.DangerousGetHandle(), intPtr, num, mono_style ? 1 : 0);
				base.CheckError(num2 > 0, "GetSerialNumber");
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr, array, 0, num2);
				array2 = array;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return array2;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public int GetVersion()
		{
			return MonoBtlsX509.mono_btls_x509_get_version(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		public string GetSignatureAlgorithm()
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			string text;
			try
			{
				int num2 = MonoBtlsX509.mono_btls_x509_get_signature_algorithm(this.Handle.DangerousGetHandle(), intPtr, num);
				base.CheckError(num2 > 0, "GetSignatureAlgorithm");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000FE18 File Offset: 0x0000E018
		public AsnEncodedData GetPublicKeyAsn1()
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(256);
			IntPtr intPtr2;
			int num3;
			string text;
			try
			{
				int num2 = MonoBtlsX509.mono_btls_x509_get_public_key_asn1(this.Handle.DangerousGetHandle(), intPtr, num, out intPtr2, out num3);
				base.CheckError(num2, "GetPublicKeyAsn1");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			AsnEncodedData asnEncodedData;
			try
			{
				byte[] array = new byte[num3];
				Marshal.Copy(intPtr2, array, 0, num3);
				asnEncodedData = new AsnEncodedData(text.ToString(), array);
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					base.FreeDataPtr(intPtr2);
				}
			}
			return asnEncodedData;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public AsnEncodedData GetPublicKeyParameters()
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(256);
			IntPtr intPtr2;
			int num3;
			string text;
			try
			{
				int num2 = MonoBtlsX509.mono_btls_x509_get_public_key_parameters(this.Handle.DangerousGetHandle(), intPtr, num, out intPtr2, out num3);
				base.CheckError(num2, "GetPublicKeyParameters");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			AsnEncodedData asnEncodedData;
			try
			{
				byte[] array = new byte[num3];
				Marshal.Copy(intPtr2, array, 0, num3);
				asnEncodedData = new AsnEncodedData(text.ToString(), array);
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					base.FreeDataPtr(intPtr2);
				}
			}
			return asnEncodedData;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000FF70 File Offset: 0x0000E170
		public byte[] GetSubjectKeyIdentifier()
		{
			IntPtr zero = IntPtr.Zero;
			byte[] array2;
			try
			{
				int num2;
				int num = MonoBtlsX509.mono_btls_x509_get_subject_key_identifier(this.Handle.DangerousGetHandle(), out zero, out num2);
				base.CheckError(num, "GetSubjectKeyIdentifier");
				byte[] array = new byte[num2];
				Marshal.Copy(zero, array, 0, num2);
				array2 = array;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					base.FreeDataPtr(zero);
				}
			}
			return array2;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public MonoBtlsKey GetPublicKey()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_get_pubkey(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetPublicKey");
			return new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(intPtr));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00010020 File Offset: 0x0000E220
		public void Print(MonoBtlsBio bio)
		{
			int num = MonoBtlsX509.mono_btls_x509_print(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
			base.CheckError(num, "Print");
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00010058 File Offset: 0x0000E258
		public void ExportAsPEM(MonoBtlsBio bio, bool includeHumanReadableForm)
		{
			this.GetRawData(bio, MonoBtlsX509Format.PEM);
			if (!includeHumanReadableForm)
			{
				return;
			}
			this.Print(bio);
			byte[] certHash = this.GetCertHash();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SHA1 Fingerprint=");
			for (int i = 0; i < certHash.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(":");
				}
				stringBuilder.AppendFormat("{0:X2}", certHash[i]);
			}
			stringBuilder.AppendLine();
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			bio.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public void AddTrustObject(MonoBtlsX509Purpose purpose)
		{
			base.CheckThrow();
			int num = MonoBtlsX509.mono_btls_x509_add_trust_object(this.Handle.DangerousGetHandle(), purpose);
			base.CheckError(num, "AddTrustObject");
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001011C File Offset: 0x0000E31C
		public void AddRejectObject(MonoBtlsX509Purpose purpose)
		{
			base.CheckThrow();
			int num = MonoBtlsX509.mono_btls_x509_add_reject_object(this.Handle.DangerousGetHandle(), purpose);
			base.CheckError(num, "AddRejectObject");
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00010150 File Offset: 0x0000E350
		public void AddExplicitTrust(MonoBtlsX509TrustKind kind)
		{
			base.CheckThrow();
			int num = MonoBtlsX509.mono_btls_x509_add_explicit_trust(this.Handle.DangerousGetHandle(), kind);
			base.CheckError(num, "AddExplicitTrust");
		}

		// Token: 0x020000F2 RID: 242
		internal class BoringX509Handle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000587 RID: 1415 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
			public BoringX509Handle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x00010181 File Offset: 0x0000E381
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsX509.mono_btls_x509_free(this.handle);
				}
				return true;
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x000101A1 File Offset: 0x0000E3A1
			public IntPtr StealHandle()
			{
				return Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			}
		}
	}
}
