using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces
{
	interface ITransform
	{
		public Vector2 Position { get; set; }

		public Vector2 Velocity { get; set; }
	}
}
