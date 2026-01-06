using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000227 RID: 551
	[Preserve]
	[ES3Properties(new string[] { "effectType", "textKeys", "damageIndicatorForText" })]
	public class ES3UserType_VisualEffectOnItemUse : ES3ComponentType
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x000ABE39 File Offset: 0x000AA039
		public ES3UserType_VisualEffectOnItemUse()
			: base(typeof(VisualEffectOnItemUse))
		{
			ES3UserType_VisualEffectOnItemUse.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000ABE58 File Offset: 0x000AA058
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			VisualEffectOnItemUse visualEffectOnItemUse = (VisualEffectOnItemUse)obj;
			writer.WriteProperty("effectType", visualEffectOnItemUse.effectType, ES3TypeMgr.GetOrCreateES3Type(typeof(VisualEffectOnItemUse.EffectType), true));
			writer.WriteProperty("textKeys", visualEffectOnItemUse.textKeys, ES3TypeMgr.GetOrCreateES3Type(typeof(List<string>), true));
			writer.WritePrivateFieldByRef("damageIndicatorForText", visualEffectOnItemUse);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000ABEC0 File Offset: 0x000AA0C0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			VisualEffectOnItemUse visualEffectOnItemUse = (VisualEffectOnItemUse)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "effectType"))
				{
					if (!(text == "textKeys"))
					{
						if (!(text == "damageIndicatorForText"))
						{
							reader.Skip();
						}
						else
						{
							reader.SetPrivateField("damageIndicatorForText", reader.Read<GameObject>(), visualEffectOnItemUse);
						}
					}
					else
					{
						visualEffectOnItemUse.textKeys = reader.Read<List<string>>();
					}
				}
				else
				{
					visualEffectOnItemUse.effectType = reader.Read<VisualEffectOnItemUse.EffectType>();
				}
			}
		}

		// Token: 0x04000E53 RID: 3667
		public static ES3Type Instance;
	}
}
