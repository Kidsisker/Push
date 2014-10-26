using System.Configuration;

namespace PushUI.Models.Application.Config
{
	public class MergeEnvironmentConfigurationElement : ConfigurationElement
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

		[ConfigurationProperty("states")]
		public WorkItemStateCollection AllowedStates
		{
			get { return (WorkItemStateCollection)this["states"]; }
		}

		[ConfigurationProperty("branches")]
		public BranchCollection Branches
		{
			get { return (BranchCollection)this["branches"]; }
		}
	}

	public class Source : ConfigurationElement
	{
		[ConfigurationProperty("id", IsKey = true)]
		public string Id { get { return (string)this["id"]; } }
	}

	public class Target : ConfigurationElement
	{
		[ConfigurationProperty("id", IsKey = true)]
		public string Id { get { return (string)this["id"]; } }
	}
	
	public class MergeEnvironmentCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new MergeEnvironmentConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((MergeEnvironmentConfigurationElement)element).Id;
		}

		protected override string ElementName
		{
			get
			{
				return "mergeEnvironment";
			}
		}

		protected override bool IsElementName(string elementName)
		{
			return !string.IsNullOrEmpty(elementName) && elementName == "mergeEnvironment";
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		public MergeEnvironmentConfigurationElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as MergeEnvironmentConfigurationElement;
			}
		}

		public new MergeEnvironmentConfigurationElement this[string key]
		{
			get
			{
				return base.BaseGet(key) as MergeEnvironmentConfigurationElement;
			}
		}
	}
}