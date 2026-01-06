using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000373 RID: 883
	internal struct StyleValidationResult
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000838B8 File Offset: 0x00081AB8
		public bool success
		{
			get
			{
				return this.status == StyleValidationStatus.Ok;
			}
		}

		// Token: 0x04000E0A RID: 3594
		public StyleValidationStatus status;

		// Token: 0x04000E0B RID: 3595
		public string message;

		// Token: 0x04000E0C RID: 3596
		public string errorValue;

		// Token: 0x04000E0D RID: 3597
		public string hint;
	}
}
