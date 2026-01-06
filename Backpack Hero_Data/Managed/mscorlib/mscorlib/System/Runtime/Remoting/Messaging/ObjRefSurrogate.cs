using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000633 RID: 1587
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x06003BE6 RID: 15334 RVA: 0x000D096A File Offset: 0x000CEB6A
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000D0991 File Offset: 0x000CEB91
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}
	}
}
