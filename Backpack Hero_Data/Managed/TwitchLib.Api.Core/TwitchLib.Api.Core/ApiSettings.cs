using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core
{
	// Token: 0x02000003 RID: 3
	public class ApiSettings : IApiSettings, INotifyPropertyChanged
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000029A6 File Offset: 0x00000BA6
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000029AE File Offset: 0x00000BAE
		public string ClientId
		{
			get
			{
				return this._clientId;
			}
			set
			{
				if (value != this._clientId)
				{
					this._clientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000029D8 File Offset: 0x00000BD8
		public string Secret
		{
			get
			{
				return this._secret;
			}
			set
			{
				if (value != this._secret)
				{
					this._secret = value;
					this.NotifyPropertyChanged("Secret");
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000029FA File Offset: 0x00000BFA
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002A02 File Offset: 0x00000C02
		public string AccessToken
		{
			get
			{
				return this._accessToken;
			}
			set
			{
				if (value != this._accessToken)
				{
					this._accessToken = value;
					this.NotifyPropertyChanged("AccessToken");
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002A24 File Offset: 0x00000C24
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002A2C File Offset: 0x00000C2C
		public bool SkipDynamicScopeValidation
		{
			get
			{
				return this._skipDynamicScopeValidation;
			}
			set
			{
				if (value != this._skipDynamicScopeValidation)
				{
					this._skipDynamicScopeValidation = value;
					this.NotifyPropertyChanged("SkipDynamicScopeValidation");
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A49 File Offset: 0x00000C49
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002A51 File Offset: 0x00000C51
		public bool SkipAutoServerTokenGeneration
		{
			get
			{
				return this._skipAutoServerTokenGeneration;
			}
			set
			{
				if (value != this._skipAutoServerTokenGeneration)
				{
					this._skipAutoServerTokenGeneration = value;
					this.NotifyPropertyChanged("SkipAutoServerTokenGeneration");
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002A76 File Offset: 0x00000C76
		public List<AuthScopes> Scopes
		{
			get
			{
				return this._scopes;
			}
			set
			{
				if (value != this._scopes)
				{
					this._scopes = value;
					this.NotifyPropertyChanged("Scopes");
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000022 RID: 34 RVA: 0x00002A94 File Offset: 0x00000C94
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002ACC File Offset: 0x00000CCC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000024 RID: 36 RVA: 0x00002B01 File Offset: 0x00000D01
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x0400000A RID: 10
		private string _clientId;

		// Token: 0x0400000B RID: 11
		private string _secret;

		// Token: 0x0400000C RID: 12
		private string _accessToken;

		// Token: 0x0400000D RID: 13
		private bool _skipDynamicScopeValidation;

		// Token: 0x0400000E RID: 14
		private bool _skipAutoServerTokenGeneration;

		// Token: 0x0400000F RID: 15
		private List<AuthScopes> _scopes;
	}
}
