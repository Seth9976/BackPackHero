using System;

namespace UnityEngine
{
	// Token: 0x02000258 RID: 600
	public struct DrivenRectTransformTracker
	{
		// Token: 0x060019F4 RID: 6644 RVA: 0x00029F14 File Offset: 0x00028114
		internal static bool CanRecordModifications()
		{
			return true;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00004557 File Offset: 0x00002757
		public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
		{
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00029F27 File Offset: 0x00028127
		[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
		public void Clear(bool revertValues)
		{
			this.Clear();
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00004557 File Offset: 0x00002757
		public void Clear()
		{
		}
	}
}
