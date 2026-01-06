using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SaveSystem.States
{
	// Token: 0x020000B2 RID: 178
	public class OptionsState : State
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00016EC2 File Offset: 0x000150C2
		public new object Migrate()
		{
			return null;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00016EC8 File Offset: 0x000150C8
		public static OptionsState Capture()
		{
			return new OptionsState
			{
				musicVolume = SoundManager.instance.musicVolume,
				sfxVolume = SoundManager.instance.sfxVolume,
				screenMode = Screen.fullScreenMode,
				screenResolution = Screen.currentResolution,
				stretchToFill = Singleton.instance.stretchToFill,
				controllerSpriteSet = ControllerSpriteManager.instance.currentSpriteSet,
				controllerAutoDetect = ControllerSpriteManager.instance.autoDetect,
				chosenLanguage = LanguageManager.main.discoveredLanguages[LanguageManager.main.chosenLanguageNum]
			};
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00016F60 File Offset: 0x00015160
		public new void Restore()
		{
			SoundManager.instance.musicVolume = this.musicVolume;
			SoundManager.instance.sfxVolume = this.sfxVolume;
			Screen.SetResolution(this.screenResolution.width, this.screenResolution.height, this.screenMode);
			Screen.fullScreenMode = this.screenMode;
			Singleton.instance.stretchToFill = this.stretchToFill;
			ControllerSpriteManager.instance.autoDetect = this.controllerAutoDetect;
			if (this.controllerAutoDetect)
			{
				ControllerSpriteManager.instance.AutoDetect(Gamepad.current);
			}
			else
			{
				ControllerSpriteManager.instance.SwitchSpriteSet(this.controllerSpriteSet);
			}
			LanguageManager.main.ChooseLanguageViaString(this.chosenLanguage);
			PlatformConfigurator.instance.ApplyConfigs();
		}

		// Token: 0x0400039B RID: 923
		public static readonly Dictionary<int, Type> versionClasses = new Dictionary<int, Type> { 
		{
			1,
			typeof(OptionsState)
		} };

		// Token: 0x0400039C RID: 924
		public new StateType type;

		// Token: 0x0400039D RID: 925
		public new int version = 1;

		// Token: 0x0400039E RID: 926
		public float musicVolume = 0.2f;

		// Token: 0x0400039F RID: 927
		public float sfxVolume = 0.4f;

		// Token: 0x040003A0 RID: 928
		public Resolution screenResolution = new Resolution
		{
			width = 1366,
			height = 768,
			refreshRate = 60
		};

		// Token: 0x040003A1 RID: 929
		public FullScreenMode screenMode = FullScreenMode.Windowed;

		// Token: 0x040003A2 RID: 930
		public bool stretchToFill = true;

		// Token: 0x040003A3 RID: 931
		[JsonConverter(typeof(StringEnumConverter))]
		public ControllerSpriteManager.ControllerSpriteType controllerSpriteSet;

		// Token: 0x040003A4 RID: 932
		public bool controllerAutoDetect = true;

		// Token: 0x040003A5 RID: 933
		public string chosenLanguage = "English";
	}
}
