using Core.BusinessLogic.Interfaces;

namespace AgentViewSwitcher.Interfaces
{
	internal interface ISwitchersManager
	{
		void ExecuteSwitchers(ISwitcher[] switchers);
	}
}
