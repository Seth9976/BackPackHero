using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000360 RID: 864
	internal class NegotiationInfoClass
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x00067748 File Offset: 0x00065948
		internal unsafe NegotiationInfoClass(SafeHandle safeHandle, int negotiationState)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle:{0}", new object[] { safeHandle }), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x}", new object[] { intPtr, negotiationState }), ".ctor");
			}
			if (negotiationState == 0 || negotiationState == 1)
			{
				string text = null;
				IntPtr name = ((SecurityPackageInfo*)(void*)intPtr)->Name;
				if (name != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(name);
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x} name:{2}", new object[] { intPtr, negotiationState, text }), ".ctor");
				}
				if (string.Compare(text, "Kerberos", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "Kerberos";
					return;
				}
				if (string.Compare(text, "NTLM", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "NTLM";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x04000E92 RID: 3730
		internal string AuthenticationPackage;

		// Token: 0x04000E93 RID: 3731
		internal const string NTLM = "NTLM";

		// Token: 0x04000E94 RID: 3732
		internal const string Kerberos = "Kerberos";

		// Token: 0x04000E95 RID: 3733
		internal const string Negotiate = "Negotiate";

		// Token: 0x04000E96 RID: 3734
		internal const string Basic = "Basic";
	}
}
