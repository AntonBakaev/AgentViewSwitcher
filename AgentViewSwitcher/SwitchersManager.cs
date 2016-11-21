using System.Linq;
using AgentViewSwitcher.Interfaces;
using Core.BusinessLogic.Interfaces;

namespace AgentViewSwitcher
{
	internal class SwitchersManager : ISwitchersManager
	{
		public void ExecuteSwitchers(ISwitcher[] switchers)
		{
			bool agentViewTurnedOn = CheckIfAgentViewTurnedOn(switchers);
			foreach (var switcher in switchers)
				switcher.SwitchFile(agentViewTurnedOn);
		}

		private bool CheckIfAgentViewTurnedOn(ISwitcher[] switchers)
		{
			return switchers.All(s => s.AgentViewTurnedOnForThisSwitcher);
		}
	}
}
