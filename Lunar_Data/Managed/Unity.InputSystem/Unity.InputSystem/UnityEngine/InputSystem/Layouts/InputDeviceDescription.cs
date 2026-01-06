using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public struct InputDeviceDescription : IEquatable<InputDeviceDescription>
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0004AAE6 File Offset: 0x00048CE6
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0004AAEE File Offset: 0x00048CEE
		public string interfaceName
		{
			get
			{
				return this.m_InterfaceName;
			}
			set
			{
				this.m_InterfaceName = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0004AAF7 File Offset: 0x00048CF7
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0004AAFF File Offset: 0x00048CFF
		public string deviceClass
		{
			get
			{
				return this.m_DeviceClass;
			}
			set
			{
				this.m_DeviceClass = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0004AB08 File Offset: 0x00048D08
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0004AB10 File Offset: 0x00048D10
		public string manufacturer
		{
			get
			{
				return this.m_Manufacturer;
			}
			set
			{
				this.m_Manufacturer = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0004AB19 File Offset: 0x00048D19
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0004AB21 File Offset: 0x00048D21
		public string product
		{
			get
			{
				return this.m_Product;
			}
			set
			{
				this.m_Product = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0004AB2A File Offset: 0x00048D2A
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x0004AB32 File Offset: 0x00048D32
		public string serial
		{
			get
			{
				return this.m_Serial;
			}
			set
			{
				this.m_Serial = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0004AB3B File Offset: 0x00048D3B
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0004AB43 File Offset: 0x00048D43
		public string version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0004AB4C File Offset: 0x00048D4C
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0004AB54 File Offset: 0x00048D54
		public string capabilities
		{
			get
			{
				return this.m_Capabilities;
			}
			set
			{
				this.m_Capabilities = value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0004AB60 File Offset: 0x00048D60
		public bool empty
		{
			get
			{
				return string.IsNullOrEmpty(this.m_InterfaceName) && string.IsNullOrEmpty(this.m_DeviceClass) && string.IsNullOrEmpty(this.m_Manufacturer) && string.IsNullOrEmpty(this.m_Product) && string.IsNullOrEmpty(this.m_Serial) && string.IsNullOrEmpty(this.m_Version) && string.IsNullOrEmpty(this.m_Capabilities);
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0004ABC8 File Offset: 0x00048DC8
		public override string ToString()
		{
			bool flag = !string.IsNullOrEmpty(this.product);
			bool flag2 = !string.IsNullOrEmpty(this.manufacturer);
			bool flag3 = !string.IsNullOrEmpty(this.interfaceName);
			if (flag && flag2)
			{
				if (flag3)
				{
					return string.Concat(new string[] { this.manufacturer, " ", this.product, " (", this.interfaceName, ")" });
				}
				return this.manufacturer + " " + this.product;
			}
			else if (flag)
			{
				if (flag3)
				{
					return this.product + " (" + this.interfaceName + ")";
				}
				return this.product;
			}
			else if (!string.IsNullOrEmpty(this.deviceClass))
			{
				if (flag3)
				{
					return this.deviceClass + " (" + this.interfaceName + ")";
				}
				return this.deviceClass;
			}
			else if (!string.IsNullOrEmpty(this.capabilities))
			{
				string text = this.capabilities;
				if (this.capabilities.Length > 40)
				{
					text = text.Substring(0, 40) + "...";
				}
				if (flag3)
				{
					return text + " (" + this.interfaceName + ")";
				}
				return text;
			}
			else
			{
				if (flag3)
				{
					return this.interfaceName;
				}
				return "<Empty Device Description>";
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0004AD20 File Offset: 0x00048F20
		public bool Equals(InputDeviceDescription other)
		{
			return this.m_InterfaceName.InvariantEqualsIgnoreCase(other.m_InterfaceName) && this.m_DeviceClass.InvariantEqualsIgnoreCase(other.m_DeviceClass) && this.m_Manufacturer.InvariantEqualsIgnoreCase(other.m_Manufacturer) && this.m_Product.InvariantEqualsIgnoreCase(other.m_Product) && this.m_Serial.InvariantEqualsIgnoreCase(other.m_Serial) && this.m_Version.InvariantEqualsIgnoreCase(other.m_Version) && this.m_Capabilities.InvariantEqualsIgnoreCase(other.m_Capabilities);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0004ADB4 File Offset: 0x00048FB4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputDeviceDescription)
			{
				InputDeviceDescription inputDeviceDescription = (InputDeviceDescription)obj;
				return this.Equals(inputDeviceDescription);
			}
			return false;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0004ADE0 File Offset: 0x00048FE0
		public override int GetHashCode()
		{
			return (((((((((((((this.m_InterfaceName != null) ? this.m_InterfaceName.GetHashCode() : 0) * 397) ^ ((this.m_DeviceClass != null) ? this.m_DeviceClass.GetHashCode() : 0)) * 397) ^ ((this.m_Manufacturer != null) ? this.m_Manufacturer.GetHashCode() : 0)) * 397) ^ ((this.m_Product != null) ? this.m_Product.GetHashCode() : 0)) * 397) ^ ((this.m_Serial != null) ? this.m_Serial.GetHashCode() : 0)) * 397) ^ ((this.m_Version != null) ? this.m_Version.GetHashCode() : 0)) * 397) ^ ((this.m_Capabilities != null) ? this.m_Capabilities.GetHashCode() : 0);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0004AEB1 File Offset: 0x000490B1
		public static bool operator ==(InputDeviceDescription left, InputDeviceDescription right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004AEBB File Offset: 0x000490BB
		public static bool operator !=(InputDeviceDescription left, InputDeviceDescription right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004AEC8 File Offset: 0x000490C8
		public string ToJson()
		{
			return JsonUtility.ToJson(new InputDeviceDescription.DeviceDescriptionJson
			{
				@interface = this.interfaceName,
				type = this.deviceClass,
				product = this.product,
				manufacturer = this.manufacturer,
				serial = this.serial,
				version = this.version,
				capabilities = this.capabilities
			}, true);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004AF44 File Offset: 0x00049144
		public static InputDeviceDescription FromJson(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			InputDeviceDescription.DeviceDescriptionJson deviceDescriptionJson = JsonUtility.FromJson<InputDeviceDescription.DeviceDescriptionJson>(json);
			return new InputDeviceDescription
			{
				interfaceName = deviceDescriptionJson.@interface,
				deviceClass = deviceDescriptionJson.type,
				product = deviceDescriptionJson.product,
				manufacturer = deviceDescriptionJson.manufacturer,
				serial = deviceDescriptionJson.serial,
				version = deviceDescriptionJson.version,
				capabilities = deviceDescriptionJson.capabilities
			};
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0004AFCC File Offset: 0x000491CC
		internal static bool ComparePropertyToDeviceDescriptor(string propertyName, string propertyValue, string deviceDescriptor)
		{
			JsonParser jsonParser = new JsonParser(deviceDescriptor);
			if (!jsonParser.NavigateToProperty(propertyName))
			{
				return string.IsNullOrEmpty(propertyValue);
			}
			return jsonParser.CurrentPropertyHasValueEqualTo(propertyValue);
		}

		// Token: 0x0400064B RID: 1611
		[SerializeField]
		private string m_InterfaceName;

		// Token: 0x0400064C RID: 1612
		[SerializeField]
		private string m_DeviceClass;

		// Token: 0x0400064D RID: 1613
		[SerializeField]
		private string m_Manufacturer;

		// Token: 0x0400064E RID: 1614
		[SerializeField]
		private string m_Product;

		// Token: 0x0400064F RID: 1615
		[SerializeField]
		private string m_Serial;

		// Token: 0x04000650 RID: 1616
		[SerializeField]
		private string m_Version;

		// Token: 0x04000651 RID: 1617
		[SerializeField]
		private string m_Capabilities;

		// Token: 0x0200022A RID: 554
		private struct DeviceDescriptionJson
		{
			// Token: 0x04000BBD RID: 3005
			public string @interface;

			// Token: 0x04000BBE RID: 3006
			public string type;

			// Token: 0x04000BBF RID: 3007
			public string product;

			// Token: 0x04000BC0 RID: 3008
			public string serial;

			// Token: 0x04000BC1 RID: 3009
			public string version;

			// Token: 0x04000BC2 RID: 3010
			public string manufacturer;

			// Token: 0x04000BC3 RID: 3011
			public string capabilities;
		}
	}
}
