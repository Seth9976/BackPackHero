using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CC RID: 204
	[UnitCategory("Math/Generic")]
	[UnitTitle("Add")]
	[RenamedFrom("Bolt.GenericAdd")]
	[RenamedFrom("Unity.VisualScripting.GenericAdd")]
	[Obsolete("Use the new \"Add (Math/Generic)\" node instead.")]
	public sealed class DeprecatedGenericAdd : Add<object>
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0000C711 File Offset: 0x0000A911
		public override object Operation(object a, object b)
		{
			return OperatorUtility.Add(a, b);
		}
	}
}
