using System;

namespace Pathfinding.Serialization
{
	// Token: 0x020000B3 RID: 179
	public class SerializeSettings
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0003569B File Offset: 0x0003389B
		public static SerializeSettings Settings
		{
			get
			{
				return new SerializeSettings
				{
					nodes = false
				};
			}
		}

		// Token: 0x040004BE RID: 1214
		public bool nodes = true;

		// Token: 0x040004BF RID: 1215
		[Obsolete("There is no support for pretty printing the json anymore")]
		public bool prettyPrint;

		// Token: 0x040004C0 RID: 1216
		public bool editorSettings;
	}
}
