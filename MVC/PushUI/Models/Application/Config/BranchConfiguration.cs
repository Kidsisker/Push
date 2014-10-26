using System.Configuration;

namespace PushUI.Models.Application.Config
{
	public class BranchConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("id", IsKey = true)]
		public string Id { get { return (string)this["id"]; } }

		[ConfigurationProperty("source")]
		public Source Source
		{
			get { return (Source)this["source"]; }
		}

		[ConfigurationProperty("target")]
		public Target Target
		{
			get { return (Target)this["target"]; }
		}
	}

	public class BranchCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new BranchConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BranchConfigurationElement)element).Id;
		}

		protected override string ElementName
		{
			get
			{
				return "branch";
			}
		}

		protected override bool IsElementName(string elementName)
		{
			return !string.IsNullOrEmpty(elementName) && elementName == "branch";
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		public BranchConfigurationElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as BranchConfigurationElement;
			}
		}

		public new BranchConfigurationElement this[string key]
		{
			get
			{
				return base.BaseGet(key) as BranchConfigurationElement;
			}
		}
	}

}