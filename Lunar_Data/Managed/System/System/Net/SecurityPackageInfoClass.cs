using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200036B RID: 875
	internal class SecurityPackageInfoClass
	{
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00068CD4 File Offset: 0x00066ED4
		internal unsafe SecurityPackageInfoClass(SafeHandle safeHandle, int index)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle: {0}", new object[] { safeHandle }), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle() + sizeof(SecurityPackageInfo) * index;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("unmanagedAddress: {0}", new object[] { intPtr }), ".ctor");
			}
			SecurityPackageInfo* ptr = (SecurityPackageInfo*)(void*)intPtr;
			this.Capabilities = ptr->Capabilities;
			this.Version = ptr->Version;
			this.RPCID = ptr->RPCID;
			this.MaxToken = ptr->MaxToken;
			IntPtr intPtr2 = ptr->Name;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Name = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Name: {0}", new object[] { this.Name }), ".ctor");
				}
			}
			intPtr2 = ptr->Comment;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Comment = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Comment: {0}", new object[] { this.Comment }), ".ctor");
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, this.ToString(), ".ctor");
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00068E3C File Offset: 0x0006703C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Capabilities:",
				string.Format(CultureInfo.InvariantCulture, "0x{0:x}", this.Capabilities),
				" Version:",
				this.Version.ToString(NumberFormatInfo.InvariantInfo),
				" RPCID:",
				this.RPCID.ToString(NumberFormatInfo.InvariantInfo),
				" MaxToken:",
				this.MaxToken.ToString(NumberFormatInfo.InvariantInfo),
				" Name:",
				(this.Name == null) ? "(null)" : this.Name,
				" Comment:",
				(this.Comment == null) ? "(null)" : this.Comment
			});
		}

		// Token: 0x04000EB3 RID: 3763
		internal int Capabilities;

		// Token: 0x04000EB4 RID: 3764
		internal short Version;

		// Token: 0x04000EB5 RID: 3765
		internal short RPCID;

		// Token: 0x04000EB6 RID: 3766
		internal int MaxToken;

		// Token: 0x04000EB7 RID: 3767
		internal string Name;

		// Token: 0x04000EB8 RID: 3768
		internal string Comment;
	}
}
