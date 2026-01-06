using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A4 RID: 164
	public static class CameraCaptureBridge
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00019800 File Offset: 0x00017A00
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00019807 File Offset: 0x00017A07
		public static bool enabled
		{
			get
			{
				return CameraCaptureBridge._enabled;
			}
			set
			{
				CameraCaptureBridge._enabled = value;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00019810 File Offset: 0x00017A10
		public static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCaptureActions(Camera camera)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (!CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet) || hashSet.Count == 0)
			{
				return null;
			}
			return hashSet.GetEnumerator();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00019844 File Offset: 0x00017A44
		public static void AddCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet);
			if (hashSet == null)
			{
				hashSet = new HashSet<Action<RenderTargetIdentifier, CommandBuffer>>();
				CameraCaptureBridge.actionDict.Add(camera, hashSet);
			}
			hashSet.Add(action);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001987C File Offset: 0x00017A7C
		public static void RemoveCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			if (camera == null)
			{
				return;
			}
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet))
			{
				hashSet.Remove(action);
			}
		}

		// Token: 0x04000350 RID: 848
		private static Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>> actionDict = new Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>>();

		// Token: 0x04000351 RID: 849
		private static bool _enabled;
	}
}
