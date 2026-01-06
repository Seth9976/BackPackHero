using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001F4 RID: 500
	internal enum ParsingErrorState
	{
		// Token: 0x040010EC RID: 4332
		Undefined,
		// Token: 0x040010ED RID: 4333
		FedAuthInfoLengthTooShortForCountOfInfoIds,
		// Token: 0x040010EE RID: 4334
		FedAuthInfoLengthTooShortForData,
		// Token: 0x040010EF RID: 4335
		FedAuthInfoFailedToReadCountOfInfoIds,
		// Token: 0x040010F0 RID: 4336
		FedAuthInfoFailedToReadTokenStream,
		// Token: 0x040010F1 RID: 4337
		FedAuthInfoInvalidOffset,
		// Token: 0x040010F2 RID: 4338
		FedAuthInfoFailedToReadData,
		// Token: 0x040010F3 RID: 4339
		FedAuthInfoDataNotUnicode,
		// Token: 0x040010F4 RID: 4340
		FedAuthInfoDoesNotContainStsurlAndSpn,
		// Token: 0x040010F5 RID: 4341
		FedAuthInfoNotReceived,
		// Token: 0x040010F6 RID: 4342
		FedAuthNotAcknowledged,
		// Token: 0x040010F7 RID: 4343
		FedAuthFeatureAckContainsExtraData,
		// Token: 0x040010F8 RID: 4344
		FedAuthFeatureAckUnknownLibraryType,
		// Token: 0x040010F9 RID: 4345
		UnrequestedFeatureAckReceived,
		// Token: 0x040010FA RID: 4346
		UnknownFeatureAck,
		// Token: 0x040010FB RID: 4347
		InvalidTdsTokenReceived,
		// Token: 0x040010FC RID: 4348
		SessionStateLengthTooShort,
		// Token: 0x040010FD RID: 4349
		SessionStateInvalidStatus,
		// Token: 0x040010FE RID: 4350
		CorruptedTdsStream,
		// Token: 0x040010FF RID: 4351
		ProcessSniPacketFailed,
		// Token: 0x04001100 RID: 4352
		FedAuthRequiredPreLoginResponseInvalidValue
	}
}
