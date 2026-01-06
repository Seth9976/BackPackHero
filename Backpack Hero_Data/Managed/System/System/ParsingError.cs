using System;

namespace System
{
	// Token: 0x0200015A RID: 346
	internal enum ParsingError
	{
		// Token: 0x0400062B RID: 1579
		None,
		// Token: 0x0400062C RID: 1580
		BadFormat,
		// Token: 0x0400062D RID: 1581
		BadScheme,
		// Token: 0x0400062E RID: 1582
		BadAuthority,
		// Token: 0x0400062F RID: 1583
		EmptyUriString,
		// Token: 0x04000630 RID: 1584
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x04000631 RID: 1585
		SchemeLimit,
		// Token: 0x04000632 RID: 1586
		SizeLimit,
		// Token: 0x04000633 RID: 1587
		MustRootedPath,
		// Token: 0x04000634 RID: 1588
		BadHostName,
		// Token: 0x04000635 RID: 1589
		NonEmptyHost,
		// Token: 0x04000636 RID: 1590
		BadPort,
		// Token: 0x04000637 RID: 1591
		BadAuthorityTerminator,
		// Token: 0x04000638 RID: 1592
		CannotCreateRelative
	}
}
