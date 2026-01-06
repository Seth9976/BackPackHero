using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000007 RID: 7
	public struct Migrations
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002213 File Offset: 0x00000413
		public bool IsLegacyFormat
		{
			get
			{
				return (this.finishedMigrations & 1073741824) == 0;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		public int LegacyVersion
		{
			get
			{
				return this.finishedMigrations;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000222C File Offset: 0x0000042C
		public Migrations(int value)
		{
			this.finishedMigrations = value;
			this.allMigrations = 1073741824;
			this.ignore = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002247 File Offset: 0x00000447
		public bool TryMigrateFromLegacyFormat(out int legacyVersion)
		{
			legacyVersion = this.finishedMigrations;
			if (this.IsLegacyFormat)
			{
				this = new Migrations(1073741824);
				return true;
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000226C File Offset: 0x0000046C
		public void MarkMigrationFinished(int flag)
		{
			if (this.IsLegacyFormat)
			{
				throw new InvalidOperationException("Version must first be migrated to the bitfield format");
			}
			this.finishedMigrations |= flag;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000228F File Offset: 0x0000048F
		public bool AddAndMaybeRunMigration(int flag, bool filter = true)
		{
			if ((flag & 1073741824) != 0)
			{
				throw new ArgumentException("Cannot use the MIGRATE_TO_BITFIELD flag when adding a migration");
			}
			this.allMigrations |= flag;
			if (filter)
			{
				bool flag2 = (this.finishedMigrations & flag) != flag;
				this.MarkMigrationFinished(flag);
				return flag2;
			}
			return false;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CD File Offset: 0x000004CD
		public void IgnoreMigrationAttempt()
		{
			this.ignore = true;
		}

		// Token: 0x04000004 RID: 4
		internal int finishedMigrations;

		// Token: 0x04000005 RID: 5
		internal int allMigrations;

		// Token: 0x04000006 RID: 6
		internal bool ignore;

		// Token: 0x04000007 RID: 7
		private const int MIGRATE_TO_BITFIELD = 1073741824;
	}
}
