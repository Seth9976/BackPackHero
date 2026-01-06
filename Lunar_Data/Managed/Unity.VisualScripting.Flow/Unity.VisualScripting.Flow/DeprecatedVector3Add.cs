using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000102 RID: 258
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Add")]
	[Obsolete("Use the new \"Add (Math/Vector 3)\" instead.")]
	[RenamedFrom("Bolt.Vector3Add")]
	[RenamedFrom("Unity.VisualScripting.Vector3Add")]
	public sealed class DeprecatedVector3Add : Add<Vector3>
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000DB00 File Offset: 0x0000BD00
		protected override Vector3 defaultB
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000DB07 File Offset: 0x0000BD07
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return a + b;
		}
	}
}
