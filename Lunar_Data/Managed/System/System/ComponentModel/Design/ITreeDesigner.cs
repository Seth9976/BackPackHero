using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for building a set of related custom designers.</summary>
	// Token: 0x02000780 RID: 1920
	public interface ITreeDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets a collection of child designers.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" />, containing the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> child objects of the current designer. </returns>
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003CFB RID: 15611
		ICollection Children { get; }

		/// <summary>Gets the parent designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or null if there is no parent.</returns>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003CFC RID: 15612
		IDesigner Parent { get; }
	}
}
