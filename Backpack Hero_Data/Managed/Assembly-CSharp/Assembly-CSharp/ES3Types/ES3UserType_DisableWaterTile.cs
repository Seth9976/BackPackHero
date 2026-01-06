using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001BD RID: 445
	[Preserve]
	[ES3Properties(new string[] { "bridgeCollider" })]
	public class ES3UserType_DisableWaterTile : ES3ComponentType
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x000A2629 File Offset: 0x000A0829
		public ES3UserType_DisableWaterTile()
			: base(typeof(DisableWaterTile))
		{
			ES3UserType_DisableWaterTile.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000A2648 File Offset: 0x000A0848
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DisableWaterTile disableWaterTile = (DisableWaterTile)obj;
			writer.WritePrivateFieldByRef("bridgeCollider", disableWaterTile);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000A2668 File Offset: 0x000A0868
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DisableWaterTile disableWaterTile = (DisableWaterTile)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "bridgeCollider")
					{
						reader.SetPrivateField("bridgeCollider", reader.Read<BoxCollider2D>(), disableWaterTile);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000DE9 RID: 3561
		public static ES3Type Instance;
	}
}
