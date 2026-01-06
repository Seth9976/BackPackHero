using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000063 RID: 99
	public sealed class GraphPointerException : Exception
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000883D File Offset: 0x00006A3D
		public GraphPointer pointer { get; }

		// Token: 0x06000344 RID: 836 RVA: 0x00008845 File Offset: 0x00006A45
		public GraphPointerException(string message, GraphPointer pointer)
			: base(message + "\n" + ((pointer != null) ? pointer.ToString() : null))
		{
			this.pointer = pointer;
		}
	}
}
