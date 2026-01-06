using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FB RID: 251
	[DesignTimeVisible(false)]
	internal class CompensateRotationProcessor : InputProcessor<Quaternion>
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x000475C0 File Offset: 0x000457C0
		public override Quaternion Process(Quaternion value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion identity = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				identity = new Quaternion(0f, 0f, 1f, 0f);
				break;
			case ScreenOrientation.Landscape:
				identity = new Quaternion(0f, 0f, 0.70710677f, -0.70710677f);
				break;
			case ScreenOrientation.LandscapeRight:
				identity = new Quaternion(0f, 0f, -0.70710677f, -0.70710677f);
				break;
			}
			return value * identity;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0004765E File Offset: 0x0004585E
		public override string ToString()
		{
			return "CompensateRotation()";
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00047665 File Offset: 0x00045865
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
