using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001DD RID: 477
	[Preserve]
	[ES3Properties(new string[] { "maxPower", "currentPower", "amountTextParent", "trigger", "target", "description" })]
	public class ES3UserType_ManaStone : ES3ComponentType
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x000A5551 File Offset: 0x000A3751
		public ES3UserType_ManaStone()
			: base(typeof(ManaStone))
		{
			ES3UserType_ManaStone.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000A5570 File Offset: 0x000A3770
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ManaStone manaStone = (ManaStone)obj;
			writer.WriteProperty("maxPower", manaStone.maxPower, ES3Type_int.Instance);
			writer.WriteProperty("currentPower", manaStone.currentPower, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("amountTextParent", manaStone);
			writer.WriteProperty("trigger", manaStone.trigger, ES3TypeMgr.GetOrCreateES3Type(typeof(Item2.Trigger), true));
			writer.WriteProperty("target", manaStone.target, ES3Type_enum.Instance);
			writer.WriteProperty("description", manaStone.description, ES3Type_string.Instance);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000A5618 File Offset: 0x000A3818
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ManaStone manaStone = (ManaStone)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "maxPower"))
				{
					if (!(text == "currentPower"))
					{
						if (!(text == "amountTextParent"))
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
										manaStone.description = reader.Read<string>(ES3Type_string.Instance);
									}
								}
								else
								{
									manaStone.target = reader.Read<Item2.Effect.Target>(ES3Type_enum.Instance);
								}
							}
							else
							{
								manaStone.trigger = reader.Read<Item2.Trigger>();
							}
						}
						else
						{
							reader.SetPrivateField("amountTextParent", reader.Read<GameObject>(), manaStone);
						}
					}
					else
					{
						manaStone.currentPower = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					manaStone.maxPower = reader.Read<int>(ES3Type_int.Instance);
				}
			}
		}

		// Token: 0x04000E09 RID: 3593
		public static ES3Type Instance;
	}
}
