using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x02000754 RID: 1876
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to. </param>
		/// <param name="commandID">The numeric identifier of this menu command. </param>
		// Token: 0x06003C08 RID: 15368 RVA: 0x000D7C38 File Offset: 0x000D5E38
		public CommandID(Guid menuGroup, int commandID)
		{
			this.Guid = menuGroup;
			this.ID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000D7C4E File Offset: 0x000D5E4E
		public virtual int ID { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <returns>true if the specified object is equivalent to this one; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x06003C0A RID: 15370 RVA: 0x000D7C58 File Offset: 0x000D5E58
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.Guid.Equals(this.Guid) && commandID.ID == this.ID;
		}

		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06003C0B RID: 15371 RVA: 0x000D7C9C File Offset: 0x000D5E9C
		public override int GetHashCode()
		{
			return (this.Guid.GetHashCode() << 2) | this.ID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x000D7CC6 File Offset: 0x000D5EC6
		public virtual Guid Guid { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier. </returns>
		// Token: 0x06003C0D RID: 15373 RVA: 0x000D7CD0 File Offset: 0x000D5ED0
		public override string ToString()
		{
			return this.Guid.ToString() + " : " + this.ID.ToString(CultureInfo.CurrentCulture);
		}
	}
}
