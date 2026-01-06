using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000217 RID: 535
	[Preserve]
	[ES3Properties(new string[] { "prefabs", "type", "trigger", "target", "description", "value" })]
	public class ES3UserType_SpecialItemUnique : ES3ComponentType
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x000A90D9 File Offset: 0x000A72D9
		public ES3UserType_SpecialItemUnique()
			: base(typeof(SpecialItemUnique))
		{
			ES3UserType_SpecialItemUnique.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000A90F8 File Offset: 0x000A72F8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemUnique specialItemUnique = (SpecialItemUnique)obj;
			writer.WritePrivateField("prefabs", specialItemUnique);
			writer.WritePrivateField("type", specialItemUnique);
			writer.WriteProperty("trigger", specialItemUnique.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemUnique.target, ES3Type_enum.Instance);
			writer.WriteProperty("description", specialItemUnique.description, ES3Type_string.Instance);
			writer.WriteProperty("value", specialItemUnique.value, ES3Type_float.Instance);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000A9194 File Offset: 0x000A7394
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemUnique specialItemUnique = (SpecialItemUnique)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "prefabs"))
				{
					if (!(text == "type"))
					{
						if (!(text == "trigger"))
						{
							if (!(text == "target"))
							{
								if (!(text == "description"))
								{
									if (!(text == "value"))
									{
										reader.Skip();
									}
									else
									{
										specialItemUnique.value = reader.Read<float>(ES3Type_float.Instance);
									}
								}
								else
								{
									specialItemUnique.description = reader.Read<string>(ES3Type_string.Instance);
								}
							}
							else
							{
								specialItemUnique.target = reader.Read<Item2.Effect.Target>(ES3Type_enum.Instance);
							}
						}
						else
						{
							specialItemUnique.trigger = reader.Read<Item2.Trigger>();
						}
					}
					else
					{
						reader.SetPrivateField("type", reader.Read<SpecialItemUnique.Type>(), specialItemUnique);
					}
				}
				else
				{
					reader.SetPrivateField("prefabs", reader.Read<List<GameObject>>(), specialItemUnique);
				}
			}
		}

		// Token: 0x04000E43 RID: 3651
		public static ES3Type Instance;
	}
}
