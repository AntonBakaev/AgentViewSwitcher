using System;
using System.Configuration;
using Core.BusinessLogic.Interfaces;
using Core.Helpers;
using Core.Helpers.Interfaces;

namespace Core.BusinessLogic
{
	public class OneScreenProjSwitcher : ISwitcher
	{
		private const string AnonAuthEnabled = "<IISExpressAnonymousAuthentication>enabled</IISExpressAnonymousAuthentication>";
		private const string WinAuthDisabled = "<IISExpressWindowsAuthentication>disabled</IISExpressWindowsAuthentication>";
		private const string AnonAuthDisabled = "<IISExpressAnonymousAuthentication>disabled</IISExpressAnonymousAuthentication>";
		private const string WinAuthEnabled = "<IISExpressWindowsAuthentication>enabled</IISExpressWindowsAuthentication>";
		private const string OneScreenProjLocationKey = "OneScreenProjLocationInOS";

		private readonly IFileHelper _file = new FileHelper();

		public bool AgentViewTurnedOnForThisSwitcher { get { return CheckIfAgentViewTurnedOn(); } }
		private string OneScreenProjRelativePath { get { return ConfigurationManager.AppSettings[OneScreenProjLocationKey]; } }
		private string OneScreenProjPath { get { return String.Format(@"{0}\{1}", ConfigHelper.OneScreenLocation, OneScreenProjRelativePath); } }

		public void SwitchFile(bool agentViewTurnedOn = false)
		{
			string oneScreenProjPath = OneScreenProjPath;
			string newContent = CreateNewFileContent(_file.ReadFile(oneScreenProjPath), agentViewTurnedOn);
			_file.WriteFile(oneScreenProjPath, newContent);
		}

		private string CreateNewFileContent(string oldContent, bool agentViewTurnedOn)
		{
			if (agentViewTurnedOn)
				return oldContent.Replace(AnonAuthDisabled, AnonAuthEnabled)
					.Replace(WinAuthEnabled, WinAuthDisabled);

			return oldContent.Replace(AnonAuthEnabled, AnonAuthDisabled)
				.Replace(WinAuthDisabled, WinAuthEnabled);
		}

		private bool CheckIfAgentViewTurnedOn()
		{
			string content = _file.ReadFile(OneScreenProjPath);
			return content.Contains(WinAuthEnabled);
		}
	}
}
