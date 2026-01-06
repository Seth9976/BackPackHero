using System;

namespace Unity.Collections
{
	// Token: 0x020000A3 RID: 163
	public interface IUTF8Bytes
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000643 RID: 1603
		bool IsEmpty { get; }

		// Token: 0x06000644 RID: 1604
		unsafe byte* GetUnsafePtr();

		// Token: 0x06000645 RID: 1605
		bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory);
	}
}
