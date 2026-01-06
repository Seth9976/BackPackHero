using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001FD RID: 509
	[Preserve]
	[ES3Properties(new string[] { "gamesSavedandLoaded", "stats" })]
	public class ES3UserType_PlayerStatTracking : ES3ComponentType
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x000A7809 File Offset: 0x000A5A09
		public ES3UserType_PlayerStatTracking()
			: base(typeof(PlayerStatTracking))
		{
			ES3UserType_PlayerStatTracking.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000A7828 File Offset: 0x000A5A28
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PlayerStatTracking playerStatTracking = (PlayerStatTracking)obj;
			writer.WriteProperty("gamesSavedandLoaded", playerStatTracking.gamesSavedandLoaded, ES3TypeMgr.GetOrCreateES3Type(typeof(List<string>), true));
			writer.WriteProperty("stats", playerStatTracking.stats, ES3TypeMgr.GetOrCreateES3Type(typeof(List<PlayerStatTracking.Stat>), true));
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000A7880 File Offset: 0x000A5A80
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PlayerStatTracking playerStatTracking = (PlayerStatTracking)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "gamesSavedandLoaded"))
				{
					if (!(text == "stats"))
					{
						reader.Skip();
					}
					else
					{
						playerStatTracking.stats = reader.Read<List<PlayerStatTracking.Stat>>();
					}
				}
				else
				{
					playerStatTracking.gamesSavedandLoaded = reader.Read<List<string>>();
				}
			}
		}

		// Token: 0x04000E29 RID: 3625
		public static ES3Type Instance;
	}
}
