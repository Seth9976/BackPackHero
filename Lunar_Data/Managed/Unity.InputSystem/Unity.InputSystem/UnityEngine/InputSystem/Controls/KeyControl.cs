using System;
using System.Globalization;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000115 RID: 277
	public class KeyControl : ButtonControl
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0004C107 File Offset: 0x0004A307
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0004C10F File Offset: 0x0004A30F
		public Key keyCode { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0004C118 File Offset: 0x0004A318
		public int scanCode
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_ScanCode;
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0004C128 File Offset: 0x0004A328
		protected override void RefreshConfiguration()
		{
			base.displayName = null;
			this.m_ScanCode = 0;
			QueryKeyNameCommand queryKeyNameCommand = QueryKeyNameCommand.Create(this.keyCode);
			if (base.device.ExecuteCommand<QueryKeyNameCommand>(ref queryKeyNameCommand) > 0L)
			{
				this.m_ScanCode = queryKeyNameCommand.scanOrKeyCode;
				string text = queryKeyNameCommand.ReadKeyName();
				if (string.IsNullOrEmpty(text))
				{
					base.displayName = text;
					return;
				}
				TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
				string text2 = textInfo.ToLower(text);
				if (string.IsNullOrEmpty(text2))
				{
					base.displayName = text;
					return;
				}
				base.displayName = textInfo.ToTitleCase(text2);
			}
		}

		// Token: 0x04000675 RID: 1653
		private int m_ScanCode;
	}
}
