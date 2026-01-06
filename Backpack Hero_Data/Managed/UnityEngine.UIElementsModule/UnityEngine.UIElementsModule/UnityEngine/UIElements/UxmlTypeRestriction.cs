using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E9 RID: 745
	public abstract class UxmlTypeRestriction : IEquatable<UxmlTypeRestriction>
	{
		// Token: 0x06001883 RID: 6275 RVA: 0x00061C24 File Offset: 0x0005FE24
		public virtual bool Equals(UxmlTypeRestriction other)
		{
			return this == other;
		}
	}
}
