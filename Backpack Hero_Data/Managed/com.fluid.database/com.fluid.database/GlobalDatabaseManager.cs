using System;
using CleverCrow.Fluid.Utilities;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000C RID: 12
	public class GlobalDatabaseManager : Singleton<GlobalDatabaseManager>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023D4 File Offset: 0x000005D4
		public DatabaseInstance Database { get; } = new DatabaseInstance();

		// Token: 0x06000024 RID: 36 RVA: 0x000023DC File Offset: 0x000005DC
		public string Save()
		{
			return this.Database.Save();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023E9 File Offset: 0x000005E9
		public void Load(string save)
		{
			this.Database.Load(save);
		}
	}
}
