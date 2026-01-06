using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EE RID: 494
	[AttributeUsage(4, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x00023B8E File Offset: 0x00021D8E
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00023B9F File Offset: 0x00021D9F
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00023BB7 File Offset: 0x00021DB7
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x040007C8 RID: 1992
		public Type m_Type0;

		// Token: 0x040007C9 RID: 1993
		public Type m_Type1;

		// Token: 0x040007CA RID: 1994
		public Type m_Type2;
	}
}
