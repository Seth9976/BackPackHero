using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000042 RID: 66
	internal interface ISerializableJsonDictionary
	{
		// Token: 0x060001A0 RID: 416
		void Set<T>(string key, T value) where T : class;

		// Token: 0x060001A1 RID: 417
		T Get<T>(string key) where T : class;

		// Token: 0x060001A2 RID: 418
		T GetScriptable<T>(string key) where T : ScriptableObject;

		// Token: 0x060001A3 RID: 419
		void Overwrite(object obj, string key);

		// Token: 0x060001A4 RID: 420
		bool ContainsKey(string key);

		// Token: 0x060001A5 RID: 421
		void OnBeforeSerialize();

		// Token: 0x060001A6 RID: 422
		void OnAfterDeserialize();
	}
}
