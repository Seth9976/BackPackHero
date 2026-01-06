using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000235 RID: 565
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x060017F9 RID: 6137
		[RequiredByNativeCode]
		void OnBeforeSerialize();

		// Token: 0x060017FA RID: 6138
		[RequiredByNativeCode]
		void OnAfterDeserialize();
	}
}
