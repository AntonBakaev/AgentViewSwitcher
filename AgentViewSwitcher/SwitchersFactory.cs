using System.Collections.Generic;
using AgentViewSwitcher.Interfaces;
using Core.BusinessLogic;
using Core.BusinessLogic.Interfaces;

namespace AgentViewSwitcher
{
	public class SwitchersFactory : ISwitchersFactory
	{
		public ISwitcher[] GetSwitchers()
		{
			return new List<ISwitcher>
			{
				new WebConfigSwitcher(),
				new OneScreenProjSwitcher(),
				new LoginRepositorySwitcher()
			}.ToArray();
		}
	}
}
