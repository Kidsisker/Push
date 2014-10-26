using System.Configuration;

namespace PushUI.Models.Application.Config
{
	public class ProjectsConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("", IsDefaultCollection = true, IsKey = false, IsRequired = true)]
		public ProjectCollection Projects
		{
			get
			{
				return base[""] as ProjectCollection;
			}

			set
			{
				base[""] = value;
			}
		}

	}

	public class ProjectConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("id", IsKey = true)]
		public string Id { get { return (string)this["id"]; } }

		[ConfigurationProperty("isDefault")]
		public bool IsDefault { get { return (bool)this["isDefault"]; } }

		[ConfigurationProperty("mergeEnvironments")]
		public MergeEnvironmentCollection MergeEnvironments
		{
			get { return (MergeEnvironmentCollection)this["mergeEnvironments"]; }
		}
	}

	public class ProjectCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProjectConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProjectConfigurationElement)element).Id;
		}

		protected override string ElementName
		{
			get
			{
				return "project";
			}
		}

		protected override bool IsElementName(string elementName)
		{
			return !string.IsNullOrEmpty(elementName) && elementName == "project";
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		public ProjectConfigurationElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as ProjectConfigurationElement;
			}
		}

		public new ProjectConfigurationElement this[string key]
		{
			get
			{
				return base.BaseGet(key) as ProjectConfigurationElement;
			}
		}
	}

}