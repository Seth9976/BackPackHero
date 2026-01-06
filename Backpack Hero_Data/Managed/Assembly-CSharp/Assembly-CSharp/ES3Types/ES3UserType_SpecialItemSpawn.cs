using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000213 RID: 531
	[Preserve]
	[ES3Properties(new string[] { "rewardItems", "trigger", "target", "description" })]
	public class ES3UserType_SpecialItemSpawn : ES3ComponentType
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x000A8D65 File Offset: 0x000A6F65
		public ES3UserType_SpecialItemSpawn()
			: base(typeof(SpecialItemSpawn))
		{
			ES3UserType_SpecialItemSpawn.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000A8D84 File Offset: 0x000A6F84
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpecialItemSpawn specialItemSpawn = (SpecialItemSpawn)obj;
			writer.WritePrivateField("rewardItems", specialItemSpawn);
			writer.WriteProperty("trigger", specialItemSpawn.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", specialItemSpawn.target, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Effect.Target), true));
			writer.WriteProperty("description", specialItemSpawn.description, ES3Type_string.Instance);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000A8E04 File Offset: 0x000A7004
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpecialItemSpawn specialItemSpawn = (SpecialItemSpawn)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "rewardItems"))
				{
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
								specialItemSpawn.description = reader.Read<string>(ES3Type_string.Instance);
							}
						}
						else
						{
							specialItemSpawn.target = reader.Read<Item2.Effect.Target>();
						}
					}
					else
					{
						specialItemSpawn.trigger = reader.Read<Item2.Trigger>();
					}
				}
				else
				{
					reader.SetPrivateField("rewardItems", reader.Read<GameObject[]>(), specialItemSpawn);
				}
			}
		}

		// Token: 0x04000E3F RID: 3647
		public static ES3Type Instance;
	}
}
