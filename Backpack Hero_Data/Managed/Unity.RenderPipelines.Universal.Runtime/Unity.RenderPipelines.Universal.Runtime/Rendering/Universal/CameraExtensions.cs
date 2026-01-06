using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CF RID: 207
	public static class CameraExtensions
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0002093C File Offset: 0x0001EB3C
		public static UniversalAdditionalCameraData GetUniversalAdditionalCameraData(this Camera camera)
		{
			GameObject gameObject = camera.gameObject;
			UniversalAdditionalCameraData universalAdditionalCameraData;
			if (!gameObject.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData))
			{
				universalAdditionalCameraData = gameObject.AddComponent<UniversalAdditionalCameraData>();
			}
			return universalAdditionalCameraData;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00020962 File Offset: 0x0001EB62
		public static VolumeFrameworkUpdateMode GetVolumeFrameworkUpdateMode(this Camera camera)
		{
			return camera.GetUniversalAdditionalCameraData().volumeFrameworkUpdateMode;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00020970 File Offset: 0x0001EB70
		public static void SetVolumeFrameworkUpdateMode(this Camera camera, VolumeFrameworkUpdateMode mode)
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			if (universalAdditionalCameraData.volumeFrameworkUpdateMode == mode)
			{
				return;
			}
			bool requiresVolumeFrameworkUpdate = universalAdditionalCameraData.requiresVolumeFrameworkUpdate;
			universalAdditionalCameraData.volumeFrameworkUpdateMode = mode;
			if (requiresVolumeFrameworkUpdate && !universalAdditionalCameraData.requiresVolumeFrameworkUpdate)
			{
				camera.UpdateVolumeStack(universalAdditionalCameraData);
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000209AC File Offset: 0x0001EBAC
		public static void UpdateVolumeStack(this Camera camera)
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			camera.UpdateVolumeStack(universalAdditionalCameraData);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000209C8 File Offset: 0x0001EBC8
		public static void UpdateVolumeStack(this Camera camera, UniversalAdditionalCameraData cameraData)
		{
			if (cameraData.requiresVolumeFrameworkUpdate)
			{
				return;
			}
			if (cameraData.volumeStack == null)
			{
				cameraData.volumeStack = VolumeManager.instance.CreateStack();
			}
			LayerMask layerMask;
			Transform transform;
			camera.GetVolumeLayerMaskAndTrigger(cameraData, out layerMask, out transform);
			VolumeManager.instance.Update(cameraData.volumeStack, transform, layerMask);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00020A14 File Offset: 0x0001EC14
		public static void DestroyVolumeStack(this Camera camera)
		{
			UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			camera.DestroyVolumeStack(universalAdditionalCameraData);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00020A2F File Offset: 0x0001EC2F
		public static void DestroyVolumeStack(this Camera camera, UniversalAdditionalCameraData cameraData)
		{
			cameraData.volumeStack.Dispose();
			cameraData.volumeStack = null;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00020A44 File Offset: 0x0001EC44
		internal static void GetVolumeLayerMaskAndTrigger(this Camera camera, UniversalAdditionalCameraData cameraData, out LayerMask layerMask, out Transform trigger)
		{
			layerMask = 1;
			trigger = camera.transform;
			if (cameraData != null)
			{
				layerMask = cameraData.volumeLayerMask;
				trigger = ((cameraData.volumeTrigger != null) ? cameraData.volumeTrigger : trigger);
				return;
			}
			if (camera.cameraType == CameraType.SceneView)
			{
				Camera main = Camera.main;
				UniversalAdditionalCameraData universalAdditionalCameraData = null;
				if (main != null && main.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData))
				{
					layerMask = universalAdditionalCameraData.volumeLayerMask;
				}
				trigger = ((universalAdditionalCameraData != null && universalAdditionalCameraData.volumeTrigger != null) ? universalAdditionalCameraData.volumeTrigger : trigger);
			}
		}
	}
}
