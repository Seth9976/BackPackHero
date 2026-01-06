using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200001C RID: 28
	public abstract class RewriteRuleElementStream<T>
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00004D52 File Offset: 0x00003D52
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription)
		{
			this.elementDescription = elementDescription;
			this.adaptor = adaptor;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004D68 File Offset: 0x00003D68
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription, T oneElement)
			: this(adaptor, elementDescription)
		{
			this.Add(oneElement);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004D79 File Offset: 0x00003D79
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription, IList<T> elements)
			: this(adaptor, elementDescription)
		{
			this.singleElement = default(T);
			this.elements = elements;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004D98 File Offset: 0x00003D98
		[Obsolete("This constructor is for internal use only and might be phased out soon. Use instead the one with IList<T>.")]
		public RewriteRuleElementStream(ITreeAdaptor adaptor, string elementDescription, IList elements)
			: this(adaptor, elementDescription)
		{
			this.singleElement = default(T);
			this.elements = new List<T>();
			if (elements != null)
			{
				foreach (object obj in elements)
				{
					T t = (T)((object)obj);
					this.elements.Add(t);
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004E14 File Offset: 0x00003E14
		public void Add(T el)
		{
			if (el == null)
			{
				return;
			}
			if (this.elements != null)
			{
				this.elements.Add(el);
				return;
			}
			if (this.singleElement == null)
			{
				this.singleElement = el;
				return;
			}
			this.elements = new List<T>(5);
			this.elements.Add(this.singleElement);
			this.singleElement = default(T);
			this.elements.Add(el);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004E89 File Offset: 0x00003E89
		public virtual void Reset()
		{
			this.cursor = 0;
			this.dirty = true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004E99 File Offset: 0x00003E99
		public bool HasNext()
		{
			return (this.singleElement != null && this.cursor < 1) || (this.elements != null && this.cursor < this.elements.Count);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004ED0 File Offset: 0x00003ED0
		public virtual object NextTree()
		{
			return this._Next();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004ED8 File Offset: 0x00003ED8
		protected object _Next()
		{
			int count = this.Count;
			if (count == 0)
			{
				throw new RewriteEmptyStreamException(this.elementDescription);
			}
			if (this.cursor >= count)
			{
				if (count == 1)
				{
					return this.ToTree(this.singleElement);
				}
				throw new RewriteCardinalityException(this.elementDescription);
			}
			else
			{
				if (this.singleElement != null)
				{
					this.cursor++;
					return this.ToTree(this.singleElement);
				}
				return this.ToTree(this.elements[this.cursor++]);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004F6B File Offset: 0x00003F6B
		protected virtual object ToTree(T el)
		{
			return el;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00004F73 File Offset: 0x00003F73
		public int Count
		{
			get
			{
				if (this.singleElement != null)
				{
					return 1;
				}
				if (this.elements != null)
				{
					return this.elements.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004F99 File Offset: 0x00003F99
		[Obsolete("Please use property Count instead.")]
		public int Size()
		{
			return this.Count;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004FA1 File Offset: 0x00003FA1
		public string Description
		{
			get
			{
				return this.elementDescription;
			}
		}

		// Token: 0x04000061 RID: 97
		protected int cursor;

		// Token: 0x04000062 RID: 98
		protected T singleElement;

		// Token: 0x04000063 RID: 99
		protected IList<T> elements;

		// Token: 0x04000064 RID: 100
		protected bool dirty;

		// Token: 0x04000065 RID: 101
		protected string elementDescription;

		// Token: 0x04000066 RID: 102
		protected ITreeAdaptor adaptor;
	}
}
