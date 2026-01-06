using System;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of MemberInfo.</summary>
	// Token: 0x020008AB RID: 2219
	[Flags]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor, representing a <see cref="T:System.Reflection.ConstructorInfo" /> member. Hexadecimal value of 0x01.</summary>
		// Token: 0x04002EBB RID: 11963
		Constructor = 1,
		/// <summary>Specifies that the member is an event, representing an <see cref="T:System.Reflection.EventInfo" /> member. Hexadecimal value of 0x02.</summary>
		// Token: 0x04002EBC RID: 11964
		Event = 2,
		/// <summary>Specifies that the member is a field, representing a <see cref="T:System.Reflection.FieldInfo" /> member. Hexadecimal value of 0x04.</summary>
		// Token: 0x04002EBD RID: 11965
		Field = 4,
		/// <summary>Specifies that the member is a method, representing a <see cref="T:System.Reflection.MethodInfo" /> member. Hexadecimal value of 0x08.</summary>
		// Token: 0x04002EBE RID: 11966
		Method = 8,
		/// <summary>Specifies that the member is a property, representing a <see cref="T:System.Reflection.PropertyInfo" /> member. Hexadecimal value of 0x10.</summary>
		// Token: 0x04002EBF RID: 11967
		Property = 16,
		/// <summary>Specifies that the member is a type, representing a <see cref="F:System.Reflection.MemberTypes.TypeInfo" /> member. Hexadecimal value of 0x20.</summary>
		// Token: 0x04002EC0 RID: 11968
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type. Hexadecimal value of 0x40.</summary>
		// Token: 0x04002EC1 RID: 11969
		Custom = 64,
		/// <summary>Specifies that the member is a nested type, extending <see cref="T:System.Reflection.MemberInfo" />.</summary>
		// Token: 0x04002EC2 RID: 11970
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x04002EC3 RID: 11971
		All = 191
	}
}
