using System;
using System.Collections.Generic;
using System.Text;

using GameDev_Gie_Vanommeslaeghe_2EACL1.Components;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;
using Microsoft.Xna.Framework;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects
{
	class Enemy : IEntity, ITransform, ICollision
	{
		private RenderComponent render;

		public Enemy(Vector2 position)
		{
			render = new RenderComponent("", "", "Enemies/blockerMad");
			this.Position = position;
			UpdateRect();
		}

		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public Rectangle CollisionRectangle { get; set; }

		public void Draw()
		{
			render.Draw(Position, Enums.EntityState.idle);
		}

		public void Update(GameTime gameTime)
		{
		}

		public void UpdateRect()
		{
			var dimensions = render.GetDimensions();
			this.CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
		}
	}
}
