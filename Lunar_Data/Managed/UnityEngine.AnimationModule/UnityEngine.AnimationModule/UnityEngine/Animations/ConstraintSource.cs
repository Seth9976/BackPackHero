using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000063 RID: 99
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Modules/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
	[UsedByNativeCode]
	[Serializable]
	public struct ConstraintSource
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00007B84 File Offset: 0x00005D84
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00007B9C File Offset: 0x00005D9C
		public Transform sourceTransform
		{
			get
			{
				return this.m_SourceTransform;
			}
			set
			{
				this.m_SourceTransform = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00007BA8 File Offset: 0x00005DA8
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x04000180 RID: 384
		[NativeName("sourceTransform")]
		private Transform m_SourceTransform;

		// Token: 0x04000181 RID: 385
		[NativeName("weight")]
		private float m_Weight;
	}
}
