using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003E RID: 62
	internal abstract class HeaderInfo
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00008A8C File Offset: 0x00006C8C
		protected HeaderInfo(string name, HttpHeaderKind headerKind)
		{
			this.Name = name;
			this.HeaderKind = headerKind;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008AA2 File Offset: 0x00006CA2
		public static HeaderInfo CreateSingle<T>(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind, Func<object, string> toString = null)
		{
			return new HeaderInfo.HeaderTypeInfo<T, object>(name, parser, headerKind)
			{
				CustomToString = toString
			};
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008AB3 File Offset: 0x00006CB3
		public static HeaderInfo CreateMulti<T>(string name, TryParseListDelegate<T> elementParser, HttpHeaderKind headerKind, int minimalCount = 1, string separator = ", ") where T : class
		{
			return new HeaderInfo.CollectionHeaderTypeInfo<T, T>(name, elementParser, headerKind, minimalCount, separator);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public object CreateCollection(HttpHeaders headers)
		{
			return this.CreateCollection(headers, this);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008ACA File Offset: 0x00006CCA
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00008AD2 File Offset: 0x00006CD2
		public Func<object, string> CustomToString { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008ADB File Offset: 0x00006CDB
		public virtual string Separator
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000226 RID: 550
		public abstract void AddToCollection(object collection, object value);

		// Token: 0x06000227 RID: 551
		protected abstract object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo);

		// Token: 0x06000228 RID: 552
		public abstract List<string> ToStringCollection(object collection);

		// Token: 0x06000229 RID: 553
		public abstract bool TryParse(string value, out object result);

		// Token: 0x040000F4 RID: 244
		public bool AllowsMany;

		// Token: 0x040000F5 RID: 245
		public readonly HttpHeaderKind HeaderKind;

		// Token: 0x040000F6 RID: 246
		public readonly string Name;

		// Token: 0x0200003F RID: 63
		private class HeaderTypeInfo<T, U> : HeaderInfo where U : class
		{
			// Token: 0x0600022A RID: 554 RVA: 0x00008AE2 File Offset: 0x00006CE2
			public HeaderTypeInfo(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind)
				: base(name, headerKind)
			{
				this.parser = parser;
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00008AF4 File Offset: 0x00006CF4
			public override void AddToCollection(object collection, object value)
			{
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				List<U> list = value as List<U>;
				if (list != null)
				{
					httpHeaderValueCollection.AddRange(list);
					return;
				}
				httpHeaderValueCollection.Add((U)((object)value));
			}

			// Token: 0x0600022C RID: 556 RVA: 0x00008B26 File Offset: 0x00006D26
			protected override object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo)
			{
				return new HttpHeaderValueCollection<U>(headers, headerInfo);
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00008B30 File Offset: 0x00006D30
			public override List<string> ToStringCollection(object collection)
			{
				if (collection == null)
				{
					return null;
				}
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				if (httpHeaderValueCollection.Count != 0)
				{
					List<string> list = new List<string>();
					foreach (U u in httpHeaderValueCollection)
					{
						list.Add(u.ToString());
					}
					if (httpHeaderValueCollection.InvalidValues != null)
					{
						list.AddRange(httpHeaderValueCollection.InvalidValues);
					}
					return list;
				}
				if (httpHeaderValueCollection.InvalidValues == null)
				{
					return null;
				}
				return new List<string>(httpHeaderValueCollection.InvalidValues);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x00008BC8 File Offset: 0x00006DC8
			public override bool TryParse(string value, out object result)
			{
				T t;
				bool flag = this.parser(value, out t);
				result = t;
				return flag;
			}

			// Token: 0x040000F8 RID: 248
			private readonly TryParseDelegate<T> parser;
		}

		// Token: 0x02000040 RID: 64
		private class CollectionHeaderTypeInfo<T, U> : HeaderInfo.HeaderTypeInfo<T, U> where U : class
		{
			// Token: 0x0600022F RID: 559 RVA: 0x00008BEB File Offset: 0x00006DEB
			public CollectionHeaderTypeInfo(string name, TryParseListDelegate<T> parser, HttpHeaderKind headerKind, int minimalCount, string separator)
				: base(name, null, headerKind)
			{
				this.parser = parser;
				this.minimalCount = minimalCount;
				this.AllowsMany = true;
				this.separator = separator;
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000230 RID: 560 RVA: 0x00008C14 File Offset: 0x00006E14
			public override string Separator
			{
				get
				{
					return this.separator;
				}
			}

			// Token: 0x06000231 RID: 561 RVA: 0x00008C1C File Offset: 0x00006E1C
			public override bool TryParse(string value, out object result)
			{
				List<T> list;
				if (!this.parser(value, this.minimalCount, out list))
				{
					result = null;
					return false;
				}
				result = list;
				return true;
			}

			// Token: 0x040000F9 RID: 249
			private readonly int minimalCount;

			// Token: 0x040000FA RID: 250
			private readonly string separator;

			// Token: 0x040000FB RID: 251
			private readonly TryParseListDelegate<T> parser;
		}
	}
}
