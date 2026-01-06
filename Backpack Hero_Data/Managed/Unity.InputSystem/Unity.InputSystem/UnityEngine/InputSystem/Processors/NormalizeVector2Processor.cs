using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000100 RID: 256
	public class NormalizeVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000EAE RID: 3758 RVA: 0x0004785F File Offset: 0x00045A5F
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00047868 File Offset: 0x00045A68
		public override string ToString()
		{
			return "NormalizeVector2()";
		}
	}
}
