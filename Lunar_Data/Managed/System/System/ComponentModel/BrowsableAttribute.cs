using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x02000679 RID: 1657
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">true if a property or event can be modified at design time; otherwise, false. The default is true. </param>
		// Token: 0x06003558 RID: 13656 RVA: 0x000BEAD5 File Offset: 0x000BCCD5
		public BrowsableAttribute(bool browsable)
		{
			this.Browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>true if the object is browsable; otherwise, false.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000BEAE4 File Offset: 0x000BCCE4
		public bool Browsable { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x0600355A RID: 13658 RVA: 0x000BEAEC File Offset: 0x000BCCEC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			bool? flag = ((browsableAttribute != null) ? new bool?(browsableAttribute.Browsable) : null);
			bool browsable = this.Browsable;
			return (flag.GetValueOrDefault() == browsable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600355B RID: 13659 RVA: 0x000BEB38 File Offset: 0x000BCD38
		public override int GetHashCode()
		{
			return this.Browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600355C RID: 13660 RVA: 0x000BEB53 File Offset: 0x000BCD53
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		/// <summary>Specifies that a property or event can be modified at design time. This static field is read-only.</summary>
		// Token: 0x04002009 RID: 8201
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This static field is read-only.</summary>
		// Token: 0x0400200A RID: 8202
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This static field is read-only.</summary>
		// Token: 0x0400200B RID: 8203
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;
	}
}
