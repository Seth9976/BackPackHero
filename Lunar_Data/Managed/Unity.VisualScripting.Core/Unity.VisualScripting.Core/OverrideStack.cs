using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200015B RID: 347
	public class OverrideStack<T>
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x0002804C File Offset: 0x0002624C
		public OverrideStack(T defaultValue)
		{
			this._value = defaultValue;
			this.getValue = () => this._value;
			this.setValue = delegate(T value)
			{
				this._value = value;
			};
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002808C File Offset: 0x0002628C
		public OverrideStack(Func<T> getValue, Action<T> setValue)
		{
			Ensure.That("getValue").IsNotNull<Func<T>>(getValue);
			Ensure.That("setValue").IsNotNull<Action<T>>(setValue);
			this.getValue = getValue;
			this.setValue = setValue;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000280D8 File Offset: 0x000262D8
		public OverrideStack(Func<T> getValue, Action<T> setValue, Action clearValue)
			: this(getValue, setValue)
		{
			Ensure.That("clearValue").IsNotNull<Action>(clearValue);
			this.clearValue = clearValue;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000280F9 File Offset: 0x000262F9
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00028106 File Offset: 0x00026306
		public T value
		{
			get
			{
				return this.getValue();
			}
			internal set
			{
				this.setValue(value);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00028114 File Offset: 0x00026314
		public OverrideLayer<T> Override(T item)
		{
			return new OverrideLayer<T>(this, item);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002811D File Offset: 0x0002631D
		public void BeginOverride(T item)
		{
			this.previous.Push(this.value);
			this.value = item;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00028138 File Offset: 0x00026338
		public void EndOverride()
		{
			if (this.previous.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.value = this.previous.Pop();
			if (this.previous.Count == 0)
			{
				Action action = this.clearValue;
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00028186 File Offset: 0x00026386
		public static implicit operator T(OverrideStack<T> stack)
		{
			Ensure.That("stack").IsNotNull<OverrideStack<T>>(stack);
			return stack.value;
		}

		// Token: 0x0400022F RID: 559
		private readonly Func<T> getValue;

		// Token: 0x04000230 RID: 560
		private readonly Action<T> setValue;

		// Token: 0x04000231 RID: 561
		private readonly Action clearValue;

		// Token: 0x04000232 RID: 562
		private T _value;

		// Token: 0x04000233 RID: 563
		private readonly Stack<T> previous = new Stack<T>();
	}
}
