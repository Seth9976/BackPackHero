using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200020F RID: 527
	[Preserve]
	[ES3Properties(new string[] { "effect", "trigger", "target", "description", "value" })]
	public class ES3UserType_specialItemPochette : ES3ComponentType
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x000A8A29 File Offset: 0x000A6C29
		public ES3UserType_specialItemPochette()
			: base(typeof(specialItemPochette))
		{
			ES3UserType_specialItemPochette.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000A8A48 File Offset: 0x000A6C48
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			specialItemPochette specialItemPochette = (specialItemPochette)obj;
			writer.WritePrivateField("effect", specialItemPochette);
			writer.WriteProperty("trigger", specialItemPochette.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemPochette.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemPochette.description, ES3Type_string.Instance);
			writer.WriteProperty("value", specialItemPochette.value, ES3Type_float.Instance);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000A8AE0 File Offset: 0x000A6CE0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			specialItemPochette specialItemPochette = (specialItemPochette)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "effect"))
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
									specialItemPochette.value = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								specialItemPochette.description = reader.Read<string>(ES3Type_string.Instance);
							}
						}
						else
						{
							specialItemPochette.target = reader.Read<Item2.Effect.Target>();
						}
					}
					else
					{
						specialItemPochette.trigger = reader.Read<Item2.Trigger>();
					}
				}
				else
				{
					reader.SetPrivateField("effect", reader.Read<Item2.Effect>(), specialItemPochette);
				}
			}
		}

		// Token: 0x04000E3B RID: 3643
		public static ES3Type Instance;
	}
}
