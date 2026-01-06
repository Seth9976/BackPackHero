using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004E RID: 78
	[Preserve]
	internal class ES3ReflectedComponentType : ES3ComponentType
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000095F3 File Offset: 0x000077F3
		public ES3ReflectedComponentType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000960A File Offset: 0x0000780A
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009614 File Offset: 0x00007814
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}
