using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004F RID: 79
	[Preserve]
	internal class ES3ReflectedObjectType : ES3ObjectType
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000961F File Offset: 0x0000781F
		public ES3ReflectedObjectType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009636 File Offset: 0x00007836
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009640 File Offset: 0x00007840
		protected override object ReadObject<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			base.ReadProperties(reader, obj);
			return obj;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009663 File Offset: 0x00007863
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}
