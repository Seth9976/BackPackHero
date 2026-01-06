using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001E7 RID: 487
	[Preserve]
	[ES3Properties(new string[] { "key", "interactable_Actions", "interactionRadius" })]
	public class ES3UserType_Overworld_Interactable : ES3ComponentType
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x000A5F7D File Offset: 0x000A417D
		public ES3UserType_Overworld_Interactable()
			: base(typeof(Overworld_Interactable))
		{
			ES3UserType_Overworld_Interactable.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000A5F9C File Offset: 0x000A419C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_Interactable overworld_Interactable = (Overworld_Interactable)obj;
			writer.WriteProperty("key", overworld_Interactable.key, ES3Type_enum.Instance);
			writer.WriteProperty("interactable_Actions", overworld_Interactable.interactable_Actions, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Overworld_Interactable.interactable_Action>), true));
			writer.WriteProperty("interactionRadius", overworld_Interactable.interactionRadius, ES3Type_float.Instance);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000A6008 File Offset: 0x000A4208
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_Interactable overworld_Interactable = (Overworld_Interactable)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "key"))
				{
					if (!(text == "interactable_Actions"))
					{
						if (!(text == "interactionRadius"))
						{
							reader.Skip();
						}
						else
						{
							overworld_Interactable.interactionRadius = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						overworld_Interactable.interactable_Actions = reader.Read<List<Overworld_Interactable.interactable_Action>>();
					}
				}
				else
				{
					overworld_Interactable.key = reader.Read<InputHandler.Key>(ES3Type_enum.Instance);
				}
			}
		}

		// Token: 0x04000E13 RID: 3603
		public static ES3Type Instance;
	}
}
