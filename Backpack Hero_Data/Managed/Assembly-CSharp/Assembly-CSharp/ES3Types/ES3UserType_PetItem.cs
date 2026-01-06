using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001F7 RID: 503
	[Preserve]
	[ES3Properties(new string[] { })]
	public class ES3UserType_PetItem : ES3ComponentType
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x000A6E2D File Offset: 0x000A502D
		public ES3UserType_PetItem()
			: base(typeof(PetItem))
		{
			ES3UserType_PetItem.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000A6E4C File Offset: 0x000A504C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PetItem petItem = (PetItem)obj;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000A6E58 File Offset: 0x000A5058
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PetItem petItem = (PetItem)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x04000E23 RID: 3619
		public static ES3Type Instance;
	}
}
