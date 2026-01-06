using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Marks a property, event, or extender with a description. Visual designers can display this description when referencing the member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.All)]
	[Obsolete("DataSysDescriptionAttribute has been deprecated.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSysDescriptionAttribute" /> class using the specified description string.</summary>
		/// <param name="description">The description string. </param>
		// Token: 0x06000703 RID: 1795 RVA: 0x0001C5F2 File Offset: 0x0001A7F2
		[Obsolete("DataSysDescriptionAttribute has been deprecated.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DataSysDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the text for the description. </summary>
		/// <returns>The description string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001C5FB File Offset: 0x0001A7FB
		public override string Description
		{
			get
			{
				if (!this._replaced)
				{
					this._replaced = true;
					base.DescriptionValue = base.Description;
				}
				return base.Description;
			}
		}

		// Token: 0x04000567 RID: 1383
		private bool _replaced;
	}
}
