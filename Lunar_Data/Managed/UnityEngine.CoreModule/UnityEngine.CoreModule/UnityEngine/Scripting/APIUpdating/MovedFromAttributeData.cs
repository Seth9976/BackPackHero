using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x020002DD RID: 733
	internal struct MovedFromAttributeData
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x00030A88 File Offset: 0x0002EC88
		public void Set(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.className = sourceClassName;
			this.classHasChanged = this.className != null;
			this.nameSpace = sourceNamespace;
			this.nameSpaceHasChanged = this.nameSpace != null;
			this.assembly = sourceAssembly;
			this.assemblyHasChanged = this.assembly != null;
			this.autoUdpateAPI = autoUpdateAPI;
		}

		// Token: 0x040009D0 RID: 2512
		public string className;

		// Token: 0x040009D1 RID: 2513
		public string nameSpace;

		// Token: 0x040009D2 RID: 2514
		public string assembly;

		// Token: 0x040009D3 RID: 2515
		public bool classHasChanged;

		// Token: 0x040009D4 RID: 2516
		public bool nameSpaceHasChanged;

		// Token: 0x040009D5 RID: 2517
		public bool assemblyHasChanged;

		// Token: 0x040009D6 RID: 2518
		public bool autoUdpateAPI;
	}
}
