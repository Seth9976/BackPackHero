using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001B3 RID: 435
	[Preserve]
	[ES3Properties(new string[] { "summoningCosts" })]
	public class ES3UserType_Carving : ES3ComponentType
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x000A1F61 File Offset: 0x000A0161
		public ES3UserType_Carving()
			: base(typeof(Carving))
		{
			ES3UserType_Carving.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000A1F80 File Offset: 0x000A0180
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Carving carving = (Carving)obj;
			writer.WriteProperty("summoningCosts", carving.summoningCosts, ES3TypeMgr.GetOrCreateES3Type(typeof(List<Item2.Cost>), true));
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000A1FB8 File Offset: 0x000A01B8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Carving carving = (Carving)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "summoningCosts")
					{
						carving.summoningCosts = reader.Read<List<Item2.Cost>>();
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000DDF RID: 3551
		public static ES3Type Instance;
	}
}
