using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a service that can generate unique names for objects.</summary>
	// Token: 0x020007A0 RID: 1952
	public interface INameCreationService
	{
		/// <summary>Creates a new name that is unique to all components in the specified container.</summary>
		/// <returns>A unique name for the data type.</returns>
		/// <param name="container">The container where the new object is added. </param>
		/// <param name="dataType">The data type of the object that receives the name. </param>
		// Token: 0x06003DB1 RID: 15793
		string CreateName(IContainer container, Type dataType);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <returns>true if the name is valid; otherwise, false.</returns>
		/// <param name="name">The name to validate. </param>
		// Token: 0x06003DB2 RID: 15794
		bool IsValidName(string name);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <param name="name">The name to validate. </param>
		// Token: 0x06003DB3 RID: 15795
		void ValidateName(string name);
	}
}
