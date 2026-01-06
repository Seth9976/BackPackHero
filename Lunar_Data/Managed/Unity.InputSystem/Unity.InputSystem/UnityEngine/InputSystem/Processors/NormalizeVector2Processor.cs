using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000100 RID: 256
	public class NormalizeVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00047813 File Offset: 0x00045A13
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0004781C File Offset: 0x00045A1C
		public override string ToString()
		{
			return "NormalizeVector2()";
		}
	}
}
