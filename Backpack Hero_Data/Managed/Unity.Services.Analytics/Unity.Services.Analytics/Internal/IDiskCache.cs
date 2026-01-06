using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000031 RID: 49
	internal interface IDiskCache
	{
		// Token: 0x06000102 RID: 258
		void Clear();

		// Token: 0x06000103 RID: 259
		void Write(List<int> eventEndIndices, Stream payload);

		// Token: 0x06000104 RID: 260
		bool Read(List<int> eventEndIndices, Stream buffer);
	}
}
