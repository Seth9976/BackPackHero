using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000203 RID: 515
	[Preserve]
	[ES3Properties(new string[]
	{
		"triggerKey", "secondTriggerKey", "itemTypes", "triggerType", "scriptedTriggerType", "showProgress", "onlyInCombat", "recursive", "recursiveUsesCosts", "triggerValueType",
		"triggerValue"
	})]
	public class ES3UserType_ScriptedTrigger : ES3ComponentType
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x000A7C55 File Offset: 0x000A5E55
		public ES3UserType_ScriptedTrigger()
			: base(typeof(ScriptedTrigger))
		{
			ES3UserType_ScriptedTrigger.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000A7C74 File Offset: 0x000A5E74
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ScriptedTrigger scriptedTrigger = (ScriptedTrigger)obj;
			writer.WriteProperty("triggerKey", scriptedTrigger.triggerKey, ES3Type_string.Instance);
			writer.WriteProperty("secondTriggerKey", scriptedTrigger.secondTriggerKey, ES3Type_string.Instance);
			writer.WriteProperty("itemTypes", scriptedTrigger.itemTypes, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.ItemType>), true));
			writer.WriteProperty("triggerType", scriptedTrigger.triggerType, ES3Type_enum.Instance);
			writer.WriteProperty("scriptedTriggerType", scriptedTrigger.scriptedTriggerType, ES3TypeMgr.GetOrCreateES3Type(typeof(ScriptedTrigger.ScriptedTriggerType), true));
			writer.WriteProperty("showProgress", scriptedTrigger.showProgress, ES3Type_bool.Instance);
			writer.WriteProperty("onlyInCombat", scriptedTrigger.onlyInCombat, ES3Type_bool.Instance);
			writer.WriteProperty("recursive", scriptedTrigger.recursive, ES3Type_bool.Instance);
			writer.WriteProperty("recursiveUsesCosts", scriptedTrigger.recursiveUsesCosts, ES3Type_bool.Instance);
			writer.WriteProperty("triggerValueType", scriptedTrigger.triggerValueType, ES3TypeMgr.GetOrCreateES3Type(typeof(ScriptedTrigger.TriggerValueType), true));
			writer.WriteProperty("triggerValue", scriptedTrigger.triggerValue, ES3Type_int.Instance);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000A7DC4 File Offset: 0x000A5FC4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ScriptedTrigger scriptedTrigger = (ScriptedTrigger)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1542339703U)
				{
					if (num <= 154608800U)
					{
						if (num != 28304730U)
						{
							if (num == 154608800U)
							{
								if (text == "triggerValue")
								{
									scriptedTrigger.triggerValue = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "triggerKey")
						{
							scriptedTrigger.triggerKey = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (num != 275479533U)
					{
						if (num != 1209697239U)
						{
							if (num == 1542339703U)
							{
								if (text == "recursive")
								{
									scriptedTrigger.recursive = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "showProgress")
						{
							scriptedTrigger.showProgress = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (text == "scriptedTriggerType")
					{
						scriptedTrigger.scriptedTriggerType = reader.Read<ScriptedTrigger.ScriptedTriggerType>();
						continue;
					}
				}
				else if (num <= 2605013595U)
				{
					if (num != 1919263208U)
					{
						if (num != 2547934276U)
						{
							if (num == 2605013595U)
							{
								if (text == "triggerType")
								{
									scriptedTrigger.triggerType = reader.Read<Item2.Trigger.ActionTrigger>(ES3Type_enum.Instance);
									continue;
								}
							}
						}
						else if (text == "onlyInCombat")
						{
							scriptedTrigger.onlyInCombat = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (text == "triggerValueType")
					{
						scriptedTrigger.triggerValueType = reader.Read<ScriptedTrigger.TriggerValueType>();
						continue;
					}
				}
				else if (num != 2948965762U)
				{
					if (num != 3464437511U)
					{
						if (num == 3707285407U)
						{
							if (text == "itemTypes")
							{
								scriptedTrigger.itemTypes = reader.Read<List<Item2.ItemType>>();
								continue;
							}
						}
					}
					else if (text == "recursiveUsesCosts")
					{
						scriptedTrigger.recursiveUsesCosts = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (text == "secondTriggerKey")
				{
					scriptedTrigger.secondTriggerKey = reader.Read<string>(ES3Type_string.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E2F RID: 3631
		public static ES3Type Instance;
	}
}
