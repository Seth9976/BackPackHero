using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000053 RID: 83
	[Preserve]
	internal class ES3ReflectedValueType : ES3Type
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00009922 File Offset: 0x00007B22
		public ES3ReflectedValueType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009939 File Offset: 0x00007B39
		public override void Write(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009944 File Offset: 0x00007B44
		public override object Read<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			if (obj == null)
			{
				string text = "Cannot create an instance of ";
				Type type = this.type;
				throw new NotSupportedException(text + ((type != null) ? type.ToString() : null) + ". However, you may be able to add support for it using a custom ES3Type file. For more information see: http://docs.moodkie.com/easy-save-3/es3-guides/controlling-serialization-using-es3types/");
			}
			return base.ReadProperties(reader, obj);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000998F File Offset: 0x00007B8F
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			throw new NotSupportedException("Cannot perform self-assigning load on a value type.");
		}
	}
}
