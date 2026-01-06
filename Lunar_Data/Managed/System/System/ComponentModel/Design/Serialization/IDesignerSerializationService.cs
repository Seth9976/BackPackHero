using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can invoke serialization and deserialization.</summary>
	// Token: 0x0200079F RID: 1951
	public interface IDesignerSerializationService
	{
		/// <summary>Deserializes the specified serialization data object and returns a collection of objects represented by that data.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of objects rebuilt from the specified serialization data object.</returns>
		/// <param name="serializationData">An object consisting of serialized data. </param>
		// Token: 0x06003DAF RID: 15791
		ICollection Deserialize(object serializationData);

		/// <summary>Serializes the specified collection of objects and stores them in a serialization data object.</summary>
		/// <returns>An object that contains the serialized state of the specified collection of objects.</returns>
		/// <param name="objects">A collection of objects to serialize. </param>
		// Token: 0x06003DB0 RID: 15792
		object Serialize(ICollection objects);
	}
}
