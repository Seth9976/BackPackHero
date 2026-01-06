using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FC RID: 252
	public class InvertProcessor : InputProcessor<float>
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00047670 File Offset: 0x00045870
		public override float Process(float value, InputControl control)
		{
			return value * -1f;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00047679 File Offset: 0x00045879
		public override string ToString()
		{
			return "Invert()";
		}
	}
}
