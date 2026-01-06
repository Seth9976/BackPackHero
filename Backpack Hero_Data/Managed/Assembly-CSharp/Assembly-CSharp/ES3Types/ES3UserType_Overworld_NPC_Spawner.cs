using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001EB RID: 491
	[Preserve]
	[ES3Properties(new string[] { "spawnGenericNPCs", "timeToSpawn" })]
	public class ES3UserType_Overworld_NPC_Spawner : ES3ComponentType
	{
		// Token: 0x0600119D RID: 4509 RVA: 0x000A6211 File Offset: 0x000A4411
		public ES3UserType_Overworld_NPC_Spawner()
			: base(typeof(Overworld_NPC_Spawner))
		{
			ES3UserType_Overworld_NPC_Spawner.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000A6230 File Offset: 0x000A4430
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_NPC_Spawner overworld_NPC_Spawner = (Overworld_NPC_Spawner)obj;
			writer.WritePrivateField("spawnGenericNPCs", overworld_NPC_Spawner);
			writer.WritePrivateField("timeToSpawn", overworld_NPC_Spawner);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000A625C File Offset: 0x000A445C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_NPC_Spawner overworld_NPC_Spawner = (Overworld_NPC_Spawner)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "spawnGenericNPCs"))
				{
					if (!(text == "timeToSpawn"))
					{
						reader.Skip();
					}
					else
					{
						reader.SetPrivateField("timeToSpawn", reader.Read<float>(), overworld_NPC_Spawner);
					}
				}
				else
				{
					reader.SetPrivateField("spawnGenericNPCs", reader.Read<bool>(), overworld_NPC_Spawner);
				}
			}
		}

		// Token: 0x04000E17 RID: 3607
		public static ES3Type Instance;
	}
}
