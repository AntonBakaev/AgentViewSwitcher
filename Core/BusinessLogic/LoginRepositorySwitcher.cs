using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Core.BusinessLogic.Interfaces;
using Core.Helpers;
using Core.Helpers.Interfaces;

namespace Core.BusinessLogic
{
	public class LoginRepositorySwitcher : ISwitcher
	{
		private const string AgentViewIndicatorString = "username = \"";
		private const string UsernameRegex = "\"[a-zA-Z]+\"";
		private const string DomainRegex = "\"[a-zA-Z.]+\"";
		private const string AgentViewTurnedOffString = "var input = new AgentLoginRequestDto\n\t\t\t{\r\n\t\t\t\tusername = username,\r\n\t\t\t\tdomain = domain";
		private const string AgentViewTurnedOnStringFormat = "var input = new AgentLoginRequestDto\n\t\t\t{{\r\n\t\t\t\tusername = {0},\r\n\t\t\t\tdomain = {1}";
		private const string LoginRepositoryLocationKey = "LoginRepositoryLocationInOS";
		private const string AgentUsernameKey = "AgentUsername";
		private const string AgentDomainKey = "AgentDomain";

		private readonly IFileHelper _file = new FileHelper();

		public bool AgentViewTurnedOnForThisSwitcher { get { return CheckIfAgentViewTurnedOn(); } }
		private string LoginRepositoryRelativePath { get { return ConfigurationManager.AppSettings[LoginRepositoryLocationKey]; } }
		private string LoginRepositoryPath { get { return String.Format(@"{0}\{1}", ConfigHelper.OneScreenLocation, LoginRepositoryRelativePath); } }
		private string AgentUsername { get { return ConfigurationManager.AppSettings[AgentUsernameKey]; } }
		private string AgentDomain { get { return ConfigurationManager.AppSettings[AgentDomainKey]; } }

		public void SwitchFile(bool agentViewTurnedOn = false)
		{
			string loginRepositoryPath = LoginRepositoryPath;
			string newContent = CreateNewFileContent(_file.ReadFile(loginRepositoryPath), agentViewTurnedOn);
			_file.WriteFile(loginRepositoryPath, newContent);
		}

		private string CreateNewFileContent(string oldContent, bool agentViewTurnedOn)
		{
			if (agentViewTurnedOn)
				return Regex.Replace(oldContent, String.Format(AgentViewTurnedOnStringFormat, UsernameRegex, DomainRegex), AgentViewTurnedOffString);
			return oldContent.Replace(AgentViewTurnedOffString, GetStringWithAgenCredenials());
		}

		private bool CheckIfAgentViewTurnedOn()
		{
			string content = _file.ReadFile(LoginRepositoryPath);
			return content.Contains(AgentViewIndicatorString);
		}

		private string GetStringWithAgenCredenials()
		{
			return String.Format(AgentViewTurnedOnStringFormat, QuoteString(AgentUsername),
				QuoteString(AgentDomain));
		}

		private string QuoteString(string str)
		{
			return String.Format("\"{0}\"", str);
		}
	}
}
