using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001C7 RID: 455
	[Preserve]
	[ES3Properties(new string[] { "getRoomShape", "dungeonRoom" })]
	public class ES3UserType_DungeonRoomSelector : ES3ComponentType
	{
		// Token: 0x06001155 RID: 4437 RVA: 0x000A320D File Offset: 0x000A140D
		public ES3UserType_DungeonRoomSelector()
			: base(typeof(DungeonRoomSelector))
		{
			ES3UserType_DungeonRoomSelector.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000A322C File Offset: 0x000A142C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DungeonRoomSelector dungeonRoomSelector = (DungeonRoomSelector)obj;
			writer.WritePrivateField("getRoomShape", dungeonRoomSelector);
			writer.WritePrivateFieldByRef("dungeonRoom", dungeonRoomSelector);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000A3258 File Offset: 0x000A1458
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DungeonRoomSelector dungeonRoomSelector = (DungeonRoomSelector)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "getRoomShape"))
				{
					if (!(text == "dungeonRoom"))
					{
						reader.Skip();
					}
					else
					{
						reader.SetPrivateField("dungeonRoom", reader.Read<DungeonRoom>(), dungeonRoomSelector);
					}
				}
				else
				{
					reader.SetPrivateField("getRoomShape", reader.Read<bool>(), dungeonRoomSelector);
				}
			}
		}

		// Token: 0x04000DF3 RID: 3571
		public static ES3Type Instance;
	}
}
