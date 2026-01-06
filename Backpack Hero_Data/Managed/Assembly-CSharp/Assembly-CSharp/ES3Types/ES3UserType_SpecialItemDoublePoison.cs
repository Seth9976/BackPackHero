using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200020D RID: 525
	[Preserve]
	[ES3Properties(new string[] { "trigger", "target", "description" })]
	public class ES3UserType_SpecialItemDoublePoison : ES3ComponentType
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x000A88C9 File Offset: 0x000A6AC9
		public ES3UserType_SpecialItemDoublePoison()
			: base(typeof(SpecialItemDoublePoison))
		{
			ES3UserType_SpecialItemDoublePoison.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000A88E8 File Offset: 0x000A6AE8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemDoublePoison specialItemDoublePoison = (SpecialItemDoublePoison)obj;
			writer.WriteProperty("trigger", specialItemDoublePoison.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemDoublePoison.target, ES3Type_enum.Instance);
			writer.WriteProperty("description", specialItemDoublePoison.description, ES3Type_string.Instance);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000A8950 File Offset: 0x000A6B50
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemDoublePoison specialItemDoublePoison = (SpecialItemDoublePoison)obj;
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
							specialItemDoublePoison.description = reader.Read<string>(ES3Type_string.Instance);
						}
					}
					else
					{
						specialItemDoublePoison.target = reader.Read<Item2.Effect.Target>(ES3Type_enum.Instance);
					}
				}
				else
				{
					specialItemDoublePoison.trigger = reader.Read<Item2.Trigger>();
				}
			}
		}

		// Token: 0x04000E39 RID: 3641
		public static ES3Type Instance;
	}
}
