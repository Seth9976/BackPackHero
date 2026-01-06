using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BD RID: 957
	internal sealed class BinarySerializeSerializer : Serializer
	{
		// Token: 0x06002ED4 RID: 11988 RVA: 0x000CAD90 File Offset: 0x000C8F90
		internal BinarySerializeSerializer(Type t)
			: base(t)
		{
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000CAD9C File Offset: 0x000C8F9C
		public override void Serialize(Stream s, object o)
		{
			BinaryWriter binaryWriter = new BinaryWriter(s);
			((IBinarySerialize)o).Write(binaryWriter);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000CADBC File Offset: 0x000C8FBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Deserialize(Stream s)
		{
			object obj = Activator.CreateInstance(this._type);
			BinaryReader binaryReader = new BinaryReader(s);
			((IBinarySerialize)obj).Read(binaryReader);
			return obj;
		}
	}
}
