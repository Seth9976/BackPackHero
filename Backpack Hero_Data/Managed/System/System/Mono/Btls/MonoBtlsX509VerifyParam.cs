using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x02000110 RID: 272
	internal class MonoBtlsX509VerifyParam : MonoBtlsObject
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000117F1 File Offset: 0x0000F9F1
		internal new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle Handle
		{
			get
			{
				return (MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle)base.Handle;
			}
		}

		// Token: 0x06000653 RID: 1619
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_new();

		// Token: 0x06000654 RID: 1620
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_copy(IntPtr handle);

		// Token: 0x06000655 RID: 1621
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_lookup(IntPtr name);

		// Token: 0x06000656 RID: 1622
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_can_modify(IntPtr param);

		// Token: 0x06000657 RID: 1623
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_name(IntPtr handle, IntPtr name);

		// Token: 0x06000658 RID: 1624
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x06000659 RID: 1625
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_add_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x0600065A RID: 1626
		[DllImport("libmono-btls-shared")]
		private static extern ulong mono_btls_x509_verify_param_get_flags(IntPtr handle);

		// Token: 0x0600065B RID: 1627
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_flags(IntPtr handle, ulong flags);

		// Token: 0x0600065C RID: 1628
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509VerifyFlags mono_btls_x509_verify_param_get_mono_flags(IntPtr handle);

		// Token: 0x0600065D RID: 1629
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_mono_flags(IntPtr handle, MonoBtlsX509VerifyFlags flags);

		// Token: 0x0600065E RID: 1630
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_purpose(IntPtr handle, MonoBtlsX509Purpose purpose);

		// Token: 0x0600065F RID: 1631
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_get_depth(IntPtr handle);

		// Token: 0x06000660 RID: 1632
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_depth(IntPtr handle, int depth);

		// Token: 0x06000661 RID: 1633
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_time(IntPtr handle, long time);

		// Token: 0x06000662 RID: 1634
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_get_peername(IntPtr handle);

		// Token: 0x06000663 RID: 1635
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_verify_param_free(IntPtr handle);

		// Token: 0x06000664 RID: 1636 RVA: 0x000117FE File Offset: 0x0000F9FE
		internal MonoBtlsX509VerifyParam()
			: base(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_new()))
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		internal MonoBtlsX509VerifyParam(MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle handle)
			: base(handle)
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00011810 File Offset: 0x0000FA10
		public MonoBtlsX509VerifyParam Copy()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_copy(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001184F File Offset: 0x0000FA4F
		public static MonoBtlsX509VerifyParam GetSslClient()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_client", true);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001185C File Offset: 0x0000FA5C
		public static MonoBtlsX509VerifyParam GetSslServer()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_server", true);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001186C File Offset: 0x0000FA6C
		public static MonoBtlsX509VerifyParam Lookup(string name, bool fail = false)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			MonoBtlsX509VerifyParam monoBtlsX509VerifyParam;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				intPtr2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_lookup(intPtr);
				if (intPtr2 == IntPtr.Zero)
				{
					if (fail)
					{
						throw new MonoBtlsException("X509_VERIFY_PARAM_lookup() could not find '{0}'.", new object[] { name });
					}
					monoBtlsX509VerifyParam = null;
				}
				else
				{
					monoBtlsX509VerifyParam = new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr2));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return monoBtlsX509VerifyParam;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public bool CanModify
		{
			get
			{
				return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_can_modify(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00011905 File Offset: 0x0000FB05
		private void WantToModify()
		{
			if (!this.CanModify)
			{
				throw new MonoBtlsException("Attempting to modify read-only MonoBtlsX509VerifyParam instance.");
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001191C File Offset: 0x0000FB1C
		public void SetName(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00011980 File Offset: 0x0000FB80
		public void SetHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(num, "SetHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000119EC File Offset: 0x0000FBEC
		public void AddHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_add_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(num, "AddHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00011A58 File Offset: 0x0000FC58
		public ulong GetFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public void SetFlags(ulong flags)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(num, "SetFlags");
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00011A9D File Offset: 0x0000FC9D
		public MonoBtlsX509VerifyFlags GetMonoFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_mono_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public void SetMonoFlags(MonoBtlsX509VerifyFlags flags)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_mono_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(num, "SetMonoFlags");
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public void SetPurpose(MonoBtlsX509Purpose purpose)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_purpose(this.Handle.DangerousGetHandle(), purpose);
			base.CheckError(num, "SetPurpose");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00011B15 File Offset: 0x0000FD15
		public int GetDepth()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_depth(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00011B28 File Offset: 0x0000FD28
		public void SetDepth(int depth)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_depth(this.Handle.DangerousGetHandle(), depth);
			base.CheckError(num, "SetDepth");
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public void SetTime(DateTime time)
		{
			this.WantToModify();
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long num = (long)time.Subtract(dateTime).TotalSeconds;
			int num2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_time(this.Handle.DangerousGetHandle(), num);
			base.CheckError(num2, "SetTime");
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		public string GetPeerName()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_peername(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x02000111 RID: 273
		internal class BoringX509VerifyParamHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000678 RID: 1656 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
			public BoringX509VerifyParamHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x00011BE7 File Offset: 0x0000FDE7
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_free(this.handle);
				return true;
			}
		}
	}
}
