using System;
using Unity.Services.Analytics.Internal;

namespace Unity.Services.Analytics.Data
{
	// Token: 0x02000044 RID: 68
	internal interface IDataGenerator
	{
		// Token: 0x0600016C RID: 364
		void SetBuffer(IBuffer buffer);

		// Token: 0x0600016D RID: 365
		void GameRunning(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier);

		// Token: 0x0600016E RID: 366
		void SdkStartup(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier);

		// Token: 0x0600016F RID: 367
		void NewPlayer(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string deviceModel);

		// Token: 0x06000170 RID: 368
		void GameStarted(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string idLocalProject, string osVersion, bool isTiny, bool debugDevice, string userLocale);

		// Token: 0x06000171 RID: 369
		void GameEnded(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, DataGenerator.SessionEndState quitState);

		// Token: 0x06000172 RID: 370
		void AdImpression(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, AdImpressionParameters adImpressionParameters);

		// Token: 0x06000173 RID: 371
		void Transaction(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, TransactionParameters transactionParameters);

		// Token: 0x06000174 RID: 372
		void TransactionFailed(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, TransactionFailedParameters transactionParameters);

		// Token: 0x06000175 RID: 373
		void ClientDevice(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, string cpuType, string gpuType, long cpuCores, long ramTotal, long screenWidth, long screenHeight, long screenDPI);

		// Token: 0x06000176 RID: 374
		void AcquisitionSource(DateTime datetime, StdCommonParams commonParams, string callingMethodIdentifier, AcquisitionSourceParameters acquisitionSourceParameters);
	}
}
