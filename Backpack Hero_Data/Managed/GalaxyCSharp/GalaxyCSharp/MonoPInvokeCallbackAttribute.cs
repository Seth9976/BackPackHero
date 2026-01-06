using System;

// Token: 0x02000198 RID: 408
public class MonoPInvokeCallbackAttribute : Attribute
{
	// Token: 0x06000E89 RID: 3721 RVA: 0x0001CD9A File Offset: 0x0001AF9A
	public MonoPInvokeCallbackAttribute(Type t)
	{
		this.type = t;
	}

	// Token: 0x0400037E RID: 894
	private Type type;
}
