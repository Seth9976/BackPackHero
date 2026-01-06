using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001FF RID: 511
	[Preserve]
	[ES3Properties(new string[] { "runTypeLanguageKey", "requiredToUnlock", "validForCharacters", "runProperties", "name" })]
	public class ES3UserType_RunType : ES3ScriptableObjectType
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x000A7935 File Offset: 0x000A5B35
		public ES3UserType_RunType()
			: base(typeof(RunType))
		{
			ES3UserType_RunType.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000A7954 File Offset: 0x000A5B54
		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			RunType runType = (RunType)obj;
			writer.WriteProperty("runTypeLanguageKey", runType.runTypeLanguageKey, ES3Type_string.Instance);
			writer.WriteProperty("requiredToUnlock", runType.requiredToUnlock, ES3TypeMgr.GetOrCreateES3Type(typeof(List<RunType>), true));
			writer.WriteProperty("validForCharacters", runType.validForCharacters, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Character.CharacterName>), true));
			writer.WriteProperty("runProperties", runType.runProperties, ES3TypeMgr.GetOrCreateES3Type(typeof(List<RunType.RunProperty>), true));
			writer.WriteProperty("name", runType.name, ES3Type_string.Instance);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000A79F8 File Offset: 0x000A5BF8
		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			RunType runType = (RunType)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "runTypeLanguageKey"))
				{
					if (!(text == "requiredToUnlock"))
					{
						if (!(text == "validForCharacters"))
						{
							if (!(text == "runProperties"))
							{
								if (!(text == "name"))
								{
									reader.Skip();
								}
								else
								{
									runType.name = reader.Read<string>(ES3Type_string.Instance);
								}
							}
							else
							{
								runType.runProperties = reader.Read<List<RunType.RunProperty>>();
							}
						}
						else
						{
							runType.validForCharacters = reader.Read<List<Character.CharacterName>>();
						}
					}
					else
					{
						runType.requiredToUnlock = reader.Read<List<RunType>>();
					}
				}
				else
				{
					runType.runTypeLanguageKey = reader.Read<string>(ES3Type_string.Instance);
				}
			}
		}

		// Token: 0x04000E2B RID: 3627
		public static ES3Type Instance;
	}
}
