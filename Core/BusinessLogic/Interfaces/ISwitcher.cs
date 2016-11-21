namespace Core.BusinessLogic.Interfaces
{
	public interface ISwitcher
	{
		bool AgentViewTurnedOnForThisSwitcher { get; }
		void SwitchFile(bool agentViewTurnedOn = false);
	}
}
