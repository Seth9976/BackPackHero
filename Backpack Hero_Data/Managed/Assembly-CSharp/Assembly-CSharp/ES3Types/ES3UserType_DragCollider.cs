using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001BF RID: 447
	[Preserve]
	[ES3Properties(new string[] { })]
	public class ES3UserType_DragCollider : ES3ComponentType
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x000A2705 File Offset: 0x000A0905
		public ES3UserType_DragCollider()
			: base(typeof(DragCollider))
		{
			ES3UserType_DragCollider.Instance = this;
			this.priority = 1;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000A2724 File Offset: 0x000A0924
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			DragCollider dragCollider = (DragCollider)obj;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000A2730 File Offset: 0x000A0930
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			DragCollider dragCollider = (DragCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x04000DEB RID: 3563
		public static ES3Type Instance;
	}
}
