using System;

namespace UnityEngine
{
	// Token: 0x0200010E RID: 270
	public interface IExposedPropertyTable
	{
		// Token: 0x0600067C RID: 1660
		void SetReferenceValue(PropertyName id, Object value);

		// Token: 0x0600067D RID: 1661
		Object GetReferenceValue(PropertyName id, out bool idValid);

		// Token: 0x0600067E RID: 1662
		void ClearReferenceValue(PropertyName id);
	}
}
