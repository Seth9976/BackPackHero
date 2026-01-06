using System;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001B0 RID: 432
	public struct fsVersionedType
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x000311AD File Offset: 0x0002F3AD
		public object Migrate(object ancestorInstance)
		{
			return Activator.CreateInstance(this.ModelType, new object[] { ancestorInstance });
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000311C4 File Offset: 0x0002F3C4
		public override string ToString()
		{
			string[] array = new string[7];
			array[0] = "fsVersionedType [ModelType=";
			int num = 1;
			Type modelType = this.ModelType;
			array[num] = ((modelType != null) ? modelType.ToString() : null);
			array[2] = ", VersionString=";
			array[3] = this.VersionString;
			array[4] = ", Ancestors.Length=";
			array[5] = this.Ancestors.Length.ToString();
			array[6] = "]";
			return string.Concat(array);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003122D File Offset: 0x0002F42D
		public static bool operator ==(fsVersionedType a, fsVersionedType b)
		{
			return a.ModelType == b.ModelType;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00031240 File Offset: 0x0002F440
		public static bool operator !=(fsVersionedType a, fsVersionedType b)
		{
			return a.ModelType != b.ModelType;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00031253 File Offset: 0x0002F453
		public override bool Equals(object obj)
		{
			return obj is fsVersionedType && this.ModelType == ((fsVersionedType)obj).ModelType;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00031275 File Offset: 0x0002F475
		public override int GetHashCode()
		{
			return this.ModelType.GetHashCode();
		}

		// Token: 0x040002CB RID: 715
		public fsVersionedType[] Ancestors;

		// Token: 0x040002CC RID: 716
		public string VersionString;

		// Token: 0x040002CD RID: 717
		public Type ModelType;
	}
}
