using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000039 RID: 57
	internal class DependencyTreeSortFailedException : Exception
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00002E24 File Offset: 0x00001024
		public DependencyTreeSortFailedException(DependencyTree tree, ICollection<int> target)
			: base(DependencyTreeSortFailedException.CreateExceptionMessage(tree, target, null))
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002E34 File Offset: 0x00001034
		public DependencyTreeSortFailedException(DependencyTree tree, ICollection<int> target, Exception inner)
			: base(DependencyTreeSortFailedException.CreateExceptionMessage(tree, target, inner), inner)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002E48 File Offset: 0x00001048
		private static string CreateExceptionMessage(DependencyTree tree, ICollection<int> target, Exception inner = null)
		{
			string text = tree.ToJson(target);
			return "Failed to sort tree! It is likely there is a missing required dependency:\n" + text + ((inner != null) ? ("\n Error: " + inner.Message) : string.Empty);
		}
	}
}
