using System;

namespace System.Net
{
	// Token: 0x02000397 RID: 919
	internal enum FtpLoginState : byte
	{
		// Token: 0x04000FF2 RID: 4082
		NotLoggedIn,
		// Token: 0x04000FF3 RID: 4083
		LoggedIn,
		// Token: 0x04000FF4 RID: 4084
		LoggedInButNeedsRelogin,
		// Token: 0x04000FF5 RID: 4085
		ReloginFailed
	}
}
