using System;

namespace System
{
	/// <summary>Indicates that the value of a static field is unique for each thread.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000191 RID: 401
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[Serializable]
	public class ThreadStaticAttribute : Attribute
	{
	}
}
