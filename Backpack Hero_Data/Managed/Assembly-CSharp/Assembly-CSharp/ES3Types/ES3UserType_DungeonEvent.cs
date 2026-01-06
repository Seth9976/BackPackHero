using System;
using System.Collections.Generic;
using ES3Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001C1 RID: 449
	[Preserve]
	[ES3Properties(new string[]
	{
		"caveIn", "passable", "dungeonEventType", "itemsToSpawn", "exitPrefab", "sprites", "destroyParticles", "turnsToExpire", "text", "eventProperties",
		"doorNumber", "storedItems"
	})]
	public class ES3UserType_DungeonEvent : ES3ComponentType
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x000A27AD File Offset: 0x000A09AD
		public ES3UserType_DungeonEvent()
			: base(typeof(DungeonEvent))
		{
			ES3UserType_DungeonEvent.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000A27CC File Offset: 0x000A09CC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DungeonEvent dungeonEvent = (DungeonEvent)obj;
			writer.WritePrivateField("caveIn", dungeonEvent);
			writer.WriteProperty("passable", dungeonEvent.passable, ES3Type_bool.Instance);
			writer.WriteProperty("dungeonEventType", dungeonEvent.dungeonEventType, ES3TypeMgr.GetOrCreateES3Type(typeof(DungeonEvent.DungeonEventType), true));
			writer.WriteProperty("itemsToSpawn", dungeonEvent.itemsToSpawn, ES3TypeMgr.GetOrCreateES3Type(typeof(List<GameObject>), true));
			writer.WritePropertyByRef("exitPrefab", dungeonEvent.exitPrefab);
			writer.WritePrivateField("sprites", dungeonEvent);
			writer.WritePrivateFieldByRef("destroyParticles", dungeonEvent);
			writer.WriteProperty("turnsToExpire", dungeonEvent.turnsToExpire, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("text", dungeonEvent);
			writer.WriteProperty("eventProperties", dungeonEvent.eventProperties, ES3TypeMgr.GetOrCreateES3Type(typeof(List<DungeonEvent.EventProperty>), true));
			writer.WriteProperty("doorNumber", dungeonEvent.doorNumber, ES3Type_int.Instance);
			writer.WriteProperty("storedItems", dungeonEvent.storedItems, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2>), true));
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000A28FC File Offset: 0x000A0AFC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DungeonEvent dungeonEvent = (DungeonEvent)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1326782934U)
				{
					if (num <= 807317162U)
					{
						if (num != 289362175U)
						{
							if (num != 436831587U)
							{
								if (num == 807317162U)
								{
									if (text == "storedItems")
									{
										dungeonEvent.storedItems = reader.Read<List<Item2>>();
										continue;
									}
								}
							}
							else if (text == "exitPrefab")
							{
								dungeonEvent.exitPrefab = reader.Read<GameObject>(ES3Type_GameObject.Instance);
								continue;
							}
						}
						else if (text == "dungeonEventType")
						{
							dungeonEvent.dungeonEventType = reader.Read<DungeonEvent.DungeonEventType>();
							continue;
						}
					}
					else if (num != 1086568785U)
					{
						if (num != 1119300695U)
						{
							if (num == 1326782934U)
							{
								if (text == "eventProperties")
								{
									dungeonEvent.eventProperties = reader.Read<List<DungeonEvent.EventProperty>>();
									continue;
								}
							}
						}
						else if (text == "caveIn")
						{
							reader.SetPrivateField("caveIn", reader.Read<Vector2>(), dungeonEvent);
							continue;
						}
					}
					else if (text == "itemsToSpawn")
					{
						dungeonEvent.itemsToSpawn = reader.Read<List<GameObject>>();
						continue;
					}
				}
				else if (num <= 3481203961U)
				{
					if (num != 2496828084U)
					{
						if (num != 3185987134U)
						{
							if (num == 3481203961U)
							{
								if (text == "turnsToExpire")
								{
									dungeonEvent.turnsToExpire = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "text")
						{
							reader.SetPrivateField("text", reader.Read<TextMeshPro>(), dungeonEvent);
							continue;
						}
					}
					else if (text == "doorNumber")
					{
						dungeonEvent.doorNumber = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num != 3532484164U)
				{
					if (num != 3864082109U)
					{
						if (num == 3970901968U)
						{
							if (text == "passable")
							{
								dungeonEvent.passable = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "sprites")
					{
						reader.SetPrivateField("sprites", reader.Read<Sprite[]>(), dungeonEvent);
						continue;
					}
				}
				else if (text == "destroyParticles")
				{
					reader.SetPrivateField("destroyParticles", reader.Read<GameObject>(), dungeonEvent);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000DED RID: 3565
		public static ES3Type Instance;
	}
}
