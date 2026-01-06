using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200082C RID: 2092
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x060042CB RID: 17099 RVA: 0x0002BA30 File Offset: 0x00029C30
		public IODescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x000BEC3B File Offset: 0x000BCE3B
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
