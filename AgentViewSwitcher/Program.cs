using System;
using AgentViewSwitcher.Interfaces;

namespace AgentViewSwitcher
{
	internal static class Program
	{
		static void Main()
		{
			try
			{
				ISwitchersFactory switchersFactory = new SwitchersFactory();
				ISwitchersManager switchersManager = new SwitchersManager();
				switchersManager.ExecuteSwitchers(switchersFactory.GetSwitchers());

				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("Operation successful.");
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine(ex.StackTrace);
				Console.ReadLine();
			}
		}
	}
}
