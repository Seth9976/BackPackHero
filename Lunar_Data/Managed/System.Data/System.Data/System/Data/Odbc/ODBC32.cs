using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200025A RID: 602
	public static class ODBC32
	{
		// Token: 0x06001B61 RID: 7009 RVA: 0x00087C82 File Offset: 0x00085E82
		internal static string RetcodeToString(ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.INVALID_HANDLE:
				return "INVALID_HANDLE";
			case ODBC32.RetCode.ERROR:
				break;
			case ODBC32.RetCode.SUCCESS:
				return "SUCCESS";
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return "SUCCESS_WITH_INFO";
			default:
				if (retcode == ODBC32.RetCode.NO_DATA)
				{
					return "NO_DATA";
				}
				break;
			}
			return "ERROR";
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00087CC1 File Offset: 0x00085EC1
		internal static OdbcErrorCollection GetDiagErrors(string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			OdbcErrorCollection odbcErrorCollection = new OdbcErrorCollection();
			ODBC32.GetDiagErrors(odbcErrorCollection, source, hrHandle, retcode);
			return odbcErrorCollection;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00087CD4 File Offset: 0x00085ED4
		internal static void GetDiagErrors(OdbcErrorCollection errors, string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				short num = 0;
				short num2 = 0;
				StringBuilder stringBuilder = new StringBuilder(1024);
				bool flag = true;
				while (flag)
				{
					num += 1;
					string text;
					int num3;
					retcode = hrHandle.GetDiagnosticRecord(num, out text, stringBuilder, out num3, out num2);
					if (ODBC32.RetCode.SUCCESS_WITH_INFO == retcode && stringBuilder.Capacity - 1 < (int)num2)
					{
						stringBuilder.Capacity = (int)(num2 + 1);
						retcode = hrHandle.GetDiagnosticRecord(num, out text, stringBuilder, out num3, out num2);
					}
					flag = retcode == ODBC32.RetCode.SUCCESS || retcode == ODBC32.RetCode.SUCCESS_WITH_INFO;
					if (flag)
					{
						errors.Add(new OdbcError(source, stringBuilder.ToString(), text, num3));
					}
				}
			}
		}

		// Token: 0x040013A1 RID: 5025
		internal const short SQL_COMMIT = 0;

		// Token: 0x040013A2 RID: 5026
		internal const short SQL_ROLLBACK = 1;

		// Token: 0x040013A3 RID: 5027
		internal static readonly IntPtr SQL_AUTOCOMMIT_OFF = ADP.PtrZero;

		// Token: 0x040013A4 RID: 5028
		internal static readonly IntPtr SQL_AUTOCOMMIT_ON = new IntPtr(1);

		// Token: 0x040013A5 RID: 5029
		private const int SIGNED_OFFSET = -20;

		// Token: 0x040013A6 RID: 5030
		private const int UNSIGNED_OFFSET = -22;

		// Token: 0x040013A7 RID: 5031
		internal const short SQL_ALL_TYPES = 0;

		// Token: 0x040013A8 RID: 5032
		internal static readonly IntPtr SQL_HANDLE_NULL = ADP.PtrZero;

		// Token: 0x040013A9 RID: 5033
		internal const int SQL_NULL_DATA = -1;

		// Token: 0x040013AA RID: 5034
		internal const int SQL_NO_TOTAL = -4;

		// Token: 0x040013AB RID: 5035
		internal const int SQL_DEFAULT_PARAM = -5;

		// Token: 0x040013AC RID: 5036
		internal const int COLUMN_NAME = 4;

		// Token: 0x040013AD RID: 5037
		internal const int COLUMN_TYPE = 5;

		// Token: 0x040013AE RID: 5038
		internal const int DATA_TYPE = 6;

		// Token: 0x040013AF RID: 5039
		internal const int COLUMN_SIZE = 8;

		// Token: 0x040013B0 RID: 5040
		internal const int DECIMAL_DIGITS = 10;

		// Token: 0x040013B1 RID: 5041
		internal const int NUM_PREC_RADIX = 11;

		// Token: 0x040013B2 RID: 5042
		internal static readonly IntPtr SQL_OV_ODBC3 = new IntPtr(3);

		// Token: 0x040013B3 RID: 5043
		internal const int SQL_NTS = -3;

		// Token: 0x040013B4 RID: 5044
		internal static readonly IntPtr SQL_CP_OFF = new IntPtr(0);

		// Token: 0x040013B5 RID: 5045
		internal static readonly IntPtr SQL_CP_ONE_PER_DRIVER = new IntPtr(1);

		// Token: 0x040013B6 RID: 5046
		internal static readonly IntPtr SQL_CP_ONE_PER_HENV = new IntPtr(2);

		// Token: 0x040013B7 RID: 5047
		internal const int SQL_CD_TRUE = 1;

		// Token: 0x040013B8 RID: 5048
		internal const int SQL_CD_FALSE = 0;

		// Token: 0x040013B9 RID: 5049
		internal const int SQL_DTC_DONE = 0;

		// Token: 0x040013BA RID: 5050
		internal const int SQL_IS_POINTER = -4;

		// Token: 0x040013BB RID: 5051
		internal const int SQL_IS_PTR = 1;

		// Token: 0x040013BC RID: 5052
		internal const int MAX_CONNECTION_STRING_LENGTH = 1024;

		// Token: 0x040013BD RID: 5053
		internal const short SQL_DIAG_SQLSTATE = 4;

		// Token: 0x040013BE RID: 5054
		internal const short SQL_RESULT_COL = 3;

		// Token: 0x0200025B RID: 603
		internal enum SQL_HANDLE : short
		{
			// Token: 0x040013C0 RID: 5056
			ENV = 1,
			// Token: 0x040013C1 RID: 5057
			DBC,
			// Token: 0x040013C2 RID: 5058
			STMT,
			// Token: 0x040013C3 RID: 5059
			DESC
		}

		// Token: 0x0200025C RID: 604
		public enum RETCODE
		{
			// Token: 0x040013C5 RID: 5061
			SUCCESS,
			// Token: 0x040013C6 RID: 5062
			SUCCESS_WITH_INFO,
			// Token: 0x040013C7 RID: 5063
			ERROR = -1,
			// Token: 0x040013C8 RID: 5064
			INVALID_HANDLE = -2,
			// Token: 0x040013C9 RID: 5065
			NO_DATA = 100
		}

		// Token: 0x0200025D RID: 605
		internal enum RetCode : short
		{
			// Token: 0x040013CB RID: 5067
			SUCCESS,
			// Token: 0x040013CC RID: 5068
			SUCCESS_WITH_INFO,
			// Token: 0x040013CD RID: 5069
			ERROR = -1,
			// Token: 0x040013CE RID: 5070
			INVALID_HANDLE = -2,
			// Token: 0x040013CF RID: 5071
			NO_DATA = 100
		}

		// Token: 0x0200025E RID: 606
		internal enum SQL_CONVERT : ushort
		{
			// Token: 0x040013D1 RID: 5073
			BIGINT = 53,
			// Token: 0x040013D2 RID: 5074
			BINARY,
			// Token: 0x040013D3 RID: 5075
			BIT,
			// Token: 0x040013D4 RID: 5076
			CHAR,
			// Token: 0x040013D5 RID: 5077
			DATE,
			// Token: 0x040013D6 RID: 5078
			DECIMAL,
			// Token: 0x040013D7 RID: 5079
			DOUBLE,
			// Token: 0x040013D8 RID: 5080
			FLOAT,
			// Token: 0x040013D9 RID: 5081
			INTEGER,
			// Token: 0x040013DA RID: 5082
			LONGVARCHAR,
			// Token: 0x040013DB RID: 5083
			NUMERIC,
			// Token: 0x040013DC RID: 5084
			REAL,
			// Token: 0x040013DD RID: 5085
			SMALLINT,
			// Token: 0x040013DE RID: 5086
			TIME,
			// Token: 0x040013DF RID: 5087
			TIMESTAMP,
			// Token: 0x040013E0 RID: 5088
			TINYINT,
			// Token: 0x040013E1 RID: 5089
			VARBINARY,
			// Token: 0x040013E2 RID: 5090
			VARCHAR,
			// Token: 0x040013E3 RID: 5091
			LONGVARBINARY
		}

		// Token: 0x0200025F RID: 607
		[Flags]
		internal enum SQL_CVT
		{
			// Token: 0x040013E5 RID: 5093
			CHAR = 1,
			// Token: 0x040013E6 RID: 5094
			NUMERIC = 2,
			// Token: 0x040013E7 RID: 5095
			DECIMAL = 4,
			// Token: 0x040013E8 RID: 5096
			INTEGER = 8,
			// Token: 0x040013E9 RID: 5097
			SMALLINT = 16,
			// Token: 0x040013EA RID: 5098
			FLOAT = 32,
			// Token: 0x040013EB RID: 5099
			REAL = 64,
			// Token: 0x040013EC RID: 5100
			DOUBLE = 128,
			// Token: 0x040013ED RID: 5101
			VARCHAR = 256,
			// Token: 0x040013EE RID: 5102
			LONGVARCHAR = 512,
			// Token: 0x040013EF RID: 5103
			BINARY = 1024,
			// Token: 0x040013F0 RID: 5104
			VARBINARY = 2048,
			// Token: 0x040013F1 RID: 5105
			BIT = 4096,
			// Token: 0x040013F2 RID: 5106
			TINYINT = 8192,
			// Token: 0x040013F3 RID: 5107
			BIGINT = 16384,
			// Token: 0x040013F4 RID: 5108
			DATE = 32768,
			// Token: 0x040013F5 RID: 5109
			TIME = 65536,
			// Token: 0x040013F6 RID: 5110
			TIMESTAMP = 131072,
			// Token: 0x040013F7 RID: 5111
			LONGVARBINARY = 262144,
			// Token: 0x040013F8 RID: 5112
			INTERVAL_YEAR_MONTH = 524288,
			// Token: 0x040013F9 RID: 5113
			INTERVAL_DAY_TIME = 1048576,
			// Token: 0x040013FA RID: 5114
			WCHAR = 2097152,
			// Token: 0x040013FB RID: 5115
			WLONGVARCHAR = 4194304,
			// Token: 0x040013FC RID: 5116
			WVARCHAR = 8388608,
			// Token: 0x040013FD RID: 5117
			GUID = 16777216
		}

		// Token: 0x02000260 RID: 608
		internal enum STMT : short
		{
			// Token: 0x040013FF RID: 5119
			CLOSE,
			// Token: 0x04001400 RID: 5120
			DROP,
			// Token: 0x04001401 RID: 5121
			UNBIND,
			// Token: 0x04001402 RID: 5122
			RESET_PARAMS
		}

		// Token: 0x02000261 RID: 609
		internal enum SQL_MAX
		{
			// Token: 0x04001404 RID: 5124
			NUMERIC_LEN = 16
		}

		// Token: 0x02000262 RID: 610
		internal enum SQL_IS
		{
			// Token: 0x04001406 RID: 5126
			POINTER = -4,
			// Token: 0x04001407 RID: 5127
			INTEGER = -6,
			// Token: 0x04001408 RID: 5128
			UINTEGER,
			// Token: 0x04001409 RID: 5129
			SMALLINT = -8
		}

		// Token: 0x02000263 RID: 611
		internal enum SQL_HC
		{
			// Token: 0x0400140B RID: 5131
			OFF,
			// Token: 0x0400140C RID: 5132
			ON
		}

		// Token: 0x02000264 RID: 612
		internal enum SQL_NB
		{
			// Token: 0x0400140E RID: 5134
			OFF,
			// Token: 0x0400140F RID: 5135
			ON
		}

		// Token: 0x02000265 RID: 613
		internal enum SQL_CA_SS
		{
			// Token: 0x04001411 RID: 5137
			BASE = 1200,
			// Token: 0x04001412 RID: 5138
			COLUMN_HIDDEN = 1211,
			// Token: 0x04001413 RID: 5139
			COLUMN_KEY,
			// Token: 0x04001414 RID: 5140
			VARIANT_TYPE = 1215,
			// Token: 0x04001415 RID: 5141
			VARIANT_SQL_TYPE,
			// Token: 0x04001416 RID: 5142
			VARIANT_SERVER_TYPE
		}

		// Token: 0x02000266 RID: 614
		internal enum SQL_SOPT_SS
		{
			// Token: 0x04001418 RID: 5144
			BASE = 1225,
			// Token: 0x04001419 RID: 5145
			HIDDEN_COLUMNS = 1227,
			// Token: 0x0400141A RID: 5146
			NOBROWSETABLE
		}

		// Token: 0x02000267 RID: 615
		internal enum SQL_TRANSACTION
		{
			// Token: 0x0400141C RID: 5148
			READ_UNCOMMITTED = 1,
			// Token: 0x0400141D RID: 5149
			READ_COMMITTED,
			// Token: 0x0400141E RID: 5150
			REPEATABLE_READ = 4,
			// Token: 0x0400141F RID: 5151
			SERIALIZABLE = 8,
			// Token: 0x04001420 RID: 5152
			SNAPSHOT = 32
		}

		// Token: 0x02000268 RID: 616
		internal enum SQL_PARAM
		{
			// Token: 0x04001422 RID: 5154
			INPUT = 1,
			// Token: 0x04001423 RID: 5155
			INPUT_OUTPUT,
			// Token: 0x04001424 RID: 5156
			OUTPUT = 4,
			// Token: 0x04001425 RID: 5157
			RETURN_VALUE
		}

		// Token: 0x02000269 RID: 617
		internal enum SQL_API : ushort
		{
			// Token: 0x04001427 RID: 5159
			SQLCOLUMNS = 40,
			// Token: 0x04001428 RID: 5160
			SQLEXECDIRECT = 11,
			// Token: 0x04001429 RID: 5161
			SQLGETTYPEINFO = 47,
			// Token: 0x0400142A RID: 5162
			SQLPROCEDURECOLUMNS = 66,
			// Token: 0x0400142B RID: 5163
			SQLPROCEDURES,
			// Token: 0x0400142C RID: 5164
			SQLSTATISTICS = 53,
			// Token: 0x0400142D RID: 5165
			SQLTABLES
		}

		// Token: 0x0200026A RID: 618
		internal enum SQL_DESC : short
		{
			// Token: 0x0400142F RID: 5167
			COUNT = 1001,
			// Token: 0x04001430 RID: 5168
			TYPE,
			// Token: 0x04001431 RID: 5169
			LENGTH,
			// Token: 0x04001432 RID: 5170
			OCTET_LENGTH_PTR,
			// Token: 0x04001433 RID: 5171
			PRECISION,
			// Token: 0x04001434 RID: 5172
			SCALE,
			// Token: 0x04001435 RID: 5173
			DATETIME_INTERVAL_CODE,
			// Token: 0x04001436 RID: 5174
			NULLABLE,
			// Token: 0x04001437 RID: 5175
			INDICATOR_PTR,
			// Token: 0x04001438 RID: 5176
			DATA_PTR,
			// Token: 0x04001439 RID: 5177
			NAME,
			// Token: 0x0400143A RID: 5178
			UNNAMED,
			// Token: 0x0400143B RID: 5179
			OCTET_LENGTH,
			// Token: 0x0400143C RID: 5180
			ALLOC_TYPE = 1099,
			// Token: 0x0400143D RID: 5181
			CONCISE_TYPE = 2,
			// Token: 0x0400143E RID: 5182
			DISPLAY_SIZE = 6,
			// Token: 0x0400143F RID: 5183
			UNSIGNED = 8,
			// Token: 0x04001440 RID: 5184
			UPDATABLE = 10,
			// Token: 0x04001441 RID: 5185
			AUTO_UNIQUE_VALUE,
			// Token: 0x04001442 RID: 5186
			TYPE_NAME = 14,
			// Token: 0x04001443 RID: 5187
			TABLE_NAME,
			// Token: 0x04001444 RID: 5188
			SCHEMA_NAME,
			// Token: 0x04001445 RID: 5189
			CATALOG_NAME,
			// Token: 0x04001446 RID: 5190
			BASE_COLUMN_NAME = 22,
			// Token: 0x04001447 RID: 5191
			BASE_TABLE_NAME
		}

		// Token: 0x0200026B RID: 619
		internal enum SQL_COLUMN
		{
			// Token: 0x04001449 RID: 5193
			COUNT,
			// Token: 0x0400144A RID: 5194
			NAME,
			// Token: 0x0400144B RID: 5195
			TYPE,
			// Token: 0x0400144C RID: 5196
			LENGTH,
			// Token: 0x0400144D RID: 5197
			PRECISION,
			// Token: 0x0400144E RID: 5198
			SCALE,
			// Token: 0x0400144F RID: 5199
			DISPLAY_SIZE,
			// Token: 0x04001450 RID: 5200
			NULLABLE,
			// Token: 0x04001451 RID: 5201
			UNSIGNED,
			// Token: 0x04001452 RID: 5202
			MONEY,
			// Token: 0x04001453 RID: 5203
			UPDATABLE,
			// Token: 0x04001454 RID: 5204
			AUTO_INCREMENT,
			// Token: 0x04001455 RID: 5205
			CASE_SENSITIVE,
			// Token: 0x04001456 RID: 5206
			SEARCHABLE,
			// Token: 0x04001457 RID: 5207
			TYPE_NAME,
			// Token: 0x04001458 RID: 5208
			TABLE_NAME,
			// Token: 0x04001459 RID: 5209
			OWNER_NAME,
			// Token: 0x0400145A RID: 5210
			QUALIFIER_NAME,
			// Token: 0x0400145B RID: 5211
			LABEL
		}

		// Token: 0x0200026C RID: 620
		internal enum SQL_GROUP_BY
		{
			// Token: 0x0400145D RID: 5213
			NOT_SUPPORTED,
			// Token: 0x0400145E RID: 5214
			GROUP_BY_EQUALS_SELECT,
			// Token: 0x0400145F RID: 5215
			GROUP_BY_CONTAINS_SELECT,
			// Token: 0x04001460 RID: 5216
			NO_RELATION,
			// Token: 0x04001461 RID: 5217
			COLLATE
		}

		// Token: 0x0200026D RID: 621
		internal enum SQL_SQL92_RELATIONAL_JOIN_OPERATORS
		{
			// Token: 0x04001463 RID: 5219
			CORRESPONDING_CLAUSE = 1,
			// Token: 0x04001464 RID: 5220
			CROSS_JOIN,
			// Token: 0x04001465 RID: 5221
			EXCEPT_JOIN = 4,
			// Token: 0x04001466 RID: 5222
			FULL_OUTER_JOIN = 8,
			// Token: 0x04001467 RID: 5223
			INNER_JOIN = 16,
			// Token: 0x04001468 RID: 5224
			INTERSECT_JOIN = 32,
			// Token: 0x04001469 RID: 5225
			LEFT_OUTER_JOIN = 64,
			// Token: 0x0400146A RID: 5226
			NATURAL_JOIN = 128,
			// Token: 0x0400146B RID: 5227
			RIGHT_OUTER_JOIN = 256,
			// Token: 0x0400146C RID: 5228
			UNION_JOIN = 512
		}

		// Token: 0x0200026E RID: 622
		internal enum SQL_OJ_CAPABILITIES
		{
			// Token: 0x0400146E RID: 5230
			LEFT = 1,
			// Token: 0x0400146F RID: 5231
			RIGHT,
			// Token: 0x04001470 RID: 5232
			FULL = 4,
			// Token: 0x04001471 RID: 5233
			NESTED = 8,
			// Token: 0x04001472 RID: 5234
			NOT_ORDERED = 16,
			// Token: 0x04001473 RID: 5235
			INNER = 32,
			// Token: 0x04001474 RID: 5236
			ALL_COMPARISON_OPS = 64
		}

		// Token: 0x0200026F RID: 623
		internal enum SQL_UPDATABLE
		{
			// Token: 0x04001476 RID: 5238
			READONLY,
			// Token: 0x04001477 RID: 5239
			WRITE,
			// Token: 0x04001478 RID: 5240
			READWRITE_UNKNOWN
		}

		// Token: 0x02000270 RID: 624
		internal enum SQL_IDENTIFIER_CASE
		{
			// Token: 0x0400147A RID: 5242
			UPPER = 1,
			// Token: 0x0400147B RID: 5243
			LOWER,
			// Token: 0x0400147C RID: 5244
			SENSITIVE,
			// Token: 0x0400147D RID: 5245
			MIXED
		}

		// Token: 0x02000271 RID: 625
		internal enum SQL_INDEX : short
		{
			// Token: 0x0400147F RID: 5247
			UNIQUE,
			// Token: 0x04001480 RID: 5248
			ALL
		}

		// Token: 0x02000272 RID: 626
		internal enum SQL_STATISTICS_RESERVED : short
		{
			// Token: 0x04001482 RID: 5250
			QUICK,
			// Token: 0x04001483 RID: 5251
			ENSURE
		}

		// Token: 0x02000273 RID: 627
		internal enum SQL_SPECIALCOLS : ushort
		{
			// Token: 0x04001485 RID: 5253
			BEST_ROWID = 1,
			// Token: 0x04001486 RID: 5254
			ROWVER
		}

		// Token: 0x02000274 RID: 628
		internal enum SQL_SCOPE : ushort
		{
			// Token: 0x04001488 RID: 5256
			CURROW,
			// Token: 0x04001489 RID: 5257
			TRANSACTION,
			// Token: 0x0400148A RID: 5258
			SESSION
		}

		// Token: 0x02000275 RID: 629
		internal enum SQL_NULLABILITY : ushort
		{
			// Token: 0x0400148C RID: 5260
			NO_NULLS,
			// Token: 0x0400148D RID: 5261
			NULLABLE,
			// Token: 0x0400148E RID: 5262
			UNKNOWN
		}

		// Token: 0x02000276 RID: 630
		internal enum SQL_SEARCHABLE
		{
			// Token: 0x04001490 RID: 5264
			UNSEARCHABLE,
			// Token: 0x04001491 RID: 5265
			LIKE_ONLY,
			// Token: 0x04001492 RID: 5266
			ALL_EXCEPT_LIKE,
			// Token: 0x04001493 RID: 5267
			SEARCHABLE
		}

		// Token: 0x02000277 RID: 631
		internal enum SQL_UNNAMED
		{
			// Token: 0x04001495 RID: 5269
			NAMED,
			// Token: 0x04001496 RID: 5270
			UNNAMED
		}

		// Token: 0x02000278 RID: 632
		internal enum HANDLER
		{
			// Token: 0x04001498 RID: 5272
			IGNORE,
			// Token: 0x04001499 RID: 5273
			THROW
		}

		// Token: 0x02000279 RID: 633
		internal enum SQL_STATISTICSTYPE
		{
			// Token: 0x0400149B RID: 5275
			TABLE_STAT,
			// Token: 0x0400149C RID: 5276
			INDEX_CLUSTERED,
			// Token: 0x0400149D RID: 5277
			INDEX_HASHED,
			// Token: 0x0400149E RID: 5278
			INDEX_OTHER
		}

		// Token: 0x0200027A RID: 634
		internal enum SQL_PROCEDURETYPE
		{
			// Token: 0x040014A0 RID: 5280
			UNKNOWN,
			// Token: 0x040014A1 RID: 5281
			PROCEDURE,
			// Token: 0x040014A2 RID: 5282
			FUNCTION
		}

		// Token: 0x0200027B RID: 635
		internal enum SQL_C : short
		{
			// Token: 0x040014A4 RID: 5284
			CHAR = 1,
			// Token: 0x040014A5 RID: 5285
			WCHAR = -8,
			// Token: 0x040014A6 RID: 5286
			SLONG = -16,
			// Token: 0x040014A7 RID: 5287
			SSHORT,
			// Token: 0x040014A8 RID: 5288
			REAL = 7,
			// Token: 0x040014A9 RID: 5289
			DOUBLE,
			// Token: 0x040014AA RID: 5290
			BIT = -7,
			// Token: 0x040014AB RID: 5291
			UTINYINT = -28,
			// Token: 0x040014AC RID: 5292
			SBIGINT = -25,
			// Token: 0x040014AD RID: 5293
			UBIGINT = -27,
			// Token: 0x040014AE RID: 5294
			BINARY = -2,
			// Token: 0x040014AF RID: 5295
			TIMESTAMP = 11,
			// Token: 0x040014B0 RID: 5296
			TYPE_DATE = 91,
			// Token: 0x040014B1 RID: 5297
			TYPE_TIME,
			// Token: 0x040014B2 RID: 5298
			TYPE_TIMESTAMP,
			// Token: 0x040014B3 RID: 5299
			NUMERIC = 2,
			// Token: 0x040014B4 RID: 5300
			GUID = -11,
			// Token: 0x040014B5 RID: 5301
			DEFAULT = 99,
			// Token: 0x040014B6 RID: 5302
			ARD_TYPE = -99
		}

		// Token: 0x0200027C RID: 636
		internal enum SQL_TYPE : short
		{
			// Token: 0x040014B8 RID: 5304
			CHAR = 1,
			// Token: 0x040014B9 RID: 5305
			VARCHAR = 12,
			// Token: 0x040014BA RID: 5306
			LONGVARCHAR = -1,
			// Token: 0x040014BB RID: 5307
			WCHAR = -8,
			// Token: 0x040014BC RID: 5308
			WVARCHAR = -9,
			// Token: 0x040014BD RID: 5309
			WLONGVARCHAR = -10,
			// Token: 0x040014BE RID: 5310
			DECIMAL = 3,
			// Token: 0x040014BF RID: 5311
			NUMERIC = 2,
			// Token: 0x040014C0 RID: 5312
			SMALLINT = 5,
			// Token: 0x040014C1 RID: 5313
			INTEGER = 4,
			// Token: 0x040014C2 RID: 5314
			REAL = 7,
			// Token: 0x040014C3 RID: 5315
			FLOAT = 6,
			// Token: 0x040014C4 RID: 5316
			DOUBLE = 8,
			// Token: 0x040014C5 RID: 5317
			BIT = -7,
			// Token: 0x040014C6 RID: 5318
			TINYINT,
			// Token: 0x040014C7 RID: 5319
			BIGINT,
			// Token: 0x040014C8 RID: 5320
			BINARY = -2,
			// Token: 0x040014C9 RID: 5321
			VARBINARY = -3,
			// Token: 0x040014CA RID: 5322
			LONGVARBINARY = -4,
			// Token: 0x040014CB RID: 5323
			TYPE_DATE = 91,
			// Token: 0x040014CC RID: 5324
			TYPE_TIME,
			// Token: 0x040014CD RID: 5325
			TIMESTAMP = 11,
			// Token: 0x040014CE RID: 5326
			TYPE_TIMESTAMP = 93,
			// Token: 0x040014CF RID: 5327
			GUID = -11,
			// Token: 0x040014D0 RID: 5328
			SS_VARIANT = -150,
			// Token: 0x040014D1 RID: 5329
			SS_UDT = -151,
			// Token: 0x040014D2 RID: 5330
			SS_XML = -152,
			// Token: 0x040014D3 RID: 5331
			SS_UTCDATETIME = -153,
			// Token: 0x040014D4 RID: 5332
			SS_TIME_EX = -154
		}

		// Token: 0x0200027D RID: 637
		internal enum SQL_ATTR
		{
			// Token: 0x040014D6 RID: 5334
			APP_ROW_DESC = 10010,
			// Token: 0x040014D7 RID: 5335
			APP_PARAM_DESC,
			// Token: 0x040014D8 RID: 5336
			IMP_ROW_DESC,
			// Token: 0x040014D9 RID: 5337
			IMP_PARAM_DESC,
			// Token: 0x040014DA RID: 5338
			METADATA_ID,
			// Token: 0x040014DB RID: 5339
			ODBC_VERSION = 200,
			// Token: 0x040014DC RID: 5340
			CONNECTION_POOLING,
			// Token: 0x040014DD RID: 5341
			AUTOCOMMIT = 102,
			// Token: 0x040014DE RID: 5342
			TXN_ISOLATION = 108,
			// Token: 0x040014DF RID: 5343
			CURRENT_CATALOG,
			// Token: 0x040014E0 RID: 5344
			LOGIN_TIMEOUT = 103,
			// Token: 0x040014E1 RID: 5345
			QUERY_TIMEOUT = 0,
			// Token: 0x040014E2 RID: 5346
			CONNECTION_DEAD = 1209,
			// Token: 0x040014E3 RID: 5347
			SQL_COPT_SS_BASE = 1200,
			// Token: 0x040014E4 RID: 5348
			SQL_COPT_SS_ENLIST_IN_DTC = 1207,
			// Token: 0x040014E5 RID: 5349
			SQL_COPT_SS_TXN_ISOLATION = 1227
		}

		// Token: 0x0200027E RID: 638
		internal enum SQL_INFO : ushort
		{
			// Token: 0x040014E7 RID: 5351
			DATA_SOURCE_NAME = 2,
			// Token: 0x040014E8 RID: 5352
			SERVER_NAME = 13,
			// Token: 0x040014E9 RID: 5353
			DRIVER_NAME = 6,
			// Token: 0x040014EA RID: 5354
			DRIVER_VER,
			// Token: 0x040014EB RID: 5355
			ODBC_VER = 10,
			// Token: 0x040014EC RID: 5356
			SEARCH_PATTERN_ESCAPE = 14,
			// Token: 0x040014ED RID: 5357
			DBMS_VER = 18,
			// Token: 0x040014EE RID: 5358
			DBMS_NAME = 17,
			// Token: 0x040014EF RID: 5359
			IDENTIFIER_CASE = 28,
			// Token: 0x040014F0 RID: 5360
			IDENTIFIER_QUOTE_CHAR,
			// Token: 0x040014F1 RID: 5361
			CATALOG_NAME_SEPARATOR = 41,
			// Token: 0x040014F2 RID: 5362
			DRIVER_ODBC_VER = 77,
			// Token: 0x040014F3 RID: 5363
			GROUP_BY = 88,
			// Token: 0x040014F4 RID: 5364
			KEYWORDS,
			// Token: 0x040014F5 RID: 5365
			ORDER_BY_COLUMNS_IN_SELECT,
			// Token: 0x040014F6 RID: 5366
			QUOTED_IDENTIFIER_CASE = 93,
			// Token: 0x040014F7 RID: 5367
			SQL_OJ_CAPABILITIES_30 = 115,
			// Token: 0x040014F8 RID: 5368
			SQL_OJ_CAPABILITIES_20 = 65003,
			// Token: 0x040014F9 RID: 5369
			SQL_SQL92_RELATIONAL_JOIN_OPERATORS = 161
		}

		// Token: 0x0200027F RID: 639
		internal enum SQL_DRIVER
		{
			// Token: 0x040014FB RID: 5371
			NOPROMPT,
			// Token: 0x040014FC RID: 5372
			COMPLETE,
			// Token: 0x040014FD RID: 5373
			PROMPT,
			// Token: 0x040014FE RID: 5374
			COMPLETE_REQUIRED
		}

		// Token: 0x02000280 RID: 640
		internal enum SQL_PRIMARYKEYS : short
		{
			// Token: 0x04001500 RID: 5376
			COLUMNNAME = 4
		}

		// Token: 0x02000281 RID: 641
		internal enum SQL_STATISTICS : short
		{
			// Token: 0x04001502 RID: 5378
			INDEXNAME = 6,
			// Token: 0x04001503 RID: 5379
			ORDINAL_POSITION = 8,
			// Token: 0x04001504 RID: 5380
			COLUMN_NAME
		}

		// Token: 0x02000282 RID: 642
		internal enum SQL_SPECIALCOLUMNSET : short
		{
			// Token: 0x04001506 RID: 5382
			COLUMN_NAME = 2
		}
	}
}
