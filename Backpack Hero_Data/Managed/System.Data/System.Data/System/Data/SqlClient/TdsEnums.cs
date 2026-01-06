using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001EC RID: 492
	internal static class TdsEnums
	{
		// Token: 0x060017D3 RID: 6099 RVA: 0x0007203C File Offset: 0x0007023C
		internal static string GetSniContextEnumName(SniContext sniContext)
		{
			switch (sniContext)
			{
			case SniContext.Undefined:
				return "Undefined";
			case SniContext.Snix_Connect:
				return "Snix_Connect";
			case SniContext.Snix_PreLoginBeforeSuccessfulWrite:
				return "Snix_PreLoginBeforeSuccessfulWrite";
			case SniContext.Snix_PreLogin:
				return "Snix_PreLogin";
			case SniContext.Snix_LoginSspi:
				return "Snix_LoginSspi";
			case SniContext.Snix_ProcessSspi:
				return "Snix_ProcessSspi";
			case SniContext.Snix_Login:
				return "Snix_Login";
			case SniContext.Snix_EnableMars:
				return "Snix_EnableMars";
			case SniContext.Snix_AutoEnlist:
				return "Snix_AutoEnlist";
			case SniContext.Snix_GetMarsSession:
				return "Snix_GetMarsSession";
			case SniContext.Snix_Execute:
				return "Snix_Execute";
			case SniContext.Snix_Read:
				return "Snix_Read";
			case SniContext.Snix_Close:
				return "Snix_Close";
			case SniContext.Snix_SendRows:
				return "Snix_SendRows";
			default:
				return null;
			}
		}

		// Token: 0x04000F52 RID: 3922
		public const string SQL_PROVIDER_NAME = "Core .Net SqlClient Data Provider";

		// Token: 0x04000F53 RID: 3923
		public static readonly decimal SQL_SMALL_MONEY_MIN = new decimal(-214748.3648);

		// Token: 0x04000F54 RID: 3924
		public static readonly decimal SQL_SMALL_MONEY_MAX = new decimal(214748.3647);

		// Token: 0x04000F55 RID: 3925
		public const SqlDbType SmallVarBinary = (SqlDbType)24;

		// Token: 0x04000F56 RID: 3926
		public const string TCP = "tcp";

		// Token: 0x04000F57 RID: 3927
		public const string NP = "np";

		// Token: 0x04000F58 RID: 3928
		public const string RPC = "rpc";

		// Token: 0x04000F59 RID: 3929
		public const string BV = "bv";

		// Token: 0x04000F5A RID: 3930
		public const string ADSP = "adsp";

		// Token: 0x04000F5B RID: 3931
		public const string SPX = "spx";

		// Token: 0x04000F5C RID: 3932
		public const string VIA = "via";

		// Token: 0x04000F5D RID: 3933
		public const string LPC = "lpc";

		// Token: 0x04000F5E RID: 3934
		public const string ADMIN = "admin";

		// Token: 0x04000F5F RID: 3935
		public const string INIT_SSPI_PACKAGE = "InitSSPIPackage";

		// Token: 0x04000F60 RID: 3936
		public const string INIT_SESSION = "InitSession";

		// Token: 0x04000F61 RID: 3937
		public const string CONNECTION_GET_SVR_USER = "ConnectionGetSvrUser";

		// Token: 0x04000F62 RID: 3938
		public const string GEN_CLIENT_CONTEXT = "GenClientContext";

		// Token: 0x04000F63 RID: 3939
		public const byte SOFTFLUSH = 0;

		// Token: 0x04000F64 RID: 3940
		public const byte HARDFLUSH = 1;

		// Token: 0x04000F65 RID: 3941
		public const byte IGNORE = 2;

		// Token: 0x04000F66 RID: 3942
		public const int HEADER_LEN = 8;

		// Token: 0x04000F67 RID: 3943
		public const int HEADER_LEN_FIELD_OFFSET = 2;

		// Token: 0x04000F68 RID: 3944
		public const int YUKON_HEADER_LEN = 12;

		// Token: 0x04000F69 RID: 3945
		public const int MARS_ID_OFFSET = 8;

		// Token: 0x04000F6A RID: 3946
		public const int HEADERTYPE_QNOTIFICATION = 1;

		// Token: 0x04000F6B RID: 3947
		public const int HEADERTYPE_MARS = 2;

		// Token: 0x04000F6C RID: 3948
		public const int HEADERTYPE_TRACE = 3;

		// Token: 0x04000F6D RID: 3949
		public const int SUCCEED = 1;

		// Token: 0x04000F6E RID: 3950
		public const int FAIL = 0;

		// Token: 0x04000F6F RID: 3951
		public const short TYPE_SIZE_LIMIT = 8000;

		// Token: 0x04000F70 RID: 3952
		public const int MIN_PACKET_SIZE = 512;

		// Token: 0x04000F71 RID: 3953
		public const int DEFAULT_LOGIN_PACKET_SIZE = 4096;

		// Token: 0x04000F72 RID: 3954
		public const int MAX_PRELOGIN_PAYLOAD_LENGTH = 1024;

		// Token: 0x04000F73 RID: 3955
		public const int MAX_PACKET_SIZE = 32768;

		// Token: 0x04000F74 RID: 3956
		public const int MAX_SERVER_USER_NAME = 256;

		// Token: 0x04000F75 RID: 3957
		public const byte MIN_ERROR_CLASS = 11;

		// Token: 0x04000F76 RID: 3958
		public const byte MAX_USER_CORRECTABLE_ERROR_CLASS = 16;

		// Token: 0x04000F77 RID: 3959
		public const byte FATAL_ERROR_CLASS = 20;

		// Token: 0x04000F78 RID: 3960
		public const byte MT_SQL = 1;

		// Token: 0x04000F79 RID: 3961
		public const byte MT_LOGIN = 2;

		// Token: 0x04000F7A RID: 3962
		public const byte MT_RPC = 3;

		// Token: 0x04000F7B RID: 3963
		public const byte MT_TOKENS = 4;

		// Token: 0x04000F7C RID: 3964
		public const byte MT_BINARY = 5;

		// Token: 0x04000F7D RID: 3965
		public const byte MT_ATTN = 6;

		// Token: 0x04000F7E RID: 3966
		public const byte MT_BULK = 7;

		// Token: 0x04000F7F RID: 3967
		public const byte MT_OPEN = 8;

		// Token: 0x04000F80 RID: 3968
		public const byte MT_CLOSE = 9;

		// Token: 0x04000F81 RID: 3969
		public const byte MT_ERROR = 10;

		// Token: 0x04000F82 RID: 3970
		public const byte MT_ACK = 11;

		// Token: 0x04000F83 RID: 3971
		public const byte MT_ECHO = 12;

		// Token: 0x04000F84 RID: 3972
		public const byte MT_LOGOUT = 13;

		// Token: 0x04000F85 RID: 3973
		public const byte MT_TRANS = 14;

		// Token: 0x04000F86 RID: 3974
		public const byte MT_OLEDB = 15;

		// Token: 0x04000F87 RID: 3975
		public const byte MT_LOGIN7 = 16;

		// Token: 0x04000F88 RID: 3976
		public const byte MT_SSPI = 17;

		// Token: 0x04000F89 RID: 3977
		public const byte MT_PRELOGIN = 18;

		// Token: 0x04000F8A RID: 3978
		public const byte ST_EOM = 1;

		// Token: 0x04000F8B RID: 3979
		public const byte ST_AACK = 2;

		// Token: 0x04000F8C RID: 3980
		public const byte ST_IGNORE = 2;

		// Token: 0x04000F8D RID: 3981
		public const byte ST_BATCH = 4;

		// Token: 0x04000F8E RID: 3982
		public const byte ST_RESET_CONNECTION = 8;

		// Token: 0x04000F8F RID: 3983
		public const byte ST_RESET_CONNECTION_PRESERVE_TRANSACTION = 16;

		// Token: 0x04000F90 RID: 3984
		public const byte SQLCOLFMT = 161;

		// Token: 0x04000F91 RID: 3985
		public const byte SQLPROCID = 124;

		// Token: 0x04000F92 RID: 3986
		public const byte SQLCOLNAME = 160;

		// Token: 0x04000F93 RID: 3987
		public const byte SQLTABNAME = 164;

		// Token: 0x04000F94 RID: 3988
		public const byte SQLCOLINFO = 165;

		// Token: 0x04000F95 RID: 3989
		public const byte SQLALTNAME = 167;

		// Token: 0x04000F96 RID: 3990
		public const byte SQLALTFMT = 168;

		// Token: 0x04000F97 RID: 3991
		public const byte SQLERROR = 170;

		// Token: 0x04000F98 RID: 3992
		public const byte SQLINFO = 171;

		// Token: 0x04000F99 RID: 3993
		public const byte SQLRETURNVALUE = 172;

		// Token: 0x04000F9A RID: 3994
		public const byte SQLRETURNSTATUS = 121;

		// Token: 0x04000F9B RID: 3995
		public const byte SQLRETURNTOK = 219;

		// Token: 0x04000F9C RID: 3996
		public const byte SQLALTCONTROL = 175;

		// Token: 0x04000F9D RID: 3997
		public const byte SQLROW = 209;

		// Token: 0x04000F9E RID: 3998
		public const byte SQLNBCROW = 210;

		// Token: 0x04000F9F RID: 3999
		public const byte SQLALTROW = 211;

		// Token: 0x04000FA0 RID: 4000
		public const byte SQLDONE = 253;

		// Token: 0x04000FA1 RID: 4001
		public const byte SQLDONEPROC = 254;

		// Token: 0x04000FA2 RID: 4002
		public const byte SQLDONEINPROC = 255;

		// Token: 0x04000FA3 RID: 4003
		public const byte SQLOFFSET = 120;

		// Token: 0x04000FA4 RID: 4004
		public const byte SQLORDER = 169;

		// Token: 0x04000FA5 RID: 4005
		public const byte SQLDEBUG_CMD = 96;

		// Token: 0x04000FA6 RID: 4006
		public const byte SQLLOGINACK = 173;

		// Token: 0x04000FA7 RID: 4007
		public const byte SQLFEATUREEXTACK = 174;

		// Token: 0x04000FA8 RID: 4008
		public const byte SQLSESSIONSTATE = 228;

		// Token: 0x04000FA9 RID: 4009
		public const byte SQLENVCHANGE = 227;

		// Token: 0x04000FAA RID: 4010
		public const byte SQLSECLEVEL = 237;

		// Token: 0x04000FAB RID: 4011
		public const byte SQLROWCRC = 57;

		// Token: 0x04000FAC RID: 4012
		public const byte SQLCOLMETADATA = 129;

		// Token: 0x04000FAD RID: 4013
		public const byte SQLALTMETADATA = 136;

		// Token: 0x04000FAE RID: 4014
		public const byte SQLSSPI = 237;

		// Token: 0x04000FAF RID: 4015
		public const byte ENV_DATABASE = 1;

		// Token: 0x04000FB0 RID: 4016
		public const byte ENV_LANG = 2;

		// Token: 0x04000FB1 RID: 4017
		public const byte ENV_CHARSET = 3;

		// Token: 0x04000FB2 RID: 4018
		public const byte ENV_PACKETSIZE = 4;

		// Token: 0x04000FB3 RID: 4019
		public const byte ENV_LOCALEID = 5;

		// Token: 0x04000FB4 RID: 4020
		public const byte ENV_COMPFLAGS = 6;

		// Token: 0x04000FB5 RID: 4021
		public const byte ENV_COLLATION = 7;

		// Token: 0x04000FB6 RID: 4022
		public const byte ENV_BEGINTRAN = 8;

		// Token: 0x04000FB7 RID: 4023
		public const byte ENV_COMMITTRAN = 9;

		// Token: 0x04000FB8 RID: 4024
		public const byte ENV_ROLLBACKTRAN = 10;

		// Token: 0x04000FB9 RID: 4025
		public const byte ENV_ENLISTDTC = 11;

		// Token: 0x04000FBA RID: 4026
		public const byte ENV_DEFECTDTC = 12;

		// Token: 0x04000FBB RID: 4027
		public const byte ENV_LOGSHIPNODE = 13;

		// Token: 0x04000FBC RID: 4028
		public const byte ENV_PROMOTETRANSACTION = 15;

		// Token: 0x04000FBD RID: 4029
		public const byte ENV_TRANSACTIONMANAGERADDRESS = 16;

		// Token: 0x04000FBE RID: 4030
		public const byte ENV_TRANSACTIONENDED = 17;

		// Token: 0x04000FBF RID: 4031
		public const byte ENV_SPRESETCONNECTIONACK = 18;

		// Token: 0x04000FC0 RID: 4032
		public const byte ENV_USERINSTANCE = 19;

		// Token: 0x04000FC1 RID: 4033
		public const byte ENV_ROUTING = 20;

		// Token: 0x04000FC2 RID: 4034
		public const int DONE_MORE = 1;

		// Token: 0x04000FC3 RID: 4035
		public const int DONE_ERROR = 2;

		// Token: 0x04000FC4 RID: 4036
		public const int DONE_INXACT = 4;

		// Token: 0x04000FC5 RID: 4037
		public const int DONE_PROC = 8;

		// Token: 0x04000FC6 RID: 4038
		public const int DONE_COUNT = 16;

		// Token: 0x04000FC7 RID: 4039
		public const int DONE_ATTN = 32;

		// Token: 0x04000FC8 RID: 4040
		public const int DONE_INPROC = 64;

		// Token: 0x04000FC9 RID: 4041
		public const int DONE_RPCINBATCH = 128;

		// Token: 0x04000FCA RID: 4042
		public const int DONE_SRVERROR = 256;

		// Token: 0x04000FCB RID: 4043
		public const int DONE_FMTSENT = 32768;

		// Token: 0x04000FCC RID: 4044
		public const byte FEATUREEXT_TERMINATOR = 255;

		// Token: 0x04000FCD RID: 4045
		public const byte FEATUREEXT_SRECOVERY = 1;

		// Token: 0x04000FCE RID: 4046
		public const byte FEATUREEXT_GLOBALTRANSACTIONS = 5;

		// Token: 0x04000FCF RID: 4047
		public const byte FEATUREEXT_FEDAUTH = 2;

		// Token: 0x04000FD0 RID: 4048
		public const byte FEDAUTHLIB_LIVEID = 0;

		// Token: 0x04000FD1 RID: 4049
		public const byte FEDAUTHLIB_SECURITYTOKEN = 1;

		// Token: 0x04000FD2 RID: 4050
		public const byte FEDAUTHLIB_ADAL = 2;

		// Token: 0x04000FD3 RID: 4051
		public const byte FEDAUTHLIB_RESERVED = 127;

		// Token: 0x04000FD4 RID: 4052
		public const byte MAX_LOG_NAME = 30;

		// Token: 0x04000FD5 RID: 4053
		public const byte MAX_PROG_NAME = 10;

		// Token: 0x04000FD6 RID: 4054
		public const byte SEC_COMP_LEN = 8;

		// Token: 0x04000FD7 RID: 4055
		public const byte MAX_PK_LEN = 6;

		// Token: 0x04000FD8 RID: 4056
		public const byte MAX_NIC_SIZE = 6;

		// Token: 0x04000FD9 RID: 4057
		public const byte SQLVARIANT_SIZE = 2;

		// Token: 0x04000FDA RID: 4058
		public const byte VERSION_SIZE = 4;

		// Token: 0x04000FDB RID: 4059
		public const int CLIENT_PROG_VER = 100663296;

		// Token: 0x04000FDC RID: 4060
		public const int YUKON_LOG_REC_FIXED_LEN = 94;

		// Token: 0x04000FDD RID: 4061
		public const int TEXT_TIME_STAMP_LEN = 8;

		// Token: 0x04000FDE RID: 4062
		public const int COLLATION_INFO_LEN = 4;

		// Token: 0x04000FDF RID: 4063
		public const int YUKON_MAJOR = 114;

		// Token: 0x04000FE0 RID: 4064
		public const int KATMAI_MAJOR = 115;

		// Token: 0x04000FE1 RID: 4065
		public const int DENALI_MAJOR = 116;

		// Token: 0x04000FE2 RID: 4066
		public const int YUKON_INCREMENT = 9;

		// Token: 0x04000FE3 RID: 4067
		public const int KATMAI_INCREMENT = 11;

		// Token: 0x04000FE4 RID: 4068
		public const int DENALI_INCREMENT = 0;

		// Token: 0x04000FE5 RID: 4069
		public const int YUKON_RTM_MINOR = 2;

		// Token: 0x04000FE6 RID: 4070
		public const int KATMAI_MINOR = 3;

		// Token: 0x04000FE7 RID: 4071
		public const int DENALI_MINOR = 4;

		// Token: 0x04000FE8 RID: 4072
		public const int ORDER_68000 = 1;

		// Token: 0x04000FE9 RID: 4073
		public const int USE_DB_ON = 1;

		// Token: 0x04000FEA RID: 4074
		public const int INIT_DB_FATAL = 1;

		// Token: 0x04000FEB RID: 4075
		public const int SET_LANG_ON = 1;

		// Token: 0x04000FEC RID: 4076
		public const int INIT_LANG_FATAL = 1;

		// Token: 0x04000FED RID: 4077
		public const int ODBC_ON = 1;

		// Token: 0x04000FEE RID: 4078
		public const int SSPI_ON = 1;

		// Token: 0x04000FEF RID: 4079
		public const int REPL_ON = 3;

		// Token: 0x04000FF0 RID: 4080
		public const int READONLY_INTENT_ON = 1;

		// Token: 0x04000FF1 RID: 4081
		public const byte SQLLenMask = 48;

		// Token: 0x04000FF2 RID: 4082
		public const byte SQLFixedLen = 48;

		// Token: 0x04000FF3 RID: 4083
		public const byte SQLVarLen = 32;

		// Token: 0x04000FF4 RID: 4084
		public const byte SQLZeroLen = 16;

		// Token: 0x04000FF5 RID: 4085
		public const byte SQLVarCnt = 0;

		// Token: 0x04000FF6 RID: 4086
		public const byte SQLDifferentName = 32;

		// Token: 0x04000FF7 RID: 4087
		public const byte SQLExpression = 4;

		// Token: 0x04000FF8 RID: 4088
		public const byte SQLKey = 8;

		// Token: 0x04000FF9 RID: 4089
		public const byte SQLHidden = 16;

		// Token: 0x04000FFA RID: 4090
		public const byte Nullable = 1;

		// Token: 0x04000FFB RID: 4091
		public const byte Identity = 16;

		// Token: 0x04000FFC RID: 4092
		public const byte Updatability = 11;

		// Token: 0x04000FFD RID: 4093
		public const byte ClrFixedLen = 1;

		// Token: 0x04000FFE RID: 4094
		public const byte IsColumnSet = 4;

		// Token: 0x04000FFF RID: 4095
		public const uint VARLONGNULL = 4294967295U;

		// Token: 0x04001000 RID: 4096
		public const int VARNULL = 65535;

		// Token: 0x04001001 RID: 4097
		public const int MAXSIZE = 8000;

		// Token: 0x04001002 RID: 4098
		public const byte FIXEDNULL = 0;

		// Token: 0x04001003 RID: 4099
		public const ulong UDTNULL = 18446744073709551615UL;

		// Token: 0x04001004 RID: 4100
		public const int SQLVOID = 31;

		// Token: 0x04001005 RID: 4101
		public const int SQLTEXT = 35;

		// Token: 0x04001006 RID: 4102
		public const int SQLVARBINARY = 37;

		// Token: 0x04001007 RID: 4103
		public const int SQLINTN = 38;

		// Token: 0x04001008 RID: 4104
		public const int SQLVARCHAR = 39;

		// Token: 0x04001009 RID: 4105
		public const int SQLBINARY = 45;

		// Token: 0x0400100A RID: 4106
		public const int SQLIMAGE = 34;

		// Token: 0x0400100B RID: 4107
		public const int SQLCHAR = 47;

		// Token: 0x0400100C RID: 4108
		public const int SQLINT1 = 48;

		// Token: 0x0400100D RID: 4109
		public const int SQLBIT = 50;

		// Token: 0x0400100E RID: 4110
		public const int SQLINT2 = 52;

		// Token: 0x0400100F RID: 4111
		public const int SQLINT4 = 56;

		// Token: 0x04001010 RID: 4112
		public const int SQLMONEY = 60;

		// Token: 0x04001011 RID: 4113
		public const int SQLDATETIME = 61;

		// Token: 0x04001012 RID: 4114
		public const int SQLFLT8 = 62;

		// Token: 0x04001013 RID: 4115
		public const int SQLFLTN = 109;

		// Token: 0x04001014 RID: 4116
		public const int SQLMONEYN = 110;

		// Token: 0x04001015 RID: 4117
		public const int SQLDATETIMN = 111;

		// Token: 0x04001016 RID: 4118
		public const int SQLFLT4 = 59;

		// Token: 0x04001017 RID: 4119
		public const int SQLMONEY4 = 122;

		// Token: 0x04001018 RID: 4120
		public const int SQLDATETIM4 = 58;

		// Token: 0x04001019 RID: 4121
		public const int SQLDECIMALN = 106;

		// Token: 0x0400101A RID: 4122
		public const int SQLNUMERICN = 108;

		// Token: 0x0400101B RID: 4123
		public const int SQLUNIQUEID = 36;

		// Token: 0x0400101C RID: 4124
		public const int SQLBIGCHAR = 175;

		// Token: 0x0400101D RID: 4125
		public const int SQLBIGVARCHAR = 167;

		// Token: 0x0400101E RID: 4126
		public const int SQLBIGBINARY = 173;

		// Token: 0x0400101F RID: 4127
		public const int SQLBIGVARBINARY = 165;

		// Token: 0x04001020 RID: 4128
		public const int SQLBITN = 104;

		// Token: 0x04001021 RID: 4129
		public const int SQLNCHAR = 239;

		// Token: 0x04001022 RID: 4130
		public const int SQLNVARCHAR = 231;

		// Token: 0x04001023 RID: 4131
		public const int SQLNTEXT = 99;

		// Token: 0x04001024 RID: 4132
		public const int SQLUDT = 240;

		// Token: 0x04001025 RID: 4133
		public const int AOPCNTB = 9;

		// Token: 0x04001026 RID: 4134
		public const int AOPSTDEV = 48;

		// Token: 0x04001027 RID: 4135
		public const int AOPSTDEVP = 49;

		// Token: 0x04001028 RID: 4136
		public const int AOPVAR = 50;

		// Token: 0x04001029 RID: 4137
		public const int AOPVARP = 51;

		// Token: 0x0400102A RID: 4138
		public const int AOPCNT = 75;

		// Token: 0x0400102B RID: 4139
		public const int AOPSUM = 77;

		// Token: 0x0400102C RID: 4140
		public const int AOPAVG = 79;

		// Token: 0x0400102D RID: 4141
		public const int AOPMIN = 81;

		// Token: 0x0400102E RID: 4142
		public const int AOPMAX = 82;

		// Token: 0x0400102F RID: 4143
		public const int AOPANY = 83;

		// Token: 0x04001030 RID: 4144
		public const int AOPNOOP = 86;

		// Token: 0x04001031 RID: 4145
		public const int SQLTIMESTAMP = 80;

		// Token: 0x04001032 RID: 4146
		public const int MAX_NUMERIC_LEN = 17;

		// Token: 0x04001033 RID: 4147
		public const int DEFAULT_NUMERIC_PRECISION = 29;

		// Token: 0x04001034 RID: 4148
		public const int SPHINX_DEFAULT_NUMERIC_PRECISION = 28;

		// Token: 0x04001035 RID: 4149
		public const int MAX_NUMERIC_PRECISION = 38;

		// Token: 0x04001036 RID: 4150
		public const byte UNKNOWN_PRECISION_SCALE = 255;

		// Token: 0x04001037 RID: 4151
		public const int SQLINT8 = 127;

		// Token: 0x04001038 RID: 4152
		public const int SQLVARIANT = 98;

		// Token: 0x04001039 RID: 4153
		public const int SQLXMLTYPE = 241;

		// Token: 0x0400103A RID: 4154
		public const int XMLUNICODEBOM = 65279;

		// Token: 0x0400103B RID: 4155
		public static readonly byte[] XMLUNICODEBOMBYTES = new byte[] { byte.MaxValue, 254 };

		// Token: 0x0400103C RID: 4156
		public const int SQLTABLE = 243;

		// Token: 0x0400103D RID: 4157
		public const int SQLDATE = 40;

		// Token: 0x0400103E RID: 4158
		public const int SQLTIME = 41;

		// Token: 0x0400103F RID: 4159
		public const int SQLDATETIME2 = 42;

		// Token: 0x04001040 RID: 4160
		public const int SQLDATETIMEOFFSET = 43;

		// Token: 0x04001041 RID: 4161
		public const int DEFAULT_VARTIME_SCALE = 7;

		// Token: 0x04001042 RID: 4162
		public const ulong SQL_PLP_NULL = 18446744073709551615UL;

		// Token: 0x04001043 RID: 4163
		public const ulong SQL_PLP_UNKNOWNLEN = 18446744073709551614UL;

		// Token: 0x04001044 RID: 4164
		public const int SQL_PLP_CHUNK_TERMINATOR = 0;

		// Token: 0x04001045 RID: 4165
		public const ushort SQL_USHORTVARMAXLEN = 65535;

		// Token: 0x04001046 RID: 4166
		public const byte TVP_ROWCOUNT_ESTIMATE = 18;

		// Token: 0x04001047 RID: 4167
		public const byte TVP_ROW_TOKEN = 1;

		// Token: 0x04001048 RID: 4168
		public const byte TVP_END_TOKEN = 0;

		// Token: 0x04001049 RID: 4169
		public const ushort TVP_NOMETADATA_TOKEN = 65535;

		// Token: 0x0400104A RID: 4170
		public const byte TVP_ORDER_UNIQUE_TOKEN = 16;

		// Token: 0x0400104B RID: 4171
		public const int TVP_DEFAULT_COLUMN = 512;

		// Token: 0x0400104C RID: 4172
		public const byte TVP_ORDERASC_FLAG = 1;

		// Token: 0x0400104D RID: 4173
		public const byte TVP_ORDERDESC_FLAG = 2;

		// Token: 0x0400104E RID: 4174
		public const byte TVP_UNIQUE_FLAG = 4;

		// Token: 0x0400104F RID: 4175
		public const string SP_EXECUTESQL = "sp_executesql";

		// Token: 0x04001050 RID: 4176
		public const string SP_PREPEXEC = "sp_prepexec";

		// Token: 0x04001051 RID: 4177
		public const string SP_PREPARE = "sp_prepare";

		// Token: 0x04001052 RID: 4178
		public const string SP_EXECUTE = "sp_execute";

		// Token: 0x04001053 RID: 4179
		public const string SP_UNPREPARE = "sp_unprepare";

		// Token: 0x04001054 RID: 4180
		public const string SP_PARAMS = "sp_procedure_params_rowset";

		// Token: 0x04001055 RID: 4181
		public const string SP_PARAMS_MANAGED = "sp_procedure_params_managed";

		// Token: 0x04001056 RID: 4182
		public const string SP_PARAMS_MGD10 = "sp_procedure_params_100_managed";

		// Token: 0x04001057 RID: 4183
		public const ushort RPC_PROCID_CURSOR = 1;

		// Token: 0x04001058 RID: 4184
		public const ushort RPC_PROCID_CURSOROPEN = 2;

		// Token: 0x04001059 RID: 4185
		public const ushort RPC_PROCID_CURSORPREPARE = 3;

		// Token: 0x0400105A RID: 4186
		public const ushort RPC_PROCID_CURSOREXECUTE = 4;

		// Token: 0x0400105B RID: 4187
		public const ushort RPC_PROCID_CURSORPREPEXEC = 5;

		// Token: 0x0400105C RID: 4188
		public const ushort RPC_PROCID_CURSORUNPREPARE = 6;

		// Token: 0x0400105D RID: 4189
		public const ushort RPC_PROCID_CURSORFETCH = 7;

		// Token: 0x0400105E RID: 4190
		public const ushort RPC_PROCID_CURSOROPTION = 8;

		// Token: 0x0400105F RID: 4191
		public const ushort RPC_PROCID_CURSORCLOSE = 9;

		// Token: 0x04001060 RID: 4192
		public const ushort RPC_PROCID_EXECUTESQL = 10;

		// Token: 0x04001061 RID: 4193
		public const ushort RPC_PROCID_PREPARE = 11;

		// Token: 0x04001062 RID: 4194
		public const ushort RPC_PROCID_EXECUTE = 12;

		// Token: 0x04001063 RID: 4195
		public const ushort RPC_PROCID_PREPEXEC = 13;

		// Token: 0x04001064 RID: 4196
		public const ushort RPC_PROCID_PREPEXECRPC = 14;

		// Token: 0x04001065 RID: 4197
		public const ushort RPC_PROCID_UNPREPARE = 15;

		// Token: 0x04001066 RID: 4198
		public const string TRANS_BEGIN = "BEGIN TRANSACTION";

		// Token: 0x04001067 RID: 4199
		public const string TRANS_COMMIT = "COMMIT TRANSACTION";

		// Token: 0x04001068 RID: 4200
		public const string TRANS_ROLLBACK = "ROLLBACK TRANSACTION";

		// Token: 0x04001069 RID: 4201
		public const string TRANS_IF_ROLLBACK = "IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION";

		// Token: 0x0400106A RID: 4202
		public const string TRANS_SAVE = "SAVE TRANSACTION";

		// Token: 0x0400106B RID: 4203
		public const string TRANS_READ_COMMITTED = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";

		// Token: 0x0400106C RID: 4204
		public const string TRANS_READ_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

		// Token: 0x0400106D RID: 4205
		public const string TRANS_REPEATABLE_READ = "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ";

		// Token: 0x0400106E RID: 4206
		public const string TRANS_SERIALIZABLE = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";

		// Token: 0x0400106F RID: 4207
		public const string TRANS_SNAPSHOT = "SET TRANSACTION ISOLATION LEVEL SNAPSHOT";

		// Token: 0x04001070 RID: 4208
		public const byte SHILOH_RPCBATCHFLAG = 128;

		// Token: 0x04001071 RID: 4209
		public const byte YUKON_RPCBATCHFLAG = 255;

		// Token: 0x04001072 RID: 4210
		public const byte RPC_RECOMPILE = 1;

		// Token: 0x04001073 RID: 4211
		public const byte RPC_NOMETADATA = 2;

		// Token: 0x04001074 RID: 4212
		public const byte RPC_PARAM_BYREF = 1;

		// Token: 0x04001075 RID: 4213
		public const byte RPC_PARAM_DEFAULT = 2;

		// Token: 0x04001076 RID: 4214
		public const byte RPC_PARAM_IS_LOB_COOKIE = 8;

		// Token: 0x04001077 RID: 4215
		public const string PARAM_OUTPUT = "output";

		// Token: 0x04001078 RID: 4216
		public const int MAX_PARAMETER_NAME_LENGTH = 128;

		// Token: 0x04001079 RID: 4217
		public const string FMTONLY_ON = " SET FMTONLY ON;";

		// Token: 0x0400107A RID: 4218
		public const string FMTONLY_OFF = " SET FMTONLY OFF;";

		// Token: 0x0400107B RID: 4219
		public const string BROWSE_ON = " SET NO_BROWSETABLE ON;";

		// Token: 0x0400107C RID: 4220
		public const string BROWSE_OFF = " SET NO_BROWSETABLE OFF;";

		// Token: 0x0400107D RID: 4221
		public const string TABLE = "Table";

		// Token: 0x0400107E RID: 4222
		public const int EXEC_THRESHOLD = 3;

		// Token: 0x0400107F RID: 4223
		public const short TIMEOUT_EXPIRED = -2;

		// Token: 0x04001080 RID: 4224
		public const short ENCRYPTION_NOT_SUPPORTED = 20;

		// Token: 0x04001081 RID: 4225
		public const int LOGON_FAILED = 18456;

		// Token: 0x04001082 RID: 4226
		public const int PASSWORD_EXPIRED = 18488;

		// Token: 0x04001083 RID: 4227
		public const int IMPERSONATION_FAILED = 1346;

		// Token: 0x04001084 RID: 4228
		public const int P_TOKENTOOLONG = 103;

		// Token: 0x04001085 RID: 4229
		public const uint SNI_UNINITIALIZED = 4294967295U;

		// Token: 0x04001086 RID: 4230
		public const uint SNI_SUCCESS = 0U;

		// Token: 0x04001087 RID: 4231
		public const uint SNI_ERROR = 1U;

		// Token: 0x04001088 RID: 4232
		public const uint SNI_WAIT_TIMEOUT = 258U;

		// Token: 0x04001089 RID: 4233
		public const uint SNI_SUCCESS_IO_PENDING = 997U;

		// Token: 0x0400108A RID: 4234
		public const short SNI_WSAECONNRESET = 10054;

		// Token: 0x0400108B RID: 4235
		public const uint SNI_QUEUE_FULL = 1048576U;

		// Token: 0x0400108C RID: 4236
		public const uint SNI_SSL_VALIDATE_CERTIFICATE = 1U;

		// Token: 0x0400108D RID: 4237
		public const uint SNI_SSL_USE_SCHANNEL_CACHE = 2U;

		// Token: 0x0400108E RID: 4238
		public const uint SNI_SSL_IGNORE_CHANNEL_BINDINGS = 16U;

		// Token: 0x0400108F RID: 4239
		public const string DEFAULT_ENGLISH_CODE_PAGE_STRING = "iso_1";

		// Token: 0x04001090 RID: 4240
		public const short DEFAULT_ENGLISH_CODE_PAGE_VALUE = 1252;

		// Token: 0x04001091 RID: 4241
		public const short CHARSET_CODE_PAGE_OFFSET = 2;

		// Token: 0x04001092 RID: 4242
		internal const int MAX_SERVERNAME = 255;

		// Token: 0x04001093 RID: 4243
		internal const ushort SELECT = 193;

		// Token: 0x04001094 RID: 4244
		internal const ushort INSERT = 195;

		// Token: 0x04001095 RID: 4245
		internal const ushort DELETE = 196;

		// Token: 0x04001096 RID: 4246
		internal const ushort UPDATE = 197;

		// Token: 0x04001097 RID: 4247
		internal const ushort ABORT = 210;

		// Token: 0x04001098 RID: 4248
		internal const ushort BEGINXACT = 212;

		// Token: 0x04001099 RID: 4249
		internal const ushort ENDXACT = 213;

		// Token: 0x0400109A RID: 4250
		internal const ushort BULKINSERT = 240;

		// Token: 0x0400109B RID: 4251
		internal const ushort OPENCURSOR = 32;

		// Token: 0x0400109C RID: 4252
		internal const ushort MERGE = 279;

		// Token: 0x0400109D RID: 4253
		internal const ushort MAXLEN_HOSTNAME = 128;

		// Token: 0x0400109E RID: 4254
		internal const ushort MAXLEN_USERNAME = 128;

		// Token: 0x0400109F RID: 4255
		internal const ushort MAXLEN_PASSWORD = 128;

		// Token: 0x040010A0 RID: 4256
		internal const ushort MAXLEN_APPNAME = 128;

		// Token: 0x040010A1 RID: 4257
		internal const ushort MAXLEN_SERVERNAME = 128;

		// Token: 0x040010A2 RID: 4258
		internal const ushort MAXLEN_CLIENTINTERFACE = 128;

		// Token: 0x040010A3 RID: 4259
		internal const ushort MAXLEN_LANGUAGE = 128;

		// Token: 0x040010A4 RID: 4260
		internal const ushort MAXLEN_DATABASE = 128;

		// Token: 0x040010A5 RID: 4261
		internal const ushort MAXLEN_ATTACHDBFILE = 260;

		// Token: 0x040010A6 RID: 4262
		internal const ushort MAXLEN_NEWPASSWORD = 128;

		// Token: 0x040010A7 RID: 4263
		public static readonly ushort[] CODE_PAGE_FROM_SORT_ID = new ushort[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			437, 437, 437, 437, 437, 0, 0, 0, 0, 0,
			850, 850, 850, 850, 850, 0, 0, 0, 0, 850,
			1252, 1252, 1252, 1252, 1252, 850, 850, 850, 850, 850,
			850, 850, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1252, 1252, 1252, 1252, 1252, 0, 0, 0, 0,
			1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250,
			1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 0,
			0, 0, 0, 0, 1251, 1251, 1251, 1251, 1251, 0,
			0, 0, 1253, 1253, 1253, 0, 0, 0, 0, 0,
			1253, 1253, 1253, 0, 1253, 0, 0, 0, 1254, 1254,
			1254, 0, 0, 0, 0, 0, 1255, 1255, 1255, 0,
			0, 0, 0, 0, 1256, 1256, 1256, 0, 0, 0,
			0, 0, 1257, 1257, 1257, 1257, 1257, 1257, 1257, 1257,
			1257, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 1252, 1252, 1252, 1252, 0, 0, 0,
			0, 0, 932, 932, 949, 949, 950, 950, 936, 936,
			932, 949, 950, 936, 874, 874, 874, 0, 0, 0,
			1252, 1252, 1252, 1252, 1252, 1252, 1252, 1252, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0
		};

		// Token: 0x040010A8 RID: 4264
		internal static readonly long[] TICKS_FROM_SCALE = new long[] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

		// Token: 0x040010A9 RID: 4265
		internal const int WHIDBEY_DATE_LENGTH = 10;

		// Token: 0x040010AA RID: 4266
		internal static readonly int[] WHIDBEY_TIME_LENGTH = new int[] { 8, 10, 11, 12, 13, 14, 15, 16 };

		// Token: 0x040010AB RID: 4267
		internal static readonly int[] WHIDBEY_DATETIME2_LENGTH = new int[] { 19, 21, 22, 23, 24, 25, 26, 27 };

		// Token: 0x040010AC RID: 4268
		internal static readonly int[] WHIDBEY_DATETIMEOFFSET_LENGTH = new int[] { 26, 28, 29, 30, 31, 32, 33, 34 };

		// Token: 0x020001ED RID: 493
		public enum EnvChangeType : byte
		{
			// Token: 0x040010AE RID: 4270
			ENVCHANGE_DATABASE = 1,
			// Token: 0x040010AF RID: 4271
			ENVCHANGE_LANG,
			// Token: 0x040010B0 RID: 4272
			ENVCHANGE_CHARSET,
			// Token: 0x040010B1 RID: 4273
			ENVCHANGE_PACKETSIZE,
			// Token: 0x040010B2 RID: 4274
			ENVCHANGE_LOCALEID,
			// Token: 0x040010B3 RID: 4275
			ENVCHANGE_COMPFLAGS,
			// Token: 0x040010B4 RID: 4276
			ENVCHANGE_COLLATION,
			// Token: 0x040010B5 RID: 4277
			ENVCHANGE_BEGINTRAN,
			// Token: 0x040010B6 RID: 4278
			ENVCHANGE_COMMITTRAN,
			// Token: 0x040010B7 RID: 4279
			ENVCHANGE_ROLLBACKTRAN,
			// Token: 0x040010B8 RID: 4280
			ENVCHANGE_ENLISTDTC,
			// Token: 0x040010B9 RID: 4281
			ENVCHANGE_DEFECTDTC,
			// Token: 0x040010BA RID: 4282
			ENVCHANGE_LOGSHIPNODE,
			// Token: 0x040010BB RID: 4283
			ENVCHANGE_PROMOTETRANSACTION = 15,
			// Token: 0x040010BC RID: 4284
			ENVCHANGE_TRANSACTIONMANAGERADDRESS,
			// Token: 0x040010BD RID: 4285
			ENVCHANGE_TRANSACTIONENDED,
			// Token: 0x040010BE RID: 4286
			ENVCHANGE_SPRESETCONNECTIONACK,
			// Token: 0x040010BF RID: 4287
			ENVCHANGE_USERINSTANCE,
			// Token: 0x040010C0 RID: 4288
			ENVCHANGE_ROUTING
		}

		// Token: 0x020001EE RID: 494
		[Flags]
		public enum FeatureExtension : uint
		{
			// Token: 0x040010C2 RID: 4290
			None = 0U,
			// Token: 0x040010C3 RID: 4291
			SessionRecovery = 1U,
			// Token: 0x040010C4 RID: 4292
			FedAuth = 2U,
			// Token: 0x040010C5 RID: 4293
			GlobalTransactions = 8U
		}

		// Token: 0x020001EF RID: 495
		public enum FedAuthLibrary : byte
		{
			// Token: 0x040010C7 RID: 4295
			LiveId,
			// Token: 0x040010C8 RID: 4296
			SecurityToken,
			// Token: 0x040010C9 RID: 4297
			ADAL,
			// Token: 0x040010CA RID: 4298
			Default = 127
		}

		// Token: 0x020001F0 RID: 496
		internal enum TransactionManagerRequestType
		{
			// Token: 0x040010CC RID: 4300
			GetDTCAddress,
			// Token: 0x040010CD RID: 4301
			Propagate,
			// Token: 0x040010CE RID: 4302
			Begin = 5,
			// Token: 0x040010CF RID: 4303
			Promote,
			// Token: 0x040010D0 RID: 4304
			Commit,
			// Token: 0x040010D1 RID: 4305
			Rollback,
			// Token: 0x040010D2 RID: 4306
			Save
		}

		// Token: 0x020001F1 RID: 497
		internal enum TransactionManagerIsolationLevel
		{
			// Token: 0x040010D4 RID: 4308
			Unspecified,
			// Token: 0x040010D5 RID: 4309
			ReadUncommitted,
			// Token: 0x040010D6 RID: 4310
			ReadCommitted,
			// Token: 0x040010D7 RID: 4311
			RepeatableRead,
			// Token: 0x040010D8 RID: 4312
			Serializable,
			// Token: 0x040010D9 RID: 4313
			Snapshot
		}

		// Token: 0x020001F2 RID: 498
		internal enum GenericType
		{
			// Token: 0x040010DB RID: 4315
			MultiSet = 131
		}
	}
}
