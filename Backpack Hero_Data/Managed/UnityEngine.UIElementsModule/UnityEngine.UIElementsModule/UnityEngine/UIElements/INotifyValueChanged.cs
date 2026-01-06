using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000148 RID: 328
	public interface INotifyValueChanged<T>
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A85 RID: 2693
		// (set) Token: 0x06000A86 RID: 2694
		T value { get; set; }

		// Token: 0x06000A87 RID: 2695
		void SetValueWithoutNotify(T newValue);
	}
}
