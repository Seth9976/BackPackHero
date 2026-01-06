using System;

namespace System.Data
{
	/// <summary>Specifies how a command string is interpreted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003C RID: 60
	public enum CommandType
	{
		/// <summary>An SQL text command. (Default.) </summary>
		// Token: 0x04000484 RID: 1156
		Text = 1,
		/// <summary>The name of a stored procedure.</summary>
		// Token: 0x04000485 RID: 1157
		StoredProcedure = 4,
		/// <summary>The name of a table.</summary>
		// Token: 0x04000486 RID: 1158
		TableDirect = 512
	}
}
