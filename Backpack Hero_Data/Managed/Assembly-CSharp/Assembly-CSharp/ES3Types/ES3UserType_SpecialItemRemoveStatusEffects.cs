using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000211 RID: 529
	[Preserve]
	[ES3Properties(new string[] { "trigger", "target", "description" })]
	public class ES3UserType_SpecialItemRemoveStatusEffects : ES3ComponentType
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x000A8BFD File Offset: 0x000A6DFD
		public ES3UserType_SpecialItemRemoveStatusEffects()
			: base(typeof(SpecialItemRemoveStatusEffects))
		{
			ES3UserType_SpecialItemRemoveStatusEffects.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000A8C1C File Offset: 0x000A6E1C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemRemoveStatusEffects specialItemRemoveStatusEffects = (SpecialItemRemoveStatusEffects)obj;
			writer.WriteProperty("trigger", specialItemRemoveStatusEffects.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemRemoveStatusEffects.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemRemoveStatusEffects.description, ES3Type_string.Instance);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000A8C90 File Offset: 0x000A6E90
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemRemoveStatusEffects specialItemRemoveStatusEffects = (SpecialItemRemoveStatusEffects)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "trigger"))
				{
					if (!(text == "target"))
					{
						if (!(text == "description"))
						{
							reader.Skip();
						}
						else
						{
							specialItemRemoveStatusEffects.description = reader.Read<string>(ES3Type_string.Instance);
						}
					}
					else
					{
						specialItemRemoveStatusEffects.target = reader.Read<Item2.Effect.Target>();
					}
				}
				else
				{
					specialItemRemoveStatusEffects.trigger = reader.Read<Item2.Trigger>();
				}
			}
		}

		// Token: 0x04000E3D RID: 3645
		public static ES3Type Instance;
	}
}
