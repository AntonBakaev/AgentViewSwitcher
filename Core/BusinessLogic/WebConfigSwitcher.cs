using System;
using System.Configuration;
using Core.BusinessLogic.Interfaces;
using Core.Helpers;
using Core.Helpers.Interfaces;

namespace Core.BusinessLogic
{
	public class WebConfigSwitcher : ISwitcher
	{
		private const string FormsString = ".Forms.";
		private const string WindowsString = ".Windows.";
		private const string WebConfigLocationKey = "WebConfigLocationInOS";

		private readonly IFileHelper _file = new FileHelper();

		public bool AgentViewTurnedOnForThisSwitcher { get { return CheckIfAgentViewTurnedOn(); } }
		private string WebConfigRelativePath { get { return ConfigurationManager.AppSettings[WebConfigLocationKey]; } }
		private string WebConfigPath { get { return String.Format(@"{0}\{1}", ConfigHelper.OneScreenLocation, WebConfigRelativePath); } }

		public void SwitchFile(bool agentViewTurnedOn = false)
		{
			string webConfigPath = WebConfigPath;
			string newContent = CreateNewFileContent(_file.ReadFile(webConfigPath), agentViewTurnedOn);
			_file.WriteFile(webConfigPath, newContent);
		}

		private string CreateNewFileContent(string oldContent, bool agentViewTurnedOn)
		{
			return agentViewTurnedOn ? oldContent.Replace(WindowsString, FormsString)
				: oldContent.Replace(FormsString, WindowsString);
		}

		private bool CheckIfAgentViewTurnedOn()
		{
			string content = _file.ReadFile(WebConfigPath);
			return content.Contains(WindowsString);
		}
	}
}
