using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class PlatformConfigurator : MonoBehaviour
{
	// Token: 0x060002FF RID: 767 RVA: 0x0000F4DA File Offset: 0x0000D6DA
	private void OnEnable()
	{
		if (PlatformConfigurator.instance == null)
		{
			PlatformConfigurator.instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0000F506 File Offset: 0x0000D706
	private void OnDisable()
	{
		if (PlatformConfigurator.instance == this)
		{
			PlatformConfigurator.instance = null;
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0000F51C File Offset: 0x0000D71C
	public void ApplyConfigs()
	{
		PlatformConfigurator.Config config;
		if (this.configs.TryGetValue(PlatformWrapper.GetCurrentPlatform(), out config))
		{
			if (config.setResolution)
			{
				Screen.SetResolution(config.resolution.x, config.resolution.y, config.screenMode);
			}
			if (config.setStretch)
			{
				Singleton.instance.stretchToFill = config.stretch;
			}
			if (config.setControllerSprites)
			{
				ControllerSpriteManager.instance.autoDetect = false;
				ControllerSpriteManager.instance.SwitchSpriteSet(config.controllerSprites);
			}
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0000F5A1 File Offset: 0x0000D7A1
	private void Awake()
	{
		this.ApplyConfigs();
	}

	// Token: 0x0400024D RID: 589
	public static PlatformConfigurator instance;

	// Token: 0x0400024E RID: 590
	[SerializeField]
	private UDictionary<PlatformWrapper.Platform, PlatformConfigurator.Config> configs = new UDictionary<PlatformWrapper.Platform, PlatformConfigurator.Config>();

	// Token: 0x020000F5 RID: 245
	[Serializable]
	private class Config
	{
		// Token: 0x04000486 RID: 1158
		public bool setResolution;

		// Token: 0x04000487 RID: 1159
		public Vector2Int resolution;

		// Token: 0x04000488 RID: 1160
		public FullScreenMode screenMode;

		// Token: 0x04000489 RID: 1161
		public bool setStretch;

		// Token: 0x0400048A RID: 1162
		public bool stretch;

		// Token: 0x0400048B RID: 1163
		public bool setControllerSprites;

		// Token: 0x0400048C RID: 1164
		public ControllerSpriteManager.ControllerSpriteType controllerSprites;
	}
}
