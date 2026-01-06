using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000232 RID: 562
	public class SerializeSettings
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00053BDB File Offset: 0x00051DDB
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

		// Token: 0x04000A62 RID: 2658
		public bool nodes = true;

		// Token: 0x04000A63 RID: 2659
		[Obsolete("There is no support for pretty printing the json anymore")]
		public bool prettyPrint;

		// Token: 0x04000A64 RID: 2660
		public bool editorSettings;
	}
}
