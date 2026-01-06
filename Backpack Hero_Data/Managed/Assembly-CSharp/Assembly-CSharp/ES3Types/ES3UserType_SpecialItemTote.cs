using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000215 RID: 533
	[Preserve]
	[ES3Properties(new string[] { "type", "trigger", "target", "description", "value" })]
	public class ES3UserType_SpecialItemTote : ES3ComponentType
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x000A8F01 File Offset: 0x000A7101
		public ES3UserType_SpecialItemTote()
			: base(typeof(SpecialItemTote))
		{
			ES3UserType_SpecialItemTote.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000A8F20 File Offset: 0x000A7120
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemTote specialItemTote = (SpecialItemTote)obj;
			writer.WritePrivateField("type", specialItemTote);
			writer.WriteProperty("trigger", specialItemTote.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemTote.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemTote.description, ES3Type_string.Instance);
			writer.WriteProperty("value", specialItemTote.value, ES3Type_float.Instance);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000A8FB8 File Offset: 0x000A71B8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemTote specialItemTote = (SpecialItemTote)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
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
									specialItemTote.value = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								specialItemTote.description = reader.Read<string>(ES3Type_string.Instance);
							}
						}
						else
						{
							specialItemTote.target = reader.Read<Item2.Effect.Target>();
						}
					}
					else
					{
						specialItemTote.trigger = reader.Read<Item2.Trigger>();
					}
				}
				else
				{
					reader.SetPrivateField("type", reader.Read<SpecialItemTote.Type>(), specialItemTote);
				}
			}
		}

		// Token: 0x04000E41 RID: 3649
		public static ES3Type Instance;
	}
}
