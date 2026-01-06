using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class XRDeviceDescriptor
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00035F0E File Offset: 0x0003410E
		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00035F16 File Offset: 0x00034116
		public static XRDeviceDescriptor FromJson(string json)
		{
			return JsonUtility.FromJson<XRDeviceDescriptor>(json);
		}

		// Token: 0x0400033E RID: 830
		public string deviceName;

		// Token: 0x0400033F RID: 831
		public string manufacturer;

		// Token: 0x04000340 RID: 832
		public string serialNumber;

		// Token: 0x04000341 RID: 833
		public InputDeviceCharacteristics characteristics;

		// Token: 0x04000342 RID: 834
		public int deviceId;

		// Token: 0x04000343 RID: 835
		public List<XRFeatureDescriptor> inputFeatures;
	}
}
