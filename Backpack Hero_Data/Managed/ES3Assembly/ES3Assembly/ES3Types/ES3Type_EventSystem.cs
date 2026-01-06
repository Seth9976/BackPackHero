using System;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000058 RID: 88
	[Preserve]
	public class ES3Type_EventSystem : ES3ComponentType
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public ES3Type_EventSystem()
			: base(typeof(EventSystem))
		{
			ES3Type_EventSystem.Instance = this;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AB14 File Offset: 0x00008D14
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AB18 File Offset: 0x00008D18
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x0400008A RID: 138
		public static ES3Type Instance;
	}
}
