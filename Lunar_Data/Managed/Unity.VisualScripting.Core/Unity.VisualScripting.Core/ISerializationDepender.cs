using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000134 RID: 308
	public interface ISerializationDepender : ISerializationCallbackReceiver
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000869 RID: 2153
		IEnumerable<ISerializationDependency> deserializationDependencies { get; }

		// Token: 0x0600086A RID: 2154
		void OnAfterDependenciesDeserialized();
	}
}
