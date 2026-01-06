using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000133 RID: 307
	public interface ISerializationDependency : ISerializationCallbackReceiver
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000868 RID: 2152
		// (set) Token: 0x06000867 RID: 2151
		bool IsDeserialized { get; set; }
	}
}
