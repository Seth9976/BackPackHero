using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a stack object that can be used by a serializer to make information available to nested serializers.</summary>
	// Token: 0x02000796 RID: 1942
	public sealed class ContextStack
	{
		/// <summary>Gets the current object on the stack.</summary>
		/// <returns>The current object on the stack, or null if no objects were pushed.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000D960E File Offset: 0x000D780E
		public object Current
		{
			get
			{
				if (this._contextStack != null && this._contextStack.Count > 0)
				{
					return this._contextStack[this._contextStack.Count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the object on the stack at the specified level.</summary>
		/// <returns>The object on the stack at the specified level, or null if no object exists at that level.</returns>
		/// <param name="level">The level of the object to retrieve on the stack. Level 0 is the top of the stack, level 1 is the next down, and so on. This level must be 0 or greater. If level is greater than the number of levels on the stack, it returns null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="level" /> is less than 0.</exception>
		// Token: 0x17000DFA RID: 3578
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				if (this._contextStack != null && level < this._contextStack.Count)
				{
					return this._contextStack[this._contextStack.Count - 1 - level];
				}
				return null;
			}
		}

		/// <summary>Gets the first object on the stack that inherits from or implements the specified type.</summary>
		/// <returns>The first object on the stack that inherits from or implements the specified type, or null if no object on the stack implements the type.</returns>
		/// <param name="type">A type to retrieve from the context stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x17000DFB RID: 3579
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this._contextStack != null)
				{
					int i = this._contextStack.Count;
					while (i > 0)
					{
						object obj = this._contextStack[--i];
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Appends an object to the end of the stack, rather than pushing it onto the top of the stack.</summary>
		/// <param name="context">A context object to append to the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06003D84 RID: 15748 RVA: 0x000D96E8 File Offset: 0x000D78E8
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this._contextStack == null)
			{
				this._contextStack = new ArrayList();
			}
			this._contextStack.Insert(0, context);
		}

		/// <summary>Removes the current object off of the stack, returning its value.</summary>
		/// <returns>The object removed from the stack; null if no objects are on the stack.</returns>
		// Token: 0x06003D85 RID: 15749 RVA: 0x000D9718 File Offset: 0x000D7918
		public object Pop()
		{
			object obj = null;
			if (this._contextStack != null && this._contextStack.Count > 0)
			{
				int num = this._contextStack.Count - 1;
				obj = this._contextStack[num];
				this._contextStack.RemoveAt(num);
			}
			return obj;
		}

		/// <summary>Pushes, or places, the specified object onto the stack.</summary>
		/// <param name="context">The context object to push onto the stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06003D86 RID: 15750 RVA: 0x000D9765 File Offset: 0x000D7965
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this._contextStack == null)
			{
				this._contextStack = new ArrayList();
			}
			this._contextStack.Add(context);
		}

		// Token: 0x040025FD RID: 9725
		private ArrayList _contextStack;
	}
}
