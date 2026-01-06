using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FA RID: 250
	[DesignTimeVisible(false)]
	internal class CompensateDirectionProcessor : InputProcessor<Vector3>
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x000474D4 File Offset: 0x000456D4
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion quaternion = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				quaternion = Quaternion.Euler(0f, 0f, 180f);
				break;
			case ScreenOrientation.Landscape:
				quaternion = Quaternion.Euler(0f, 0f, 90f);
				break;
			case ScreenOrientation.LandscapeRight:
				quaternion = Quaternion.Euler(0f, 0f, 270f);
				break;
			}
			return quaternion * value;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00047560 File Offset: 0x00045760
		public override string ToString()
		{
			return "CompensateDirection()";
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00047567 File Offset: 0x00045767
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
