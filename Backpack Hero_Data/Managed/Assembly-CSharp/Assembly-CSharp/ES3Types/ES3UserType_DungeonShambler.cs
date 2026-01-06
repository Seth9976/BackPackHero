using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001C9 RID: 457
	[Preserve]
	[ES3Properties(new string[] { "awakeSprite", "asleep", "effectPrefab" })]
	public class ES3UserType_DungeonShambler : ES3ComponentType
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x000A331D File Offset: 0x000A151D
		public ES3UserType_DungeonShambler()
			: base(typeof(DungeonShambler))
		{
			ES3UserType_DungeonShambler.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000A333C File Offset: 0x000A153C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DungeonShambler dungeonShambler = (DungeonShambler)obj;
			writer.WritePrivateFieldByRef("awakeSprite", dungeonShambler);
			writer.WriteProperty("asleep", dungeonShambler.asleep, ES3Type_bool.Instance);
			writer.WritePrivateFieldByRef("effectPrefab", dungeonShambler);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000A3384 File Offset: 0x000A1584
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DungeonShambler dungeonShambler = (DungeonShambler)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "awakeSprite"))
				{
					if (!(text == "asleep"))
					{
						if (!(text == "effectPrefab"))
						{
							reader.Skip();
						}
						else
						{
							reader.SetPrivateField("effectPrefab", reader.Read<GameObject>(), dungeonShambler);
						}
					}
					else
					{
						dungeonShambler.asleep = reader.Read<bool>(ES3Type_bool.Instance);
					}
				}
				else
				{
					reader.SetPrivateField("awakeSprite", reader.Read<Sprite>(), dungeonShambler);
				}
			}
		}

		// Token: 0x04000DF5 RID: 3573
		public static ES3Type Instance;
	}
}
