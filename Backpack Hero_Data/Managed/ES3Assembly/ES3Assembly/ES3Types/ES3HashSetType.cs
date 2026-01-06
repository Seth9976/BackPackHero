using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001C RID: 28
	[Preserve]
	public class ES3HashSetType : ES3CollectionType
	{
		// Token: 0x060001EE RID: 494 RVA: 0x000072C6 File Offset: 0x000054C6
		public ES3HashSetType(Type type)
			: base(type)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000072D0 File Offset: 0x000054D0
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			if (obj == null)
			{
				writer.WriteNull();
				return;
			}
			IEnumerable enumerable = (IEnumerable)obj;
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			int num = 0;
			foreach (object obj2 in enumerable)
			{
				num++;
			}
			int num2 = 0;
			foreach (object obj3 in enumerable)
			{
				writer.StartWriteCollectionItem(num2);
				writer.Write(obj3, this.elementType, memberReferenceMode);
				writer.EndWriteCollectionItem(num2);
				num2++;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000073A4 File Offset: 0x000055A4
		public override object Read<T>(ES3Reader reader)
		{
			object obj = this.Read(reader);
			if (obj == null)
			{
				return default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000073D8 File Offset: 0x000055D8
		public override object Read(ES3Reader reader)
		{
			Type type = ES3Reflection.GetGenericArguments(this.type)[0];
			IList list = (IList)ES3Reflection.CreateInstance(ES3Reflection.MakeGenericType(typeof(List<>), type));
			if (!reader.StartReadCollection())
			{
				while (reader.StartReadCollectionItem())
				{
					list.Add(reader.Read<object>(this.elementType));
					if (reader.EndReadCollectionItem())
					{
						break;
					}
				}
				reader.EndReadCollection();
			}
			return ES3Reflection.CreateInstance(this.type, new object[] { list });
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007454 File Offset: 0x00005654
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000745E File Offset: 0x0000565E
		public override void ReadInto(ES3Reader reader, object obj)
		{
			throw new NotImplementedException("Cannot use LoadInto/ReadInto with HashSet because HashSets do not maintain the order of elements");
		}
	}
}
