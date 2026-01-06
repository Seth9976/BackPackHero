using System;
using Unity.VisualScripting.FullSerializer;

namespace Unity.VisualScripting
{
	// Token: 0x0200013A RID: 314
	public class SerializationVersionAttribute : fsObjectAttribute
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x00026266 File Offset: 0x00024466
		public SerializationVersionAttribute(string versionString, params Type[] previousModels)
			: base(versionString, previousModels)
		{
		}
	}
}
