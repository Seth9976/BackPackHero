using System;
using System.Collections;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001D RID: 29
	[Preserve]
	public class ES3ListType : ES3CollectionType
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000746A File Offset: 0x0000566A
		public ES3ListType(Type type)
			: base(type)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007473 File Offset: 0x00005673
		public ES3ListType(Type type, ES3Type elementType)
			: base(type, elementType)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007480 File Offset: 0x00005680
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			if (obj == null)
			{
				writer.WriteNull();
				return;
			}
			IEnumerable enumerable = (IList)obj;
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

		// Token: 0x060001F7 RID: 503 RVA: 0x0000750C File Offset: 0x0000570C
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007515 File Offset: 0x00005715
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadICollectionInto(reader, (ICollection)obj, this.elementType);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000752C File Offset: 0x0000572C
		public override object Read(ES3Reader reader)
		{
			IList list = (IList)ES3Reflection.CreateInstance(this.type);
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
			return list;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007580 File Offset: 0x00005780
		public override void ReadInto(ES3Reader reader, object obj)
		{
			IList list = (IList)obj;
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			int num = 0;
			foreach (object obj2 in list)
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
				if (num == list.Count)
				{
					throw new IndexOutOfRangeException("The collection we are loading is longer than the collection provided as a parameter.");
				}
			}
			if (num != list.Count)
			{
				throw new IndexOutOfRangeException("The collection we are loading is shorter than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}
