using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x02000687 RID: 1671
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">true if the object is immutable; otherwise, false. </param>
		// Token: 0x060035A1 RID: 13729 RVA: 0x000BF0C7 File Offset: 0x000BD2C7
		public ImmutableObjectAttribute(bool immutable)
		{
			this.Immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>true if the object is immutable; otherwise, false.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000BF0D6 File Offset: 0x000BD2D6
		public bool Immutable { get; }

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x060035A3 RID: 13731 RVA: 0x000BF0E0 File Offset: 0x000BD2E0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			bool? flag = ((immutableObjectAttribute != null) ? new bool?(immutableObjectAttribute.Immutable) : null);
			bool immutable = this.Immutable;
			return (flag.GetValueOrDefault() == immutable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x060035A4 RID: 13732 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x060035A5 RID: 13733 RVA: 0x000BF12C File Offset: 0x000BD32C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This static field is read-only.</summary>
		// Token: 0x04002028 RID: 8232
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This static field is read-only.</summary>
		// Token: 0x04002029 RID: 8233
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x0400202A RID: 8234
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;
	}
}
