using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>The <see cref="T:Microsoft.SqlServer.Server.TriggerAction" /> enumeration is used by the <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> class to indicate what action fired the trigger. </summary>
	// Token: 0x020003CC RID: 972
	public enum TriggerAction
	{
		/// <summary>An invalid trigger action, one that is not exposed to the user, occurred.</summary>
		// Token: 0x04001C40 RID: 7232
		Invalid,
		/// <summary>An INSERT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C41 RID: 7233
		Insert,
		/// <summary>An UPDATE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C42 RID: 7234
		Update,
		/// <summary>A DELETE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C43 RID: 7235
		Delete,
		/// <summary>A CREATE TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C44 RID: 7236
		CreateTable = 21,
		/// <summary>An ALTER TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C45 RID: 7237
		AlterTable,
		/// <summary>A DROP TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C46 RID: 7238
		DropTable,
		/// <summary>A CREATE INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001C47 RID: 7239
		CreateIndex,
		/// <summary>An ALTER INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001C48 RID: 7240
		AlterIndex,
		/// <summary>A DROP INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x04001C49 RID: 7241
		DropIndex,
		/// <summary>A CREATE SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x04001C4A RID: 7242
		CreateSynonym = 34,
		/// <summary>A DROP SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x04001C4B RID: 7243
		DropSynonym = 36,
		/// <summary>Not available.</summary>
		// Token: 0x04001C4C RID: 7244
		CreateSecurityExpression = 31,
		/// <summary>Not available.</summary>
		// Token: 0x04001C4D RID: 7245
		DropSecurityExpression = 33,
		/// <summary>A CREATE VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001C4E RID: 7246
		CreateView = 41,
		/// <summary>An ALTER VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001C4F RID: 7247
		AlterView,
		/// <summary>A DROP VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x04001C50 RID: 7248
		DropView,
		/// <summary>A CREATE PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C51 RID: 7249
		CreateProcedure = 51,
		/// <summary>An ALTER PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C52 RID: 7250
		AlterProcedure,
		/// <summary>A DROP PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C53 RID: 7251
		DropProcedure,
		/// <summary>A CREATE FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C54 RID: 7252
		CreateFunction = 61,
		/// <summary>An ALTER FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C55 RID: 7253
		AlterFunction,
		/// <summary>A DROP FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C56 RID: 7254
		DropFunction,
		/// <summary>A CREATE TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C57 RID: 7255
		CreateTrigger = 71,
		/// <summary>An ALTER TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C58 RID: 7256
		AlterTrigger,
		/// <summary>A DROP TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C59 RID: 7257
		DropTrigger,
		/// <summary>A CREATE EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5A RID: 7258
		CreateEventNotification,
		/// <summary>A DROP EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5B RID: 7259
		DropEventNotification = 76,
		/// <summary>A CREATE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5C RID: 7260
		CreateType = 91,
		/// <summary>A DROP TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5D RID: 7261
		DropType = 93,
		/// <summary>A CREATE ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5E RID: 7262
		CreateAssembly = 101,
		/// <summary>An ALTER ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001C5F RID: 7263
		AlterAssembly,
		/// <summary>A DROP ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x04001C60 RID: 7264
		DropAssembly,
		/// <summary>A CREATE USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C61 RID: 7265
		CreateUser = 131,
		/// <summary>An ALTER USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C62 RID: 7266
		AlterUser,
		/// <summary>A DROP USER Transact-SQL statement was executed.</summary>
		// Token: 0x04001C63 RID: 7267
		DropUser,
		/// <summary>A CREATE ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C64 RID: 7268
		CreateRole,
		/// <summary>An ALTER ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C65 RID: 7269
		AlterRole,
		/// <summary>A DROP ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C66 RID: 7270
		DropRole,
		/// <summary>A CREATE APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C67 RID: 7271
		CreateAppRole,
		/// <summary>An ALTER APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C68 RID: 7272
		AlterAppRole,
		/// <summary>A DROP APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C69 RID: 7273
		DropAppRole,
		/// <summary>A CREATE SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6A RID: 7274
		CreateSchema = 141,
		/// <summary>An ALTER SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6B RID: 7275
		AlterSchema,
		/// <summary>A DROP SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6C RID: 7276
		DropSchema,
		/// <summary>A CREATE LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6D RID: 7277
		CreateLogin,
		/// <summary>An ALTER LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6E RID: 7278
		AlterLogin,
		/// <summary>A DROP LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x04001C6F RID: 7279
		DropLogin,
		/// <summary>A CREATE MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C70 RID: 7280
		CreateMsgType = 151,
		/// <summary>A DROP MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C71 RID: 7281
		DropMsgType = 153,
		/// <summary>A CREATE CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C72 RID: 7282
		CreateContract,
		/// <summary>A DROP CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C73 RID: 7283
		DropContract = 156,
		/// <summary>A CREATE QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C74 RID: 7284
		CreateQueue,
		/// <summary>An ALTER QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C75 RID: 7285
		AlterQueue,
		/// <summary>A DROP QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C76 RID: 7286
		DropQueue,
		/// <summary>A CREATE SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C77 RID: 7287
		CreateService = 161,
		/// <summary>An ALTER SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C78 RID: 7288
		AlterService,
		/// <summary>A DROP SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C79 RID: 7289
		DropService,
		/// <summary>A CREATE ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7A RID: 7290
		CreateRoute,
		/// <summary>An ALTER ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7B RID: 7291
		AlterRoute,
		/// <summary>A DROP ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7C RID: 7292
		DropRoute,
		/// <summary>A GRANT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7D RID: 7293
		GrantStatement,
		/// <summary>A DENY Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7E RID: 7294
		DenyStatement,
		/// <summary>A REVOKE Transact-SQL statement was executed.</summary>
		// Token: 0x04001C7F RID: 7295
		RevokeStatement,
		/// <summary>A GRANT OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C80 RID: 7296
		GrantObject,
		/// <summary>A DENY Object Permissions Transact-SQL statement was executed.</summary>
		// Token: 0x04001C81 RID: 7297
		DenyObject,
		/// <summary>A REVOKE OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x04001C82 RID: 7298
		RevokeObject,
		/// <summary>A CREATE_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001C83 RID: 7299
		CreateBinding = 174,
		/// <summary>An ALTER_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001C84 RID: 7300
		AlterBinding,
		/// <summary>A DROP_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x04001C85 RID: 7301
		DropBinding,
		/// <summary>A CREATE PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C86 RID: 7302
		CreatePartitionFunction = 191,
		/// <summary>An ALTER PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C87 RID: 7303
		AlterPartitionFunction,
		/// <summary>A DROP PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x04001C88 RID: 7304
		DropPartitionFunction,
		/// <summary>A CREATE PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C89 RID: 7305
		CreatePartitionScheme,
		/// <summary>An ALTER PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C8A RID: 7306
		AlterPartitionScheme,
		/// <summary>A DROP PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x04001C8B RID: 7307
		DropPartitionScheme
	}
}
