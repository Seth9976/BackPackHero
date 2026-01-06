using System;

namespace ES3Internal
{
	// Token: 0x020000D7 RID: 215
	public class ES3Member
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00021DF5 File Offset: 0x0001FFF5
		public ES3Member(string name, Type type, bool isProperty)
		{
			this.name = name;
			this.type = type;
			this.isProperty = isProperty;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00021E12 File Offset: 0x00020012
		public ES3Member(ES3Reflection.ES3ReflectedMember reflectedMember)
		{
			this.reflectedMember = reflectedMember;
			this.name = reflectedMember.Name;
			this.type = reflectedMember.MemberType;
			this.isProperty = reflectedMember.isProperty;
			this.useReflection = true;
		}

		// Token: 0x04000130 RID: 304
		public string name;

		// Token: 0x04000131 RID: 305
		public Type type;

		// Token: 0x04000132 RID: 306
		public bool isProperty;

		// Token: 0x04000133 RID: 307
		public ES3Reflection.ES3ReflectedMember reflectedMember;

		// Token: 0x04000134 RID: 308
		public bool useReflection;
	}
}
