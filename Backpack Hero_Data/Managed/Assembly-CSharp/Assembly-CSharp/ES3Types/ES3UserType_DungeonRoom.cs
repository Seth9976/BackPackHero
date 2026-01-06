using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001C5 RID: 453
	[Preserve]
	[ES3Properties(new string[]
	{
		"leftBlocked", "rightBlocked", "upBlocked", "downBlocked", "left", "right", "up", "down", "cardPrefab", "dungeonEvent",
		"enabled", "name"
	})]
	public class ES3UserType_DungeonRoom : ES3ComponentType
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x000A2DD5 File Offset: 0x000A0FD5
		public ES3UserType_DungeonRoom()
			: base(typeof(DungeonRoom))
		{
			ES3UserType_DungeonRoom.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000A2DF4 File Offset: 0x000A0FF4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DungeonRoom dungeonRoom = (DungeonRoom)obj;
			writer.WriteProperty("leftBlocked", dungeonRoom.leftBlocked, ES3Type_bool.Instance);
			writer.WriteProperty("rightBlocked", dungeonRoom.rightBlocked, ES3Type_bool.Instance);
			writer.WriteProperty("upBlocked", dungeonRoom.upBlocked, ES3Type_bool.Instance);
			writer.WriteProperty("downBlocked", dungeonRoom.downBlocked, ES3Type_bool.Instance);
			writer.WriteProperty("left", dungeonRoom.left, ES3Type_bool.Instance);
			writer.WriteProperty("right", dungeonRoom.right, ES3Type_bool.Instance);
			writer.WriteProperty("up", dungeonRoom.up, ES3Type_bool.Instance);
			writer.WriteProperty("down", dungeonRoom.down, ES3Type_bool.Instance);
			writer.WritePrivateFieldByRef("cardPrefab", dungeonRoom);
			writer.WritePrivateFieldByRef("dungeonEvent", dungeonRoom);
			writer.WriteProperty("enabled", dungeonRoom.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000A2F14 File Offset: 0x000A1114
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DungeonRoom dungeonRoom = (DungeonRoom)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 699108745U)
				{
					if (num <= 237657528U)
					{
						if (num != 49525662U)
						{
							if (num == 237657528U)
							{
								if (text == "leftBlocked")
								{
									dungeonRoom.leftBlocked = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							dungeonRoom.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 306900080U)
					{
						if (num != 530316616U)
						{
							if (num == 699108745U)
							{
								if (text == "cardPrefab")
								{
									reader.SetPrivateField("cardPrefab", reader.Read<GameObject>(), dungeonRoom);
									continue;
								}
							}
						}
						else if (text == "upBlocked")
						{
							dungeonRoom.upBlocked = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (text == "left")
					{
						dungeonRoom.left = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 1128467232U)
				{
					if (num != 823484739U)
					{
						if (num != 1035581717U)
						{
							if (num == 1128467232U)
							{
								if (text == "up")
								{
									dungeonRoom.up = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "down")
						{
							dungeonRoom.down = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (text == "downBlocked")
					{
						dungeonRoom.downBlocked = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 1976344243U)
				{
					if (num != 2028154341U)
					{
						if (num == 2411244807U)
						{
							if (text == "dungeonEvent")
							{
								reader.SetPrivateField("dungeonEvent", reader.Read<DungeonEvent>(), dungeonRoom);
								continue;
							}
						}
					}
					else if (text == "right")
					{
						dungeonRoom.right = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (text == "rightBlocked")
				{
					dungeonRoom.rightBlocked = reader.Read<bool>(ES3Type_bool.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000DF1 RID: 3569
		public static ES3Type Instance;
	}
}
