using System;

namespace ES3Types
{
	// Token: 0x020001EC RID: 492
	public class ES3UserType_Overworld_NPC_SpawnerArray : ES3ArrayType
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x000A630C File Offset: 0x000A450C
		public ES3UserType_Overworld_NPC_SpawnerArray()
			: base(typeof(Overworld_NPC_Spawner[]), ES3UserType_Overworld_NPC_Spawner.Instance)
		{
			ES3UserType_Overworld_NPC_SpawnerArray.Instance = this;
		}

		// Token: 0x04000E18 RID: 3608
		public static ES3Type Instance;
	}
}
