using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200019A RID: 410
	public sealed class fsDuplicateVersionNameException : Exception
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002D470 File Offset: 0x0002B670
		public fsDuplicateVersionNameException(Type typeA, Type typeB, string version)
			: base(string.Concat(new string[]
			{
				(typeA != null) ? typeA.ToString() : null,
				" and ",
				(typeB != null) ? typeB.ToString() : null,
				" have the same version string (",
				version,
				"); please change one of them."
			}))
		{
		}
	}
}
