using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000060 RID: 96
	[StructLayout(LayoutKind.Explicit, Size = 60)]
	public struct PoseState : IInputStateTypeInfo
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00034988 File Offset: 0x00032B88
		public FourCC format
		{
			get
			{
				return PoseState.s_Format;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0003498F File Offset: 0x00032B8F
		public PoseState(bool isTracked, InputTrackingState trackingState, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
		{
			this.isTracked = isTracked;
			this.trackingState = trackingState;
			this.position = position;
			this.rotation = rotation;
			this.velocity = velocity;
			this.angularVelocity = angularVelocity;
		}

		// Token: 0x04000302 RID: 770
		internal const int kSizeInBytes = 60;

		// Token: 0x04000303 RID: 771
		internal static readonly FourCC s_Format = new FourCC('P', 'o', 's', 'e');

		// Token: 0x04000304 RID: 772
		[InputControl(displayName = "Is Tracked", layout = "Button", sizeInBits = 8U)]
		[FieldOffset(0)]
		public bool isTracked;

		// Token: 0x04000305 RID: 773
		[InputControl(displayName = "Tracking State", layout = "Integer")]
		[FieldOffset(4)]
		public InputTrackingState trackingState;

		// Token: 0x04000306 RID: 774
		[InputControl(displayName = "Position", noisy = true)]
		[FieldOffset(8)]
		public Vector3 position;

		// Token: 0x04000307 RID: 775
		[InputControl(displayName = "Rotation", noisy = true)]
		[FieldOffset(20)]
		public Quaternion rotation;

		// Token: 0x04000308 RID: 776
		[InputControl(displayName = "Velocity", noisy = true)]
		[FieldOffset(36)]
		public Vector3 velocity;

		// Token: 0x04000309 RID: 777
		[InputControl(displayName = "Angular Velocity", noisy = true)]
		[FieldOffset(48)]
		public Vector3 angularVelocity;
	}
}
