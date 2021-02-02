using System;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new Game1())
				game.Run();
		}
	}
}
