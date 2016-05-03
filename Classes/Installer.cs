using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Fenetre.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Fenetre.Sitefinity.TwitterFeed
{
	public class Installer
	{
		/// <summary>
		/// Mthod that is called by ASP.NET before application starts.
		/// </summary>
		public static void PreApplicationStart()
		{
			Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
		}

		/// <summary>
        /// Method that subscribes this object to the Sitefinity Bootstrapper_Initialized event, 
        /// which is fired after initialization of the Sitefinity application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
		{
			if (e.CommandName != "RegisterRoutes" || !Bootstrapper.IsDataInitialized)
			{
				return;
			}

			InstallModule();
			InstallVirtualPaths();
			InstallWidgets();
		}


		/// <summary>
		/// Method to programmatically install modules.
		/// </summary>
		private static void InstallModule()
		{
			// Define content view control
			var modulesConfig = Config.Get<SystemConfig>().ApplicationModules;
			AppModuleSettings customFieldsSetting = modulesConfig.Elements.Where(el => el.GetKey().Equals(FenetreModule.moduleName)).SingleOrDefault();
			if (customFieldsSetting == null)
			{
				AppModuleSettings moduleConfigElement = new AppModuleSettings(modulesConfig)
				{
					Name = FenetreModule.moduleName,
					Title = FenetreModule.moduleTitle,
					Description = FenetreModule.moduleDescription,
					Type = typeof(FenetreModule).AssemblyQualifiedName,
					StartupType = StartupType.OnApplicationStart
				};

				// Register the module
				modulesConfig.Add(FenetreModule.moduleName, moduleConfigElement);

				ConfigManager.GetManager().SaveSection(modulesConfig.Section);
			}
		}

		/// <summary>
		/// Method to programmatically install virtual paths.
		/// </summary>
		private static void InstallVirtualPaths()
		{
			//SiteInitializer initializer = SiteInitializer.GetInitializer();
			//var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
			//var EventsCalendarViewConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
			//{
			//	VirtualPath = SampleWidget.sampleWidgetVirtualPath + "*",
			//	ResolverName = "EmbeddedResourceResolver",
			//	ResourceLocation = typeof(SampleWidget).Assembly.GetName().Name
			//};
			//if (!virtualPathConfig.VirtualPaths.ContainsKey(SampleWidget.sampleWidgetVirtualPath + "*"))
			//{
			//	virtualPathConfig.VirtualPaths.Add(EventsCalendarViewConfig);
			//	Config.GetManager().SaveSection(virtualPathConfig);
			//}
		}

		/// <summary>
		/// Method that installs the widget.
		/// </summary>
		private static void InstallWidgets()
		{
			//App.WorkWith().Module(FenetreModule.moduleName).Install()
			//	.PageToolbox().LoadOrAddSection("Fenetre")
			//		.SetTitle("Fenêtre Add-ons")
			//		.LoadOrAddWidget<TwitterFeed>("TwitterFeed")
			//			.SetTitle("Fenêtre Twitter feed")
			//			.SetDescription("Twitter feed that also works without JavaScript. Powered by Fenêtre.")
			//	.Done();

			InstallSingleWidget("TwitterFeed", "Fenêtre Twitter feed", typeof(TwitterFeed), "Fenetre", "Fenêtre Add-ons", "PageControls");
		}

        /// <summary>
        /// Method to install a single widget with the given parameters.
        /// </summary>
        /// <param name="controlName">Name of the widget</param>
        /// <param name="controlTitle">Name of the widget that will be visible in the front end</param>
        /// <param name="controlType">Type of the widget</param>
        /// <param name="sectionName">Name of the section the widget will be installed into</param>
        /// <param name="sectionTitle">Name of the section that will visible in the front end</param>
        /// <param name="toolBoxName">Name of the Sitefinity toolbox</param>
		private static void InstallSingleWidget(string controlName, string controlTitle, System.Type controlType, string sectionName, string sectionTitle, string toolBoxName)
		{
			var configManager = ConfigManager.GetManager();
			var config = configManager.GetSection<ToolboxesConfig>();

			var controls = config.Toolboxes[toolBoxName];
			var section = controls.Sections.Where<ToolboxSection>(e => e.Name == sectionName).FirstOrDefault();

			if (section == null)
			{
				section = new ToolboxSection(controls.Sections)
				{
					Name = sectionName,
					Title = sectionTitle
				};
				controls.Sections.Add(section);
			}

			if (!section.Tools.Any<ToolboxItem>(e => e.Name == controlName))
			{
				var tool = new ToolboxItem(section.Tools)
				{
					Name = controlName,
					Title = controlTitle,
					ControlType = controlType.AssemblyQualifiedName
				};
				section.Tools.Add(tool);
			}

			configManager.SaveSection(config);
		}
	}
}
