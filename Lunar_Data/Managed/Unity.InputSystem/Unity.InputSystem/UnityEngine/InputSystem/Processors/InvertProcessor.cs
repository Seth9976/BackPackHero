using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FC RID: 252
	public class InvertProcessor : InputProcessor<float>
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x00047624 File Offset: 0x00045824
		public override float Process(float value, InputControl control)
		{
			return value * -1f;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0004762D File Offset: 0x0004582D
		public override string ToString()
		{
			return "Invert()";
		}
	}
}
