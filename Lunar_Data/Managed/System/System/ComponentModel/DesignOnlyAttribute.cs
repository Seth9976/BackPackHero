using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x0200067C RID: 1660
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">true if a property can be set only at design time; false if the property can be set at design time and at run time. </param>
		// Token: 0x0600356B RID: 13675 RVA: 0x000BECAA File Offset: 0x000BCEAA
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.IsDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>true if a property can be set only at design time; otherwise, false.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000BECB9 File Offset: 0x000BCEB9
		public bool IsDesignOnly { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600356D RID: 13677 RVA: 0x000BECC4 File Offset: 0x000BCEC4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			bool? flag = ((designOnlyAttribute != null) ? new bool?(designOnlyAttribute.IsDesignOnly) : null);
			bool isDesignOnly = this.IsDesignOnly;
			return (flag.GetValueOrDefault() == isDesignOnly) & (flag != null);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000BED10 File Offset: 0x000BCF10
		public override int GetHashCode()
		{
			return this.IsDesignOnly.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600356F RID: 13679 RVA: 0x000BED2B File Offset: 0x000BCF2B
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		/// <summary>Specifies that a property can be set only at design time. This static field is read-only.</summary>
		// Token: 0x04002010 RID: 8208
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This static field is read-only.</summary>
		// Token: 0x04002011 RID: 8209
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04002012 RID: 8210
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
