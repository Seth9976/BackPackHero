using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001D3 RID: 467
	[Preserve]
	[ES3Properties(new string[] { "storedColor" })]
	public class ES3UserType_ItemBorderBackground : ES3ComponentType
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x000A4795 File Offset: 0x000A2995
		public ES3UserType_ItemBorderBackground()
			: base(typeof(ItemBorderBackground))
		{
			ES3UserType_ItemBorderBackground.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000A47B4 File Offset: 0x000A29B4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ItemBorderBackground itemBorderBackground = (ItemBorderBackground)obj;
			writer.WriteProperty("storedColor", itemBorderBackground.storedColor, ES3Type_Color.Instance);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000A47E4 File Offset: 0x000A29E4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ItemBorderBackground itemBorderBackground = (ItemBorderBackground)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "storedColor")
					{
						itemBorderBackground.storedColor = reader.Read<Color>(ES3Type_Color.Instance);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000DFF RID: 3583
		public static ES3Type Instance;
	}
}
