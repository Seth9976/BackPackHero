using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000033 RID: 51
	[AddComponentMenu("Rendering/2D/Composite Shadow Caster 2D")]
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	[ExecuteInEditMode]
	public class CompositeShadowCaster2D : ShadowCasterGroup2D
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00010A91 File Offset: 0x0000EC91
		protected void OnEnable()
		{
			ShadowCasterGroup2DManager.AddGroup(this);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010A99 File Offset: 0x0000EC99
		protected void OnDisable()
		{
			ShadowCasterGroup2DManager.RemoveGroup(this);
		}
	}
}
