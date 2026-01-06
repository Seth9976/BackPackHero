using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000171 RID: 369
	public sealed class MissingValuePortInputException : Exception
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public MissingValuePortInputException(string key)
			: base("Missing input value for '" + key + "'.")
		{
		}
	}
}
