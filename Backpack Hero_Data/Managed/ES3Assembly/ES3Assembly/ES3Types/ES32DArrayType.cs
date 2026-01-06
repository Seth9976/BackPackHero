using System;
using System.Collections.Generic;
using ES3Internal;

namespace ES3Types
{
	// Token: 0x02000017 RID: 23
	public class ES32DArrayType : ES3CollectionType
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00006844 File Offset: 0x00004A44
		public ES32DArrayType(Type type)
			: base(type)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006850 File Offset: 0x00004A50
		public override void Write(object obj, ES3Writer writer, ES3.ReferenceMode unityObjectType)
		{
			Array array = (Array)obj;
			if (this.elementType == null)
			{
				throw new ArgumentNullException("ES3Type argument cannot be null.");
			}
			for (int i = 0; i < array.GetLength(0); i++)
			{
				writer.StartWriteCollectionItem(i);
				writer.StartWriteCollection();
				for (int j = 0; j < array.GetLength(1); j++)
				{
					writer.StartWriteCollectionItem(j);
					writer.Write(array.GetValue(i, j), this.elementType, unityObjectType);
					writer.EndWriteCollectionItem(j);
				}
				writer.EndWriteCollection();
				writer.EndWriteCollectionItem(i);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000068D8 File Offset: 0x00004AD8
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000068E4 File Offset: 0x00004AE4
		public override object Read(ES3Reader reader)
		{
			if (reader.StartReadCollection())
			{
				return null;
			}
			List<object> list = new List<object>();
			int num = 0;
			while (reader.StartReadCollectionItem())
			{
				this.ReadICollection<object>(reader, list, this.elementType);
				num++;
				if (reader.EndReadCollectionItem())
				{
					break;
				}
			}
			int num2 = list.Count / num;
			Array array = ES3Reflection.ArrayCreateInstance(this.elementType.type, new int[] { num, num2 });
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					array.SetValue(list[i * num2 + j], i, j);
				}
			}
			return array;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006986 File Offset: 0x00004B86
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006990 File Offset: 0x00004B90
		public override void ReadInto(ES3Reader reader, object obj)
		{
			Array array = (Array)obj;
			if (reader.StartReadCollection())
			{
				throw new NullReferenceException("The Collection we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			bool flag = false;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				bool flag2 = false;
				if (!reader.StartReadCollectionItem())
				{
					throw new IndexOutOfRangeException("The collection we are loading is smaller than the collection provided as a parameter.");
				}
				reader.StartReadCollection();
				for (int j = 0; j < array.GetLength(1); j++)
				{
					if (!reader.StartReadCollectionItem())
					{
						throw new IndexOutOfRangeException("The collection we are loading is smaller than the collection provided as a parameter.");
					}
					reader.ReadInto<object>(array.GetValue(i, j), this.elementType);
					flag2 = reader.EndReadCollectionItem();
				}
				if (!flag2)
				{
					throw new IndexOutOfRangeException("The collection we are loading is larger than the collection provided as a parameter.");
				}
				reader.EndReadCollection();
				flag = reader.EndReadCollectionItem();
			}
			if (!flag)
			{
				throw new IndexOutOfRangeException("The collection we are loading is larger than the collection provided as a parameter.");
			}
			reader.EndReadCollection();
		}
	}
}
