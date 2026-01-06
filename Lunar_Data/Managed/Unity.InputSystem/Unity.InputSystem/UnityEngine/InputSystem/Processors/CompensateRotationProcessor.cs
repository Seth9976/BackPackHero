using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FB RID: 251
	[DesignTimeVisible(false)]
	internal class CompensateRotationProcessor : InputProcessor<Quaternion>
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x00047574 File Offset: 0x00045774
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

		// Token: 0x06000E98 RID: 3736 RVA: 0x00047612 File Offset: 0x00045812
		public override string ToString()
		{
			return "CompensateRotation()";
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00047619 File Offset: 0x00045819
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
