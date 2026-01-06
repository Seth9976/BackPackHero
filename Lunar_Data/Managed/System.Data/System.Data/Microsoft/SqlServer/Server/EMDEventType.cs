using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003CB RID: 971
	internal enum EMDEventType
	{
		// Token: 0x04001BE2 RID: 7138
		x_eet_Invalid,
		// Token: 0x04001BE3 RID: 7139
		x_eet_Insert,
		// Token: 0x04001BE4 RID: 7140
		x_eet_Update,
		// Token: 0x04001BE5 RID: 7141
		x_eet_Delete,
		// Token: 0x04001BE6 RID: 7142
		x_eet_Create_Table = 21,
		// Token: 0x04001BE7 RID: 7143
		x_eet_Alter_Table,
		// Token: 0x04001BE8 RID: 7144
		x_eet_Drop_Table,
		// Token: 0x04001BE9 RID: 7145
		x_eet_Create_Index,
		// Token: 0x04001BEA RID: 7146
		x_eet_Alter_Index,
		// Token: 0x04001BEB RID: 7147
		x_eet_Drop_Index,
		// Token: 0x04001BEC RID: 7148
		x_eet_Create_Stats,
		// Token: 0x04001BED RID: 7149
		x_eet_Update_Stats,
		// Token: 0x04001BEE RID: 7150
		x_eet_Drop_Stats,
		// Token: 0x04001BEF RID: 7151
		x_eet_Create_Secexpr = 31,
		// Token: 0x04001BF0 RID: 7152
		x_eet_Drop_Secexpr = 33,
		// Token: 0x04001BF1 RID: 7153
		x_eet_Create_Synonym,
		// Token: 0x04001BF2 RID: 7154
		x_eet_Drop_Synonym = 36,
		// Token: 0x04001BF3 RID: 7155
		x_eet_Create_View = 41,
		// Token: 0x04001BF4 RID: 7156
		x_eet_Alter_View,
		// Token: 0x04001BF5 RID: 7157
		x_eet_Drop_View,
		// Token: 0x04001BF6 RID: 7158
		x_eet_Create_Procedure = 51,
		// Token: 0x04001BF7 RID: 7159
		x_eet_Alter_Procedure,
		// Token: 0x04001BF8 RID: 7160
		x_eet_Drop_Procedure,
		// Token: 0x04001BF9 RID: 7161
		x_eet_Create_Function = 61,
		// Token: 0x04001BFA RID: 7162
		x_eet_Alter_Function,
		// Token: 0x04001BFB RID: 7163
		x_eet_Drop_Function,
		// Token: 0x04001BFC RID: 7164
		x_eet_Create_Trigger = 71,
		// Token: 0x04001BFD RID: 7165
		x_eet_Alter_Trigger,
		// Token: 0x04001BFE RID: 7166
		x_eet_Drop_Trigger,
		// Token: 0x04001BFF RID: 7167
		x_eet_Create_Event_Notification,
		// Token: 0x04001C00 RID: 7168
		x_eet_Drop_Event_Notification = 76,
		// Token: 0x04001C01 RID: 7169
		x_eet_Create_Type = 91,
		// Token: 0x04001C02 RID: 7170
		x_eet_Drop_Type = 93,
		// Token: 0x04001C03 RID: 7171
		x_eet_Create_Assembly = 101,
		// Token: 0x04001C04 RID: 7172
		x_eet_Alter_Assembly,
		// Token: 0x04001C05 RID: 7173
		x_eet_Drop_Assembly,
		// Token: 0x04001C06 RID: 7174
		x_eet_Create_User = 131,
		// Token: 0x04001C07 RID: 7175
		x_eet_Alter_User,
		// Token: 0x04001C08 RID: 7176
		x_eet_Drop_User,
		// Token: 0x04001C09 RID: 7177
		x_eet_Create_Role,
		// Token: 0x04001C0A RID: 7178
		x_eet_Alter_Role,
		// Token: 0x04001C0B RID: 7179
		x_eet_Drop_Role,
		// Token: 0x04001C0C RID: 7180
		x_eet_Create_AppRole,
		// Token: 0x04001C0D RID: 7181
		x_eet_Alter_AppRole,
		// Token: 0x04001C0E RID: 7182
		x_eet_Drop_AppRole,
		// Token: 0x04001C0F RID: 7183
		x_eet_Create_Schema = 141,
		// Token: 0x04001C10 RID: 7184
		x_eet_Alter_Schema,
		// Token: 0x04001C11 RID: 7185
		x_eet_Drop_Schema,
		// Token: 0x04001C12 RID: 7186
		x_eet_Create_Login,
		// Token: 0x04001C13 RID: 7187
		x_eet_Alter_Login,
		// Token: 0x04001C14 RID: 7188
		x_eet_Drop_Login,
		// Token: 0x04001C15 RID: 7189
		x_eet_Create_MsgType = 151,
		// Token: 0x04001C16 RID: 7190
		x_eet_Alter_MsgType,
		// Token: 0x04001C17 RID: 7191
		x_eet_Drop_MsgType,
		// Token: 0x04001C18 RID: 7192
		x_eet_Create_Contract,
		// Token: 0x04001C19 RID: 7193
		x_eet_Alter_Contract,
		// Token: 0x04001C1A RID: 7194
		x_eet_Drop_Contract,
		// Token: 0x04001C1B RID: 7195
		x_eet_Create_Queue,
		// Token: 0x04001C1C RID: 7196
		x_eet_Alter_Queue,
		// Token: 0x04001C1D RID: 7197
		x_eet_Drop_Queue,
		// Token: 0x04001C1E RID: 7198
		x_eet_Create_Service = 161,
		// Token: 0x04001C1F RID: 7199
		x_eet_Alter_Service,
		// Token: 0x04001C20 RID: 7200
		x_eet_Drop_Service,
		// Token: 0x04001C21 RID: 7201
		x_eet_Create_Route,
		// Token: 0x04001C22 RID: 7202
		x_eet_Alter_Route,
		// Token: 0x04001C23 RID: 7203
		x_eet_Drop_Route,
		// Token: 0x04001C24 RID: 7204
		x_eet_Grant_Statement,
		// Token: 0x04001C25 RID: 7205
		x_eet_Deny_Statement,
		// Token: 0x04001C26 RID: 7206
		x_eet_Revoke_Statement,
		// Token: 0x04001C27 RID: 7207
		x_eet_Grant_Object,
		// Token: 0x04001C28 RID: 7208
		x_eet_Deny_Object,
		// Token: 0x04001C29 RID: 7209
		x_eet_Revoke_Object,
		// Token: 0x04001C2A RID: 7210
		x_eet_Activation,
		// Token: 0x04001C2B RID: 7211
		x_eet_Create_Binding,
		// Token: 0x04001C2C RID: 7212
		x_eet_Alter_Binding,
		// Token: 0x04001C2D RID: 7213
		x_eet_Drop_Binding,
		// Token: 0x04001C2E RID: 7214
		x_eet_Create_XmlSchema,
		// Token: 0x04001C2F RID: 7215
		x_eet_Alter_XmlSchema,
		// Token: 0x04001C30 RID: 7216
		x_eet_Drop_XmlSchema,
		// Token: 0x04001C31 RID: 7217
		x_eet_Create_HttpEndpoint = 181,
		// Token: 0x04001C32 RID: 7218
		x_eet_Alter_HttpEndpoint,
		// Token: 0x04001C33 RID: 7219
		x_eet_Drop_HttpEndpoint,
		// Token: 0x04001C34 RID: 7220
		x_eet_Create_Partition_Function = 191,
		// Token: 0x04001C35 RID: 7221
		x_eet_Alter_Partition_Function,
		// Token: 0x04001C36 RID: 7222
		x_eet_Drop_Partition_Function,
		// Token: 0x04001C37 RID: 7223
		x_eet_Create_Partition_Scheme,
		// Token: 0x04001C38 RID: 7224
		x_eet_Alter_Partition_Scheme,
		// Token: 0x04001C39 RID: 7225
		x_eet_Drop_Partition_Scheme,
		// Token: 0x04001C3A RID: 7226
		x_eet_Create_Database = 201,
		// Token: 0x04001C3B RID: 7227
		x_eet_Alter_Database,
		// Token: 0x04001C3C RID: 7228
		x_eet_Drop_Database,
		// Token: 0x04001C3D RID: 7229
		x_eet_Trace_Start = 1000,
		// Token: 0x04001C3E RID: 7230
		x_eet_Trace_End = 1999
	}
}
