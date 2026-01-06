using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000199 RID: 409
	public sealed class fsMissingVersionConstructorException : Exception
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x0002D441 File Offset: 0x0002B641
		public fsMissingVersionConstructorException(Type versionedType, Type constructorType)
			: base(((versionedType != null) ? versionedType.ToString() : null) + " is missing a constructor for previous model type " + ((constructorType != null) ? constructorType.ToString() : null))
		{
		}
	}
}
