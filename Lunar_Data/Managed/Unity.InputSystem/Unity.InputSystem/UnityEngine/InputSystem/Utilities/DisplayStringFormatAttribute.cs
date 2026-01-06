using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012A RID: 298
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class DisplayStringFormatAttribute : Attribute
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x0004EBF4 File Offset: 0x0004CDF4
		public string formatString { get; set; }

		// Token: 0x06001085 RID: 4229 RVA: 0x0004EBFD File Offset: 0x0004CDFD
		public DisplayStringFormatAttribute(string formatString)
		{
			this.formatString = formatString;
		}
	}
}
