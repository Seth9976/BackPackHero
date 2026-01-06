using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001E RID: 30
	[Preserve]
	public class ES3QueueType : ES3CollectionType
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000763C File Offset: 0x0000583C
		public ES3QueueType(Type type)
			: base(type)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007648 File Offset: 0x00005848
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			IEnumerable enumerable = (ICollection)obj;
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			int num = 0;
			foreach (object obj2 in enumerable)
			{
				writer.StartWriteCollectionItem(num);
				writer.Write(obj2, this.elementType, memberReferenceMode);
				writer.EndWriteCollectionItem(num);
				num++;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000076CC File Offset: 0x000058CC
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000076D8 File Offset: 0x000058D8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			Queue<T> queue = (Queue<T>)obj;
			foreach (T t in queue)
			{
				num++;
				if (!reader.StartReadCollectionItem())
				{
					break;
				}
				reader.ReadInto<T>(t, this.elementType);
				if (reader.EndReadCollectionItem())
				{
					break;
				}
				if (num == queue.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != queue.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007794 File Offset: 0x00005994
		public override object Read(ES3Reader reader)
		{
			IList list = (IList)ES3Reflection.CreateInstance(ES3Reflection.MakeGenericType(typeof(List<>), this.elementType.type));
			if (reader.StartReadCollection())
			{
				return null;
			}
			while (reader.StartReadCollectionItem())
			{
				list.Add(reader.Read<object>(this.elementType));
				if (reader.EndReadCollectionItem())
				{
					break;
				}
			}
			reader.EndReadCollection();
			return ES3Reflection.CreateInstance(this.type, new object[] { list });
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007810 File Offset: 0x00005A10
		public override void ReadInto(ES3Reader reader, object obj)
		{
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			ICollection collection = (ICollection)obj;
			foreach (object obj2 in collection)
			{
				num++;
				if (!reader.StartReadCollectionItem())
				{
					break;
				}
				reader.ReadInto<object>(obj2, this.elementType);
				if (reader.EndReadCollectionItem())
				{
					break;
				}
				if (num == collection.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != collection.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}
