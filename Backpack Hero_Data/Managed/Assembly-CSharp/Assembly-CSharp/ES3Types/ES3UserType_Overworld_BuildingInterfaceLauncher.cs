using System;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001E3 RID: 483
	[Preserve]
	[ES3Properties(new string[] { "buildingInterfacePrefab", "storedItems", "exclamation", "interactionRadius" })]
	public class ES3UserType_Overworld_BuildingInterfaceLauncher : ES3ComponentType
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x000A5CBD File Offset: 0x000A3EBD
		public ES3UserType_Overworld_BuildingInterfaceLauncher()
			: base(typeof(Overworld_BuildingInterfaceLauncher))
		{
			ES3UserType_Overworld_BuildingInterfaceLauncher.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000A5CDC File Offset: 0x000A3EDC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Overworld_BuildingInterfaceLauncher overworld_BuildingInterfaceLauncher = (Overworld_BuildingInterfaceLauncher)obj;
			writer.WritePrivateFieldByRef("buildingInterfacePrefab", overworld_BuildingInterfaceLauncher);
			writer.WriteProperty("storedItems", overworld_BuildingInterfaceLauncher.storedItems, ES3TypeMgr.GetOrCreateES3Type(typeof(List<string>), true));
			writer.WritePrivateFieldByRef("exclamation", overworld_BuildingInterfaceLauncher);
			writer.WriteProperty("interactionRadius", overworld_BuildingInterfaceLauncher.interactionRadius, ES3Type_float.Instance);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000A5D44 File Offset: 0x000A3F44
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Overworld_BuildingInterfaceLauncher overworld_BuildingInterfaceLauncher = (Overworld_BuildingInterfaceLauncher)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "buildingInterfacePrefab"))
				{
					if (!(text == "storedItems"))
					{
						if (!(text == "exclamation"))
						{
							if (!(text == "interactionRadius"))
							{
								reader.Skip();
							}
							else
							{
								overworld_BuildingInterfaceLauncher.interactionRadius = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							reader.SetPrivateField("exclamation", reader.Read<GameObject>(), overworld_BuildingInterfaceLauncher);
						}
					}
					else
					{
						overworld_BuildingInterfaceLauncher.storedItems = reader.Read<List<string>>();
					}
				}
				else
				{
					reader.SetPrivateField("buildingInterfacePrefab", reader.Read<GameObject>(), overworld_BuildingInterfaceLauncher);
				}
			}
		}

		// Token: 0x04000E0F RID: 3599
		public static ES3Type Instance;
	}
}
