using System;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000052 RID: 82
	[Preserve]
	internal class ES3ReflectedUnityObjectType : ES3UnityObjectType
	{
		// Token: 0x06000298 RID: 664 RVA: 0x000098D2 File Offset: 0x00007AD2
		public ES3ReflectedUnityObjectType(Type type)
			: base(type)
		{
			this.isReflectedType = true;
			base.GetMembers(true);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000098E9 File Offset: 0x00007AE9
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			base.WriteProperties(obj, writer);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000098F4 File Offset: 0x00007AF4
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			object obj = ES3Reflection.CreateInstance(this.type);
			base.ReadProperties(reader, obj);
			return obj;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009917 File Offset: 0x00007B17
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			base.ReadProperties(reader, obj);
		}
	}
}
