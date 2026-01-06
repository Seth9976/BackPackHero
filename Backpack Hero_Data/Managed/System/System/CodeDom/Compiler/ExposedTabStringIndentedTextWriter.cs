using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200034E RID: 846
	internal sealed class ExposedTabStringIndentedTextWriter : IndentedTextWriter
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x00066796 File Offset: 0x00064996
		public ExposedTabStringIndentedTextWriter(TextWriter writer, string tabString)
			: base(writer, tabString)
		{
			this.TabString = tabString ?? "    ";
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000667B0 File Offset: 0x000649B0
		internal void InternalOutputTabs()
		{
			TextWriter innerWriter = base.InnerWriter;
			for (int i = 0; i < base.Indent; i++)
			{
				innerWriter.Write(this.TabString);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000667E1 File Offset: 0x000649E1
		internal string TabString { get; }
	}
}
