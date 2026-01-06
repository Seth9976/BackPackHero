using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000036 RID: 54
	// (Invoke) Token: 0x060001C8 RID: 456
	internal delegate bool ElementTryParser<T>(Lexer lexer, out T parsedValue, out Token token);
}
