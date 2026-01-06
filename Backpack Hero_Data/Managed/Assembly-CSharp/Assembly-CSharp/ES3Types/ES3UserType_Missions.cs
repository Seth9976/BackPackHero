using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001DF RID: 479
	[Preserve]
	[ES3Properties(new string[] { "dungeonLevels", "runTypeLanguageKey", "runTypeNumber", "validForCharacter", "rewards", "runProperties" })]
	public class ES3UserType_Missions : ES3ScriptableObjectType
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x000A5759 File Offset: 0x000A3959
		public ES3UserType_Missions()
			: base(typeof(Missions))
		{
			ES3UserType_Missions.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000A5778 File Offset: 0x000A3978
		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			Missions missions = (Missions)obj;
			writer.WriteProperty("dungeonLevels", missions.dungeonLevels, ES3TypeMgr.GetOrCreateES3Type(typeof(List<DungeonLevel>), true));
			writer.WriteProperty("runTypeLanguageKey", missions.runTypeLanguageKey, ES3Type_string.Instance);
			writer.WriteProperty("runTypeNumber", missions.runTypeNumber, ES3Type_int.Instance);
			writer.WritePropertyByRef("validForCharacter", missions.validForCharacter);
			writer.WriteProperty("rewards", missions.rewards, ES3TypeMgr.GetOrCreateES3Type(typeof(List<GameObject>), true));
			writer.WriteProperty("runProperties", missions.runProperties, ES3TypeMgr.GetOrCreateES3Type(typeof(List<RunType.RunProperty>), true));
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000A5834 File Offset: 0x000A3A34
		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			Missions missions = (Missions)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "dungeonLevels"))
				{
					if (!(text == "runTypeLanguageKey"))
					{
						if (!(text == "runTypeNumber"))
						{
							if (!(text == "validForCharacter"))
							{
								if (!(text == "rewards"))
								{
									if (!(text == "runProperties"))
									{
										reader.Skip();
									}
									else
									{
										missions.runProperties = reader.Read<List<RunType.RunProperty>>();
									}
								}
								else
								{
									missions.rewards = reader.Read<List<GameObject>>();
								}
							}
							else
							{
								missions.validForCharacter = reader.Read<Character>();
							}
						}
						else
						{
							missions.runTypeNumber = reader.Read<int>(ES3Type_int.Instance);
						}
					}
					else
					{
						missions.runTypeLanguageKey = reader.Read<string>(ES3Type_string.Instance);
					}
				}
				else
				{
					missions.dungeonLevels = reader.Read<List<DungeonLevel>>();
				}
			}
		}

		// Token: 0x04000E0B RID: 3595
		public static ES3Type Instance;
	}
}
