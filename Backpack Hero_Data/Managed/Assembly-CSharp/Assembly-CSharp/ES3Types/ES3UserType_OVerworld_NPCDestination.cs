using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001E9 RID: 489
	[Preserve]
	[ES3Properties(new string[] { "actionHere", "radius" })]
	public class ES3UserType_OVerworld_NPCDestination : ES3ComponentType
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x000A60E1 File Offset: 0x000A42E1
		public ES3UserType_OVerworld_NPCDestination()
			: base(typeof(OVerworld_NPCDestination))
		{
			ES3UserType_OVerworld_NPCDestination.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000A6100 File Offset: 0x000A4300
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			OVerworld_NPCDestination overworld_NPCDestination = (OVerworld_NPCDestination)obj;
			writer.WriteProperty("actionHere", overworld_NPCDestination.actionHere, ES3TypeMgr.GetOrCreateES3Type(typeof(Overworld_NPC.Phase), true));
			writer.WriteProperty("radius", overworld_NPCDestination.radius, ES3Type_float.Instance);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000A6158 File Offset: 0x000A4358
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			OVerworld_NPCDestination overworld_NPCDestination = (OVerworld_NPCDestination)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "actionHere"))
				{
					if (!(text == "radius"))
					{
						reader.Skip();
					}
					else
					{
						overworld_NPCDestination.radius = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					overworld_NPCDestination.actionHere = reader.Read<Overworld_NPC.Phase>();
				}
			}
		}

		// Token: 0x04000E15 RID: 3605
		public static ES3Type Instance;
	}
}
