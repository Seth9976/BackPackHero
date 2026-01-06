using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000008 RID: 8
	public sealed class FieldsCloner : ReflectedCloner
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023CC File Offset: 0x000005CC
		protected override bool IncludeField(FieldInfo field)
		{
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023CF File Offset: 0x000005CF
		protected override bool IncludeProperty(PropertyInfo property)
		{
			return false;
		}
	}
}
