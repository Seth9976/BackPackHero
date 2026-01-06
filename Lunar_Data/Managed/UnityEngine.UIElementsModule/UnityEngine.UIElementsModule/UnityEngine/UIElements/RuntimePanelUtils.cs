using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024A RID: 586
	public static class RuntimePanelUtils
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x000438C4 File Offset: 0x00041AC4
		public static Vector2 ScreenToPanel(IPanel panel, Vector2 screenPosition)
		{
			return ((BaseRuntimePanel)panel).ScreenToPanel(screenPosition);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000438E4 File Offset: 0x00041AE4
		public static Vector2 CameraTransformWorldToPanel(IPanel panel, Vector3 worldPosition, Camera camera)
		{
			Vector2 vector = camera.WorldToScreenPoint(worldPosition);
			vector.y = (float)Screen.height - vector.y;
			return ((BaseRuntimePanel)panel).ScreenToPanel(vector);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00043924 File Offset: 0x00041B24
		public static Rect CameraTransformWorldToPanelRect(IPanel panel, Vector3 worldPosition, Vector2 worldSize, Camera camera)
		{
			worldSize.y = -worldSize.y;
			Vector2 vector = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera);
			Vector3 vector2 = worldPosition + camera.worldToCameraMatrix.MultiplyVector(worldSize);
			Vector2 vector3 = RuntimePanelUtils.CameraTransformWorldToPanel(panel, vector2, camera);
			return new Rect(vector, vector3 - vector);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00043980 File Offset: 0x00041B80
		public static void ResetDynamicAtlas(this IPanel panel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.Reset();
				}
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000439B8 File Offset: 0x00041BB8
		public static void SetTextureDirty(this IPanel panel, Texture2D texture)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.SetDirty(texture);
				}
			}
		}
	}
}
