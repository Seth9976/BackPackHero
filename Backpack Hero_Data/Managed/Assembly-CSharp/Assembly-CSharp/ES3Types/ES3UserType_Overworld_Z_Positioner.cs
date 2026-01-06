using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001F3 RID: 499
	[Preserve]
	[ES3Properties(new string[] { "baseOfObject" })]
	public class ES3UserType_Overworld_Z_Positioner : ES3ComponentType
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x000A6CA9 File Offset: 0x000A4EA9
		public ES3UserType_Overworld_Z_Positioner()
			: base(typeof(Overworld_Z_Positioner))
		{
			ES3UserType_Overworld_Z_Positioner.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000A6CC8 File Offset: 0x000A4EC8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_Z_Positioner overworld_Z_Positioner = (Overworld_Z_Positioner)obj;
			writer.WritePrivateFieldByRef("baseOfObject", overworld_Z_Positioner);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000A6CE8 File Offset: 0x000A4EE8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_Z_Positioner overworld_Z_Positioner = (Overworld_Z_Positioner)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "baseOfObject")
					{
						reader.SetPrivateField("baseOfObject", reader.Read<Transform>(), overworld_Z_Positioner);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000E1F RID: 3615
		public static ES3Type Instance;
	}
}
