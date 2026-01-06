using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000113 RID: 275
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Round")]
	public sealed class Vector3Round : Round<Vector3, Vector3>
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0000DE1F File Offset: 0x0000C01F
		protected override Vector3 Floor(Vector3 input)
		{
			return new Vector3(Mathf.Floor(input.x), Mathf.Floor(input.y), Mathf.Floor(input.z));
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000DE47 File Offset: 0x0000C047
		protected override Vector3 AwayFromZero(Vector3 input)
		{
			return new Vector3(Mathf.Round(input.x), Mathf.Round(input.y), Mathf.Round(input.z));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000DE6F File Offset: 0x0000C06F
		protected override Vector3 Ceiling(Vector3 input)
		{
			return new Vector3(Mathf.Ceil(input.x), Mathf.Ceil(input.y), Mathf.Ceil(input.z));
		}
	}
}
