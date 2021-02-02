using System;
using System.Collections.Generic;
using System.Text;
using GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Collision
{
	class CollisionManager
	{
		public bool CheckCollision(ICollision a, ICollision b) { 
			return a.CollisionRectangle.Intersects(b.CollisionRectangle); 
		}
	}
}
