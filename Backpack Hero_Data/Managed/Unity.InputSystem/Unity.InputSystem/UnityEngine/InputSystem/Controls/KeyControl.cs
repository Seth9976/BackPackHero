using System;
using System.Globalization;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000115 RID: 277
	public class KeyControl : ButtonControl
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0004C153 File Offset: 0x0004A353
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0004C15B File Offset: 0x0004A35B
		public Key keyCode { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0004C164 File Offset: 0x0004A364
		public int scanCode
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_ScanCode;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0004C174 File Offset: 0x0004A374
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

		// Token: 0x04000676 RID: 1654
		private int m_ScanCode;
	}
}
