using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001B9 RID: 441
	[Preserve]
	[ES3Properties(new string[] { "type", "size", "difficulty" })]
	public class ES3UserType_Curse : ES3ComponentType
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x000A239D File Offset: 0x000A059D
		public ES3UserType_Curse()
			: base(typeof(Curse))
		{
			ES3UserType_Curse.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000A23BC File Offset: 0x000A05BC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Curse curse = (Curse)obj;
			writer.WriteProperty("type", curse.type, ES3TypeMgr.GetOrCreateES3Type(typeof(Curse.Type), true));
			writer.WriteProperty("size", curse.size, ES3Type_int.Instance);
			writer.WriteProperty("difficulty", curse.difficulty, ES3Type_int.Instance);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000A242C File Offset: 0x000A062C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Curse curse = (Curse)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "type"))
				{
					if (!(text == "size"))
					{
						if (!(text == "difficulty"))
						{
							reader.Skip();
						}
						else
						{
							curse.difficulty = reader.Read<int>(ES3Type_int.Instance);
						}
					}
					else
					{
						curse.size = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					curse.type = reader.Read<Curse.Type>();
				}
			}
		}

		// Token: 0x04000DE5 RID: 3557
		public static ES3Type Instance;
	}
}
