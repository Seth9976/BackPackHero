using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h")]
	[StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public static class InputTracking
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x00002090 File Offset: 0x00000290
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> trackingAcquired;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> trackingLost;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> nodeAdded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002194 File Offset: 0x00000394
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> nodeRemoved;

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(InputTracking.TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			XRNodeState xrnodeState = default(XRNodeState);
			xrnodeState.uniqueID = (ulong)uniqueID;
			xrnodeState.nodeType = nodeType;
			xrnodeState.tracked = tracked;
			Action<XRNodeState> action;
			switch (eventType)
			{
			case InputTracking.TrackingStateEventType.NodeAdded:
				action = InputTracking.nodeAdded;
				break;
			case InputTracking.TrackingStateEventType.NodeRemoved:
				action = InputTracking.nodeRemoved;
				break;
			case InputTracking.TrackingStateEventType.TrackingAcquired:
				action = InputTracking.trackingAcquired;
				break;
			case InputTracking.TrackingStateEventType.TrackingLost:
				action = InputTracking.trackingLost;
				break;
			default:
				throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType.ToString());
			}
			bool flag = action != null;
			if (flag)
			{
				action.Invoke(xrnodeState);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229C File Offset: 0x0000049C
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.devicePosition usage instead.")]
		[NativeConditional("ENABLE_VR", "Vector3f::zero")]
		public static Vector3 GetLocalPosition(XRNode node)
		{
			Vector3 vector;
			InputTracking.GetLocalPosition_Injected(node, out vector);
			return vector;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B4 File Offset: 0x000004B4
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.deviceRotation usage instead.")]
		[NativeConditional("ENABLE_VR", "Quaternionf::identity()")]
		public static Quaternion GetLocalRotation(XRNode node)
		{
			Quaternion quaternion;
			InputTracking.GetLocalRotation_Injected(node, out quaternion);
			return quaternion;
		}

		// Token: 0x0600000E RID: 14
		[Obsolete("This API is obsolete, and should no longer be used. Please use XRInputSubsystem.TryRecenter() instead.")]
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(4096)]
		public static extern void Recenter();

		// Token: 0x0600000F RID: 15
		[NativeConditional("ENABLE_VR")]
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.name with the device associated with that tracking data instead.")]
		[MethodImpl(4096)]
		public static extern string GetNodeName(ulong uniqueId);

		// Token: 0x06000010 RID: 16 RVA: 0x000022CC File Offset: 0x000004CC
		public static void GetNodeStates(List<XRNodeState> nodeStates)
		{
			bool flag = nodeStates == null;
			if (flag)
			{
				throw new ArgumentNullException("nodeStates");
			}
			nodeStates.Clear();
			InputTracking.GetNodeStates_Internal(nodeStates);
		}

		// Token: 0x06000011 RID: 17
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(4096)]
		private static extern void GetNodeStates_Internal([NotNull("ArgumentNullException")] List<XRNodeState> nodeStates);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		[Obsolete("This API is obsolete, and should no longer be used. Please use the TrackedPoseDriver in the Legacy Input Helpers package for controlling a camera in XR.")]
		[NativeConditional("ENABLE_VR")]
		public static extern bool disablePositionalTracking
		{
			[NativeName("GetPositionalTrackingDisabled")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetPositionalTrackingDisabled")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000014 RID: 20
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[MethodImpl(4096)]
		internal static extern ulong GetDeviceIdAtXRNode(XRNode node);

		// Token: 0x06000015 RID: 21
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		internal static extern void GetDeviceIdsAtXRNode_Internal(XRNode node, [NotNull("ArgumentNullException")] List<ulong> deviceIds);

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern void GetLocalPosition_Injected(XRNode node, out Vector3 ret);

		// Token: 0x06000017 RID: 23
		[MethodImpl(4096)]
		private static extern void GetLocalRotation_Injected(XRNode node, out Quaternion ret);

		// Token: 0x02000005 RID: 5
		private enum TrackingStateEventType
		{
			// Token: 0x04000006 RID: 6
			NodeAdded,
			// Token: 0x04000007 RID: 7
			NodeRemoved,
			// Token: 0x04000008 RID: 8
			TrackingAcquired,
			// Token: 0x04000009 RID: 9
			TrackingLost
		}
	}
}
