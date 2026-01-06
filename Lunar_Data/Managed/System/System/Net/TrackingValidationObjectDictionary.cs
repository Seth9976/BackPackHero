using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000389 RID: 905
	internal sealed class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0006C63B File Offset: 0x0006A83B
		internal TrackingValidationObjectDictionary(Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this._validators = validators;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0006C654 File Offset: 0x0006A854
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				TrackingValidationObjectDictionary.ValidateAndParseValue validateAndParseValue;
				if (this._validators != null && this._validators.TryGetValue(key, out validateAndParseValue))
				{
					object obj = validateAndParseValue(value);
					if (this._internalObjects == null)
					{
						this._internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this._internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this._internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0006C6FE File Offset: 0x0006A8FE
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0006C706 File Offset: 0x0006A906
		internal bool IsChanged { get; set; }

		// Token: 0x06001DBB RID: 7611 RVA: 0x0006C710 File Offset: 0x0006A910
		internal object InternalGet(string key)
		{
			object obj;
			if (this._internalObjects != null && this._internalObjects.TryGetValue(key, out obj))
			{
				return obj;
			}
			return base[key];
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0006C73E File Offset: 0x0006A93E
		internal void InternalSet(string key, object value)
		{
			if (this._internalObjects == null)
			{
				this._internalObjects = new Dictionary<string, object>();
			}
			this._internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x170005E3 RID: 1507
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0006C77F File Offset: 0x0006A97F
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0006C78A File Offset: 0x0006A98A
		public override void Clear()
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0006C7AC File Offset: 0x0006A9AC
		public override void Remove(string key)
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x04000F69 RID: 3945
		private readonly Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> _validators;

		// Token: 0x04000F6A RID: 3946
		private Dictionary<string, object> _internalObjects;

		// Token: 0x0200038A RID: 906
		// (Invoke) Token: 0x06001DC3 RID: 7619
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
