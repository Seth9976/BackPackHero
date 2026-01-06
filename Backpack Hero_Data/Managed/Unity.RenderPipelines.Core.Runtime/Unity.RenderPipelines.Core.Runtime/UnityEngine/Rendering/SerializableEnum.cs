using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class SerializableEnum
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000F17C File Offset: 0x0000D37C
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000F1A6 File Offset: 0x0000D3A6
		public Enum value
		{
			get
			{
				object obj;
				if (Enum.TryParse(this.m_EnumType, this.m_EnumValueAsString, out obj))
				{
					return (Enum)obj;
				}
				return null;
			}
			set
			{
				this.m_EnumValueAsString = value.ToString();
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public SerializableEnum(Type enumType)
		{
			this.m_EnumType = enumType;
			this.m_EnumValueAsString = Enum.GetNames(enumType)[0];
		}

		// Token: 0x04000205 RID: 517
		[SerializeField]
		private string m_EnumValueAsString;

		// Token: 0x04000206 RID: 518
		[SerializeField]
		private Type m_EnumType;
	}
}
