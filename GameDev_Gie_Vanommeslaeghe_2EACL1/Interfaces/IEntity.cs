using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces
{
	interface IEntity
	{
		public void Draw();

		public void Update(GameTime gameTime);
	}
}
