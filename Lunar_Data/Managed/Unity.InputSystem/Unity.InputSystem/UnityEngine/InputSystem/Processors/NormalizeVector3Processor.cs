using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000101 RID: 257
	public class NormalizeVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0004782B File Offset: 0x00045A2B
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00047834 File Offset: 0x00045A34
		public override string ToString()
		{
			return "NormalizeVector3()";
		}
	}
}
