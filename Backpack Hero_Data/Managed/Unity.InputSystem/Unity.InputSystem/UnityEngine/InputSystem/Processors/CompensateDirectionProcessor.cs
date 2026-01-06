using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020000FA RID: 250
	[DesignTimeVisible(false)]
	internal class CompensateDirectionProcessor : InputProcessor<Vector3>
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x00047520 File Offset: 0x00045720
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

		// Token: 0x06000E99 RID: 3737 RVA: 0x000475AC File Offset: 0x000457AC
		public override string ToString()
		{
			return "CompensateDirection()";
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000475B3 File Offset: 0x000457B3
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
