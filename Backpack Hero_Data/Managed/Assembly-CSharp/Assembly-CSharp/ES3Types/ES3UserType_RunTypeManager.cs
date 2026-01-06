using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000201 RID: 513
	[Preserve]
	[ES3Properties(new string[] { "runType", "standardRunType", "runProperties" })]
	public class ES3UserType_RunTypeManager : ES3ComponentType
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x000A7B0D File Offset: 0x000A5D0D
		public ES3UserType_RunTypeManager()
			: base(typeof(RunTypeManager))
		{
			ES3UserType_RunTypeManager.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000A7B2C File Offset: 0x000A5D2C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			RunTypeManager runTypeManager = (RunTypeManager)obj;
			writer.WritePropertyByRef("runType", runTypeManager.runType);
			writer.WritePrivateFieldByRef("standardRunType", runTypeManager);
			writer.WriteProperty("runProperties", runTypeManager.runProperties, ES3TypeMgr.GetOrCreateES3Type(typeof(List<RunType.RunProperty>), true));
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000A7B80 File Offset: 0x000A5D80
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			RunTypeManager runTypeManager = (RunTypeManager)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "runType"))
				{
					if (!(text == "standardRunType"))
					{
						if (!(text == "runProperties"))
						{
							reader.Skip();
						}
						else
						{
							runTypeManager.runProperties = reader.Read<List<RunType.RunProperty>>();
						}
					}
					else
					{
						reader.SetPrivateField("standardRunType", reader.Read<RunType>(), runTypeManager);
					}
				}
				else
				{
					runTypeManager.runType = reader.Read<RunType>();
				}
			}
		}

		// Token: 0x04000E2D RID: 3629
		public static ES3Type Instance;
	}
}
