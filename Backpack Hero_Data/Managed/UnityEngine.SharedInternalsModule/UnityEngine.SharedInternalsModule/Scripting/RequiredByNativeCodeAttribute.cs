using System;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200002C RID: 44
	[AttributeUsage(1532, Inherited = false)]
	[VisibleToOtherModules]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00002078 File Offset: 0x00000278
		public RequiredByNativeCodeAttribute()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002686 File Offset: 0x00000886
		public RequiredByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002698 File Offset: 0x00000898
		public RequiredByNativeCodeAttribute(bool optional)
		{
			this.Optional = optional;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000026AA File Offset: 0x000008AA
		public RequiredByNativeCodeAttribute(string name, bool optional)
		{
			this.Name = name;
			this.Optional = optional;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000026C4 File Offset: 0x000008C4
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000026CC File Offset: 0x000008CC
		public string Name { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000026D5 File Offset: 0x000008D5
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000026DD File Offset: 0x000008DD
		public bool Optional { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000026E6 File Offset: 0x000008E6
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000026EE File Offset: 0x000008EE
		public bool GenerateProxy { get; set; }
	}
}
