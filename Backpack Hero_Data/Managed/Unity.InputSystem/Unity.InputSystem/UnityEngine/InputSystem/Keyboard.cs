using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003B RID: 59
	[InputControlLayout(stateType = typeof(KeyboardState), isGenericTypeOfDevice = true)]
	public class Keyboard : InputDevice, ITextInputReceiver
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060004D6 RID: 1238 RVA: 0x00013F9A File Offset: 0x0001219A
		// (remove) Token: 0x060004D7 RID: 1239 RVA: 0x00013FC5 File Offset: 0x000121C5
		public event Action<char> onTextInput
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_TextInputListeners.Contains(value))
				{
					this.m_TextInputListeners.Append(value);
				}
			}
			remove
			{
				this.m_TextInputListeners.Remove(value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060004D8 RID: 1240 RVA: 0x00013FD3 File Offset: 0x000121D3
		// (remove) Token: 0x060004D9 RID: 1241 RVA: 0x00013FFE File Offset: 0x000121FE
		public event Action<IMECompositionString> onIMECompositionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_ImeCompositionListeners.Contains(value))
				{
					this.m_ImeCompositionListeners.Append(value);
				}
			}
			remove
			{
				this.m_ImeCompositionListeners.Remove(value);
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001400C File Offset: 0x0001220C
		public void SetIMEEnabled(bool enabled)
		{
			EnableIMECompositionCommand enableIMECompositionCommand = EnableIMECompositionCommand.Create(enabled);
			base.ExecuteCommand<EnableIMECompositionCommand>(ref enableIMECompositionCommand);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001402C File Offset: 0x0001222C
		public void SetIMECursorPosition(Vector2 position)
		{
			SetIMECursorPositionCommand setIMECursorPositionCommand = SetIMECursorPositionCommand.Create(position);
			base.ExecuteCommand<SetIMECursorPositionCommand>(ref setIMECursorPositionCommand);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00014049 File Offset: 0x00012249
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00014057 File Offset: 0x00012257
		public string keyboardLayout
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_KeyboardLayoutName;
			}
			protected set
			{
				this.m_KeyboardLayoutName = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00014060 File Offset: 0x00012260
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00014068 File Offset: 0x00012268
		public AnyKeyControl anyKey { get; protected set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00014071 File Offset: 0x00012271
		public KeyControl spaceKey
		{
			get
			{
				return this[Key.Space];
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001407A File Offset: 0x0001227A
		public KeyControl enterKey
		{
			get
			{
				return this[Key.Enter];
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00014083 File Offset: 0x00012283
		public KeyControl tabKey
		{
			get
			{
				return this[Key.Tab];
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001408C File Offset: 0x0001228C
		public KeyControl backquoteKey
		{
			get
			{
				return this[Key.Backquote];
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00014095 File Offset: 0x00012295
		public KeyControl quoteKey
		{
			get
			{
				return this[Key.Quote];
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001409E File Offset: 0x0001229E
		public KeyControl semicolonKey
		{
			get
			{
				return this[Key.Semicolon];
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000140A7 File Offset: 0x000122A7
		public KeyControl commaKey
		{
			get
			{
				return this[Key.Comma];
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000140B0 File Offset: 0x000122B0
		public KeyControl periodKey
		{
			get
			{
				return this[Key.Period];
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000140B9 File Offset: 0x000122B9
		public KeyControl slashKey
		{
			get
			{
				return this[Key.Slash];
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000140C3 File Offset: 0x000122C3
		public KeyControl backslashKey
		{
			get
			{
				return this[Key.Backslash];
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000140CD File Offset: 0x000122CD
		public KeyControl leftBracketKey
		{
			get
			{
				return this[Key.LeftBracket];
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000140D7 File Offset: 0x000122D7
		public KeyControl rightBracketKey
		{
			get
			{
				return this[Key.RightBracket];
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000140E1 File Offset: 0x000122E1
		public KeyControl minusKey
		{
			get
			{
				return this[Key.Minus];
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000140EB File Offset: 0x000122EB
		public KeyControl equalsKey
		{
			get
			{
				return this[Key.Equals];
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000140F5 File Offset: 0x000122F5
		public KeyControl aKey
		{
			get
			{
				return this[Key.A];
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000140FF File Offset: 0x000122FF
		public KeyControl bKey
		{
			get
			{
				return this[Key.B];
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00014109 File Offset: 0x00012309
		public KeyControl cKey
		{
			get
			{
				return this[Key.C];
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00014113 File Offset: 0x00012313
		public KeyControl dKey
		{
			get
			{
				return this[Key.D];
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001411D File Offset: 0x0001231D
		public KeyControl eKey
		{
			get
			{
				return this[Key.E];
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00014127 File Offset: 0x00012327
		public KeyControl fKey
		{
			get
			{
				return this[Key.F];
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00014131 File Offset: 0x00012331
		public KeyControl gKey
		{
			get
			{
				return this[Key.G];
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001413B File Offset: 0x0001233B
		public KeyControl hKey
		{
			get
			{
				return this[Key.H];
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014145 File Offset: 0x00012345
		public KeyControl iKey
		{
			get
			{
				return this[Key.I];
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001414F File Offset: 0x0001234F
		public KeyControl jKey
		{
			get
			{
				return this[Key.J];
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00014159 File Offset: 0x00012359
		public KeyControl kKey
		{
			get
			{
				return this[Key.K];
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00014163 File Offset: 0x00012363
		public KeyControl lKey
		{
			get
			{
				return this[Key.L];
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001416D File Offset: 0x0001236D
		public KeyControl mKey
		{
			get
			{
				return this[Key.M];
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014177 File Offset: 0x00012377
		public KeyControl nKey
		{
			get
			{
				return this[Key.N];
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00014181 File Offset: 0x00012381
		public KeyControl oKey
		{
			get
			{
				return this[Key.O];
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001418B File Offset: 0x0001238B
		public KeyControl pKey
		{
			get
			{
				return this[Key.P];
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00014195 File Offset: 0x00012395
		public KeyControl qKey
		{
			get
			{
				return this[Key.Q];
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001419F File Offset: 0x0001239F
		public KeyControl rKey
		{
			get
			{
				return this[Key.R];
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000141A9 File Offset: 0x000123A9
		public KeyControl sKey
		{
			get
			{
				return this[Key.S];
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000141B3 File Offset: 0x000123B3
		public KeyControl tKey
		{
			get
			{
				return this[Key.T];
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000141BD File Offset: 0x000123BD
		public KeyControl uKey
		{
			get
			{
				return this[Key.U];
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000141C7 File Offset: 0x000123C7
		public KeyControl vKey
		{
			get
			{
				return this[Key.V];
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000141D1 File Offset: 0x000123D1
		public KeyControl wKey
		{
			get
			{
				return this[Key.W];
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000141DB File Offset: 0x000123DB
		public KeyControl xKey
		{
			get
			{
				return this[Key.X];
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000141E5 File Offset: 0x000123E5
		public KeyControl yKey
		{
			get
			{
				return this[Key.Y];
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000141EF File Offset: 0x000123EF
		public KeyControl zKey
		{
			get
			{
				return this[Key.Z];
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000141F9 File Offset: 0x000123F9
		public KeyControl digit1Key
		{
			get
			{
				return this[Key.Digit1];
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00014203 File Offset: 0x00012403
		public KeyControl digit2Key
		{
			get
			{
				return this[Key.Digit2];
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001420D File Offset: 0x0001240D
		public KeyControl digit3Key
		{
			get
			{
				return this[Key.Digit3];
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00014217 File Offset: 0x00012417
		public KeyControl digit4Key
		{
			get
			{
				return this[Key.Digit4];
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00014221 File Offset: 0x00012421
		public KeyControl digit5Key
		{
			get
			{
				return this[Key.Digit5];
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001422B File Offset: 0x0001242B
		public KeyControl digit6Key
		{
			get
			{
				return this[Key.Digit6];
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00014235 File Offset: 0x00012435
		public KeyControl digit7Key
		{
			get
			{
				return this[Key.Digit7];
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001423F File Offset: 0x0001243F
		public KeyControl digit8Key
		{
			get
			{
				return this[Key.Digit8];
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00014249 File Offset: 0x00012449
		public KeyControl digit9Key
		{
			get
			{
				return this[Key.Digit9];
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00014253 File Offset: 0x00012453
		public KeyControl digit0Key
		{
			get
			{
				return this[Key.Digit0];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001425D File Offset: 0x0001245D
		public KeyControl leftShiftKey
		{
			get
			{
				return this[Key.LeftShift];
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00014267 File Offset: 0x00012467
		public KeyControl rightShiftKey
		{
			get
			{
				return this[Key.RightShift];
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00014271 File Offset: 0x00012471
		public KeyControl leftAltKey
		{
			get
			{
				return this[Key.LeftAlt];
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001427B File Offset: 0x0001247B
		public KeyControl rightAltKey
		{
			get
			{
				return this[Key.RightAlt];
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00014285 File Offset: 0x00012485
		public KeyControl leftCtrlKey
		{
			get
			{
				return this[Key.LeftCtrl];
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001428F File Offset: 0x0001248F
		public KeyControl rightCtrlKey
		{
			get
			{
				return this[Key.RightCtrl];
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00014299 File Offset: 0x00012499
		public KeyControl leftMetaKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000142A3 File Offset: 0x000124A3
		public KeyControl rightMetaKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000142AD File Offset: 0x000124AD
		public KeyControl leftWindowsKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000142B7 File Offset: 0x000124B7
		public KeyControl rightWindowsKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000142C1 File Offset: 0x000124C1
		public KeyControl leftAppleKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000142CB File Offset: 0x000124CB
		public KeyControl rightAppleKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000142D5 File Offset: 0x000124D5
		public KeyControl leftCommandKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000142DF File Offset: 0x000124DF
		public KeyControl rightCommandKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000142E9 File Offset: 0x000124E9
		public KeyControl contextMenuKey
		{
			get
			{
				return this[Key.ContextMenu];
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000142F3 File Offset: 0x000124F3
		public KeyControl escapeKey
		{
			get
			{
				return this[Key.Escape];
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000142FD File Offset: 0x000124FD
		public KeyControl leftArrowKey
		{
			get
			{
				return this[Key.LeftArrow];
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00014307 File Offset: 0x00012507
		public KeyControl rightArrowKey
		{
			get
			{
				return this[Key.RightArrow];
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00014311 File Offset: 0x00012511
		public KeyControl upArrowKey
		{
			get
			{
				return this[Key.UpArrow];
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001431B File Offset: 0x0001251B
		public KeyControl downArrowKey
		{
			get
			{
				return this[Key.DownArrow];
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00014325 File Offset: 0x00012525
		public KeyControl backspaceKey
		{
			get
			{
				return this[Key.Backspace];
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001432F File Offset: 0x0001252F
		public KeyControl pageDownKey
		{
			get
			{
				return this[Key.PageDown];
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00014339 File Offset: 0x00012539
		public KeyControl pageUpKey
		{
			get
			{
				return this[Key.PageUp];
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00014343 File Offset: 0x00012543
		public KeyControl homeKey
		{
			get
			{
				return this[Key.Home];
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001434D File Offset: 0x0001254D
		public KeyControl endKey
		{
			get
			{
				return this[Key.End];
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00014357 File Offset: 0x00012557
		public KeyControl insertKey
		{
			get
			{
				return this[Key.Insert];
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00014361 File Offset: 0x00012561
		public KeyControl deleteKey
		{
			get
			{
				return this[Key.Delete];
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001436B File Offset: 0x0001256B
		public KeyControl capsLockKey
		{
			get
			{
				return this[Key.CapsLock];
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00014375 File Offset: 0x00012575
		public KeyControl scrollLockKey
		{
			get
			{
				return this[Key.ScrollLock];
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001437F File Offset: 0x0001257F
		public KeyControl numLockKey
		{
			get
			{
				return this[Key.NumLock];
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00014389 File Offset: 0x00012589
		public KeyControl printScreenKey
		{
			get
			{
				return this[Key.PrintScreen];
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00014393 File Offset: 0x00012593
		public KeyControl pauseKey
		{
			get
			{
				return this[Key.Pause];
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001439D File Offset: 0x0001259D
		public KeyControl numpadEnterKey
		{
			get
			{
				return this[Key.NumpadEnter];
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000143A7 File Offset: 0x000125A7
		public KeyControl numpadDivideKey
		{
			get
			{
				return this[Key.NumpadDivide];
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000143B1 File Offset: 0x000125B1
		public KeyControl numpadMultiplyKey
		{
			get
			{
				return this[Key.NumpadMultiply];
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000143BB File Offset: 0x000125BB
		public KeyControl numpadMinusKey
		{
			get
			{
				return this[Key.NumpadMinus];
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000143C5 File Offset: 0x000125C5
		public KeyControl numpadPlusKey
		{
			get
			{
				return this[Key.NumpadPlus];
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000143CF File Offset: 0x000125CF
		public KeyControl numpadPeriodKey
		{
			get
			{
				return this[Key.NumpadPeriod];
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000143D9 File Offset: 0x000125D9
		public KeyControl numpadEqualsKey
		{
			get
			{
				return this[Key.NumpadEquals];
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000143E3 File Offset: 0x000125E3
		public KeyControl numpad0Key
		{
			get
			{
				return this[Key.Numpad0];
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x000143ED File Offset: 0x000125ED
		public KeyControl numpad1Key
		{
			get
			{
				return this[Key.Numpad1];
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000143F7 File Offset: 0x000125F7
		public KeyControl numpad2Key
		{
			get
			{
				return this[Key.Numpad2];
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00014401 File Offset: 0x00012601
		public KeyControl numpad3Key
		{
			get
			{
				return this[Key.Numpad3];
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001440B File Offset: 0x0001260B
		public KeyControl numpad4Key
		{
			get
			{
				return this[Key.Numpad4];
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00014415 File Offset: 0x00012615
		public KeyControl numpad5Key
		{
			get
			{
				return this[Key.Numpad5];
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001441F File Offset: 0x0001261F
		public KeyControl numpad6Key
		{
			get
			{
				return this[Key.Numpad6];
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00014429 File Offset: 0x00012629
		public KeyControl numpad7Key
		{
			get
			{
				return this[Key.Numpad7];
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00014433 File Offset: 0x00012633
		public KeyControl numpad8Key
		{
			get
			{
				return this[Key.Numpad8];
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001443D File Offset: 0x0001263D
		public KeyControl numpad9Key
		{
			get
			{
				return this[Key.Numpad9];
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00014447 File Offset: 0x00012647
		public KeyControl f1Key
		{
			get
			{
				return this[Key.F1];
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00014451 File Offset: 0x00012651
		public KeyControl f2Key
		{
			get
			{
				return this[Key.F2];
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001445B File Offset: 0x0001265B
		public KeyControl f3Key
		{
			get
			{
				return this[Key.F3];
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00014465 File Offset: 0x00012665
		public KeyControl f4Key
		{
			get
			{
				return this[Key.F4];
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001446F File Offset: 0x0001266F
		public KeyControl f5Key
		{
			get
			{
				return this[Key.F5];
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014479 File Offset: 0x00012679
		public KeyControl f6Key
		{
			get
			{
				return this[Key.F6];
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00014483 File Offset: 0x00012683
		public KeyControl f7Key
		{
			get
			{
				return this[Key.F7];
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001448D File Offset: 0x0001268D
		public KeyControl f8Key
		{
			get
			{
				return this[Key.F8];
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00014497 File Offset: 0x00012697
		public KeyControl f9Key
		{
			get
			{
				return this[Key.F9];
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000144A1 File Offset: 0x000126A1
		public KeyControl f10Key
		{
			get
			{
				return this[Key.F10];
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000144AB File Offset: 0x000126AB
		public KeyControl f11Key
		{
			get
			{
				return this[Key.F11];
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000144B5 File Offset: 0x000126B5
		public KeyControl f12Key
		{
			get
			{
				return this[Key.F12];
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000144BF File Offset: 0x000126BF
		public KeyControl oem1Key
		{
			get
			{
				return this[Key.OEM1];
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000144C9 File Offset: 0x000126C9
		public KeyControl oem2Key
		{
			get
			{
				return this[Key.OEM2];
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000144D3 File Offset: 0x000126D3
		public KeyControl oem3Key
		{
			get
			{
				return this[Key.OEM3];
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000144DD File Offset: 0x000126DD
		public KeyControl oem4Key
		{
			get
			{
				return this[Key.OEM4];
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000144E7 File Offset: 0x000126E7
		public KeyControl oem5Key
		{
			get
			{
				return this[Key.OEM5];
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000144F1 File Offset: 0x000126F1
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x000144F9 File Offset: 0x000126F9
		public ButtonControl shiftKey { get; protected set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00014502 File Offset: 0x00012702
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0001450A File Offset: 0x0001270A
		public ButtonControl ctrlKey { get; protected set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00014513 File Offset: 0x00012713
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001451B File Offset: 0x0001271B
		public ButtonControl altKey { get; protected set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00014524 File Offset: 0x00012724
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001452C File Offset: 0x0001272C
		public ButtonControl imeSelected { get; protected set; }

		// Token: 0x170001CC RID: 460
		public KeyControl this[Key key]
		{
			get
			{
				int num = key - Key.Space;
				if (num < 0 || num >= this.m_Keys.Length)
				{
					throw new ArgumentOutOfRangeException("key");
				}
				return this.m_Keys[num];
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001456B File Offset: 0x0001276B
		public ReadOnlyArray<KeyControl> allKeys
		{
			get
			{
				return new ReadOnlyArray<KeyControl>(this.m_Keys);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00014578 File Offset: 0x00012778
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001457F File Offset: 0x0001277F
		public static Keyboard current { get; private set; }

		// Token: 0x06000560 RID: 1376 RVA: 0x00014587 File Offset: 0x00012787
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Keyboard.current = this;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00014595 File Offset: 0x00012795
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Keyboard.current == this)
			{
				Keyboard.current = null;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000145AC File Offset: 0x000127AC
		protected override void FinishSetup()
		{
			string[] array = new string[]
			{
				"space", "enter", "tab", "backquote", "quote", "semicolon", "comma", "period", "slash", "backslash",
				"leftbracket", "rightbracket", "minus", "equals", "a", "b", "c", "d", "e", "f",
				"g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
				"q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
				"leftshift", "rightshift", "leftalt", "rightalt", "leftctrl", "rightctrl", "leftmeta", "rightmeta", "contextmenu", "escape",
				"leftarrow", "rightarrow", "uparrow", "downarrow", "backspace", "pagedown", "pageup", "home", "end", "insert",
				"delete", "capslock", "numlock", "printscreen", "scrolllock", "pause", "numpadenter", "numpaddivide", "numpadmultiply", "numpadplus",
				"numpadminus", "numpadperiod", "numpadequals", "numpad0", "numpad1", "numpad2", "numpad3", "numpad4", "numpad5", "numpad6",
				"numpad7", "numpad8", "numpad9", "f1", "f2", "f3", "f4", "f5", "f6", "f7",
				"f8", "f9", "f10", "f11", "f12", "oem1", "oem2", "oem3", "oem4", "oem5"
			};
			this.m_Keys = new KeyControl[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.m_Keys[i] = base.GetChildControl<KeyControl>(array[i]);
				this.m_Keys[i].keyCode = i + Key.Space;
			}
			this.anyKey = base.GetChildControl<AnyKeyControl>("anyKey");
			this.shiftKey = base.GetChildControl<ButtonControl>("shift");
			this.ctrlKey = base.GetChildControl<ButtonControl>("ctrl");
			this.altKey = base.GetChildControl<ButtonControl>("alt");
			this.imeSelected = base.GetChildControl<ButtonControl>("IMESelected");
			base.FinishSetup();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00014A30 File Offset: 0x00012C30
		protected override void RefreshConfiguration()
		{
			this.keyboardLayout = null;
			QueryKeyboardLayoutCommand queryKeyboardLayoutCommand = QueryKeyboardLayoutCommand.Create();
			if (base.ExecuteCommand<QueryKeyboardLayoutCommand>(ref queryKeyboardLayoutCommand) >= 0L)
			{
				this.keyboardLayout = queryKeyboardLayoutCommand.ReadLayoutName();
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00014A64 File Offset: 0x00012C64
		public void OnTextInput(char character)
		{
			for (int i = 0; i < this.m_TextInputListeners.length; i++)
			{
				this.m_TextInputListeners[i](character);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014A9C File Offset: 0x00012C9C
		public KeyControl FindKeyOnCurrentKeyboardLayout(string displayName)
		{
			ReadOnlyArray<KeyControl> allKeys = this.allKeys;
			for (int i = 0; i < allKeys.Count; i++)
			{
				if (string.Equals(allKeys[i].displayName, displayName, StringComparison.CurrentCultureIgnoreCase))
				{
					return allKeys[i];
				}
			}
			return null;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014AE4 File Offset: 0x00012CE4
		public void OnIMECompositionChanged(IMECompositionString compositionString)
		{
			if (this.m_ImeCompositionListeners.length > 0)
			{
				for (int i = 0; i < this.m_ImeCompositionListeners.length; i++)
				{
					this.m_ImeCompositionListeners[i](compositionString);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00014B27 File Offset: 0x00012D27
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00014B2F File Offset: 0x00012D2F
		protected KeyControl[] keys
		{
			get
			{
				return this.m_Keys;
			}
			set
			{
				this.m_Keys = value;
			}
		}

		// Token: 0x040001F3 RID: 499
		public const int KeyCount = 110;

		// Token: 0x040001FA RID: 506
		private InlinedArray<Action<char>> m_TextInputListeners;

		// Token: 0x040001FB RID: 507
		private string m_KeyboardLayoutName;

		// Token: 0x040001FC RID: 508
		private KeyControl[] m_Keys;

		// Token: 0x040001FD RID: 509
		private InlinedArray<Action<IMECompositionString>> m_ImeCompositionListeners;
	}
}
