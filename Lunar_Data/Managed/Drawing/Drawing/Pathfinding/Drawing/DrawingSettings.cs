using System;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000047 RID: 71
	public class DrawingSettings : ScriptableObject
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000B397 File Offset: 0x00009597
		public static DrawingSettings.Settings DefaultSettings
		{
			get
			{
				return new DrawingSettings.Settings();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B39E File Offset: 0x0000959E
		public static DrawingSettings GetSettingsAsset()
		{
			return Resources.Load<DrawingSettings>("AstarGizmos");
		}

		// Token: 0x04000117 RID: 279
		public const string SettingsPathCompatibility = "Assets/Settings/ALINE.asset";

		// Token: 0x04000118 RID: 280
		public const string SettingsName = "AstarGizmos";

		// Token: 0x04000119 RID: 281
		public const string SettingsPath = "Assets/Settings/Resources/AstarGizmos.asset";

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private int version;

		// Token: 0x0400011B RID: 283
		public DrawingSettings.Settings settings;

		// Token: 0x02000048 RID: 72
		[Serializable]
		public class Settings
		{
			// Token: 0x0400011C RID: 284
			public float lineOpacity = 1f;

			// Token: 0x0400011D RID: 285
			public float solidOpacity = 0.55f;

			// Token: 0x0400011E RID: 286
			public float textOpacity = 1f;

			// Token: 0x0400011F RID: 287
			public float lineOpacityBehindObjects = 0.12f;

			// Token: 0x04000120 RID: 288
			public float solidOpacityBehindObjects = 0.45f;

			// Token: 0x04000121 RID: 289
			public float textOpacityBehindObjects = 0.9f;

			// Token: 0x04000122 RID: 290
			public float curveResolution = 1f;
		}
	}
}
