using System;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x0200033F RID: 831
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04000E05 RID: 3589
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04000E06 RID: 3590
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the Shared keyword.</summary>
		// Token: 0x04000E07 RID: 3591
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04000E08 RID: 3592
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x04000E09 RID: 3593
		Const,
		/// <summary>A new member.</summary>
		// Token: 0x04000E0A RID: 3594
		New = 16,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x04000E0B RID: 3595
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x04000E0C RID: 3596
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x04000E0D RID: 3597
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x04000E0E RID: 3598
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x04000E0F RID: 3599
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x04000E10 RID: 3600
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x04000E11 RID: 3601
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x04000E12 RID: 3602
		AccessMask = 61440,
		/// <summary>A scope mask.</summary>
		// Token: 0x04000E13 RID: 3603
		ScopeMask = 15,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04000E14 RID: 3604
		VTableMask = 240
	}
}
