using Core.BusinessLogic.Interfaces;

namespace AgentViewSwitcher.Interfaces
{
	public interface ISwitchersFactory
	{
		ISwitcher[] GetSwitchers();
	}
}
