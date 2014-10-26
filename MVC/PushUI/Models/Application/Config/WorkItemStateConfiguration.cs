using System.Configuration;

namespace PushUI.Models.Application.Config
{
	public class WorkItemStateConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("id", IsKey = true)]
		public string Id { get { return (string)this["id"]; } }
	}

	public class WorkItemStateCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new WorkItemStateConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((WorkItemStateConfigurationElement)element).Id;
		}

		protected override string ElementName
		{
			get
			{
				return "state";
			}
		}

		protected override bool IsElementName(string elementName)
		{
			return !string.IsNullOrEmpty(elementName) && elementName == "state";
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		public WorkItemStateConfigurationElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as WorkItemStateConfigurationElement;
			}
		}

		public new WorkItemStateConfigurationElement this[string key]
		{
			get
			{
				return base.BaseGet(key) as WorkItemStateConfigurationElement;
			}
		}
	}

}