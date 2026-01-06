using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001C3 RID: 451
	[Preserve]
	[ES3Properties(new string[] { "room", "cardPrefab", "footStepPrefab", "dungeonEvent", "lastSpace" })]
	public class ES3UserType_DungeonPlayer : ES3ComponentType
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x000A2C21 File Offset: 0x000A0E21
		public ES3UserType_DungeonPlayer()
			: base(typeof(DungeonPlayer))
		{
			ES3UserType_DungeonPlayer.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000A2C40 File Offset: 0x000A0E40
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DungeonPlayer dungeonPlayer = (DungeonPlayer)obj;
			writer.WritePropertyByRef("room", dungeonPlayer.room);
			writer.WritePrivateFieldByRef("cardPrefab", dungeonPlayer);
			writer.WritePropertyByRef("footStepPrefab", dungeonPlayer.footStepPrefab);
			writer.WritePropertyByRef("dungeonEvent", dungeonPlayer.dungeonEvent);
			writer.WriteProperty("lastSpace", dungeonPlayer.lastSpace, ES3Type_Vector2.Instance);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000A2CB0 File Offset: 0x000A0EB0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DungeonPlayer dungeonPlayer = (DungeonPlayer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "room"))
				{
					if (!(text == "cardPrefab"))
					{
						if (!(text == "footStepPrefab"))
						{
							if (!(text == "dungeonEvent"))
							{
								if (!(text == "lastSpace"))
								{
									reader.Skip();
								}
								else
								{
									dungeonPlayer.lastSpace = reader.Read<Vector2>(ES3Type_Vector2.Instance);
								}
							}
							else
							{
								dungeonPlayer.dungeonEvent = reader.Read<DungeonEvent>(ES3UserType_DungeonEvent.Instance);
							}
						}
						else
						{
							dungeonPlayer.footStepPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
						}
					}
					else
					{
						reader.SetPrivateField("cardPrefab", reader.Read<GameObject>(), dungeonPlayer);
					}
				}
				else
				{
					dungeonPlayer.room = reader.Read<DungeonRoom>(ES3UserType_DungeonRoom.Instance);
				}
			}
		}

		// Token: 0x04000DEF RID: 3567
		public static ES3Type Instance;
	}
}
