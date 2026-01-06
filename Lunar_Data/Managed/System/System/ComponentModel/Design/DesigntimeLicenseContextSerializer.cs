using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for design-time license context serialization.</summary>
	// Token: 0x02000764 RID: 1892
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x0000219B File Offset: 0x0000039B
		private DesigntimeLicenseContextSerializer()
		{
		}

		/// <summary>Serializes the licenses within the specified design-time license context using the specified key and output stream.</summary>
		/// <param name="o">The stream to output to. </param>
		/// <param name="cryptoKey">The key to use for encryption. </param>
		/// <param name="context">A <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> indicating the license context. </param>
		// Token: 0x06003C63 RID: 15459 RVA: 0x000D832F File Offset: 0x000D652F
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			((IFormatter)new BinaryFormatter()).Serialize(o, new object[] { cryptoKey, context.savedLicenseKeys });
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x000D8350 File Offset: 0x000D6550
		internal static void Deserialize(Stream o, string cryptoKey, RuntimeLicenseContext context)
		{
			object obj = ((IFormatter)new BinaryFormatter()).Deserialize(o);
			if (obj is object[])
			{
				object[] array = (object[])obj;
				if (array[0] is string && (string)array[0] == cryptoKey)
				{
					context.savedLicenseKeys = (Hashtable)array[1];
				}
			}
		}
	}
}
