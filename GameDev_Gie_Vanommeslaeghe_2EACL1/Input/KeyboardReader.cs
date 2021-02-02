using System;
using System.Collections.Generic;
using System.Text;

using GameDev_Gie_Vanommeslaeghe_2EACL1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Input
{
	class KeyboardReader : Interfaces.IInputReader
	{
		public Vector2 ReadInput()
		{
			var direction = Vector2.Zero;

			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Left))
				direction = new Vector2(-1, 0);

			if (keyboard.IsKeyDown(Keys.Right))
				direction = new Vector2(1, 0);

			if (keyboard.IsKeyDown(Keys.Up))
				direction.Y = 1;

			return direction;
		}
	}
}
