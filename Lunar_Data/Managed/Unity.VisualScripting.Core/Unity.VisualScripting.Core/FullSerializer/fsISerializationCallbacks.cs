using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200019C RID: 412
	public interface fsISerializationCallbacks
	{
		// Token: 0x06000AD0 RID: 2768
		void OnBeforeSerialize(Type storageType);

		// Token: 0x06000AD1 RID: 2769
		void OnAfterSerialize(Type storageType, ref fsData data);

		// Token: 0x06000AD2 RID: 2770
		void OnBeforeDeserialize(Type storageType, ref fsData data);

		// Token: 0x06000AD3 RID: 2771
		void OnAfterDeserialize(Type storageType);
	}
}
