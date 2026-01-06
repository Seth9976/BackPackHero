using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007F3 RID: 2035
	public static class FormattableStringFactory
	{
		// Token: 0x060045FF RID: 17919 RVA: 0x000E57A7 File Offset: 0x000E39A7
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x020007F4 RID: 2036
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06004600 RID: 17920 RVA: 0x000E57CC File Offset: 0x000E39CC
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x06004601 RID: 17921 RVA: 0x000E57E2 File Offset: 0x000E39E2
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06004602 RID: 17922 RVA: 0x000E57EA File Offset: 0x000E39EA
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06004603 RID: 17923 RVA: 0x000E57F2 File Offset: 0x000E39F2
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x06004604 RID: 17924 RVA: 0x000E57FC File Offset: 0x000E39FC
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x06004605 RID: 17925 RVA: 0x000E5806 File Offset: 0x000E3A06
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x04002D3A RID: 11578
			private readonly string _format;

			// Token: 0x04002D3B RID: 11579
			private readonly object[] _arguments;
		}
	}
}
