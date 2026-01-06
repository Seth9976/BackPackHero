using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000050 RID: 80
	[Preserve]
	internal class ES3ReflectedScriptableObjectType : ES3ScriptableObjectType
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000966E File Offset: 0x0000786E
		public ES3ReflectedScriptableObjectType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009685 File Offset: 0x00007885
		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000968F File Offset: 0x0000788F
		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}
