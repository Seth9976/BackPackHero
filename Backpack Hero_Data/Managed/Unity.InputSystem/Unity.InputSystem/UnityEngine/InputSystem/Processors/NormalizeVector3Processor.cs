using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000101 RID: 257
	public class NormalizeVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00047877 File Offset: 0x00045A77
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00047880 File Offset: 0x00045A80
		public override string ToString()
		{
			return "NormalizeVector3()";
		}
	}
}
