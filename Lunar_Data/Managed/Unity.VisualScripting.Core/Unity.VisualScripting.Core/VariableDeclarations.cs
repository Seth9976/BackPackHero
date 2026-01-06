using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200016F RID: 367
	[SerializationVersion("A", new Type[] { })]
	public sealed class VariableDeclarations : IEnumerable<VariableDeclaration>, IEnumerable, ISpecifiesCloner
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00029585 File Offset: 0x00027785
		public VariableDeclarations()
		{
			this.collection = new VariableDeclarationCollection();
		}

		// Token: 0x170001C5 RID: 453
		public object this[[InspectorVariableName(ActionDirection.Any)] string variable]
		{
			get
			{
				return this.Get(variable);
			}
			set
			{
				this.Set(variable, value);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000295AC File Offset: 0x000277AC
		public void Set([InspectorVariableName(ActionDirection.Set)] string variable, object value)
		{
			if (string.IsNullOrEmpty(variable))
			{
				return;
			}
			VariableDeclaration variableDeclaration;
			if (this.collection.TryGetValue(variable, out variableDeclaration))
			{
				if (variableDeclaration.value != value)
				{
					variableDeclaration.value = value;
					Action onVariableChanged = this.OnVariableChanged;
					if (onVariableChanged == null)
					{
						return;
					}
					onVariableChanged();
					return;
				}
			}
			else
			{
				this.collection.Add(new VariableDeclaration(variable, value));
				Action onVariableChanged2 = this.OnVariableChanged;
				if (onVariableChanged2 == null)
				{
					return;
				}
				onVariableChanged2();
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00029618 File Offset: 0x00027818
		public object Get([InspectorVariableName(ActionDirection.Get)] string variable)
		{
			if (string.IsNullOrEmpty(variable))
			{
				throw new ArgumentException("No variable name specified.", "variable");
			}
			VariableDeclaration variableDeclaration;
			if (this.collection.TryGetValue(variable, out variableDeclaration))
			{
				return variableDeclaration.value;
			}
			throw new InvalidOperationException("Variable not found: '" + variable + "'.");
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029669 File Offset: 0x00027869
		public T Get<T>([InspectorVariableName(ActionDirection.Get)] string variable)
		{
			return (T)((object)this.Get(variable, typeof(T)));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00029681 File Offset: 0x00027881
		public object Get([InspectorVariableName(ActionDirection.Get)] string variable, Type expectedType)
		{
			return ConversionUtility.Convert(this.Get(variable), expectedType);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029690 File Offset: 0x00027890
		public void Clear()
		{
			this.collection.Clear();
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002969D File Offset: 0x0002789D
		public bool IsDefined([InspectorVariableName(ActionDirection.Any)] string variable)
		{
			if (string.IsNullOrEmpty(variable))
			{
				throw new ArgumentException("No variable name specified.", "variable");
			}
			return this.collection.Contains(variable);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000296C4 File Offset: 0x000278C4
		public VariableDeclaration GetDeclaration(string variable)
		{
			VariableDeclaration variableDeclaration;
			if (this.collection.TryGetValue(variable, out variableDeclaration))
			{
				return variableDeclaration;
			}
			throw new InvalidOperationException("Variable not found: '" + variable + "'.");
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000296F8 File Offset: 0x000278F8
		public IEnumerator<VariableDeclaration> GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00029705 File Offset: 0x00027905
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.collection).GetEnumerator();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00029712 File Offset: 0x00027912
		ICloner ISpecifiesCloner.cloner
		{
			get
			{
				return VariableDeclarationsCloner.instance;
			}
		}

		// Token: 0x0400024C RID: 588
		public VariableKind Kind;

		// Token: 0x0400024D RID: 589
		[Serialize]
		[InspectorWide(true)]
		private VariableDeclarationCollection collection;

		// Token: 0x0400024E RID: 590
		internal Action OnVariableChanged;
	}
}
