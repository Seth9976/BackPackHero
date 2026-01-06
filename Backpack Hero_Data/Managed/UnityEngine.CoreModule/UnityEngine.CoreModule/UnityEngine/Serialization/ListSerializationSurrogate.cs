using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x020002CE RID: 718
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x06001DC8 RID: 7624 RVA: 0x0003066C File Offset: 0x0002E86C
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000306B4 File Offset: 0x0002E8B4
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			bool flag = @int == 0;
			object obj2;
			if (flag)
			{
				obj2 = list;
			}
			else
			{
				IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
				for (int i = 0; i < @int; i++)
				{
					bool flag2 = !enumerator.MoveNext();
					if (flag2)
					{
						throw new InvalidOperationException();
					}
					list.Add(enumerator.Current);
				}
				obj2 = list;
			}
			return obj2;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00030750 File Offset: 0x0002E950
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040009AF RID: 2479
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}
