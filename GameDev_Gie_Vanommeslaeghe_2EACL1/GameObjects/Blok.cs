using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev_Gie_Vanommeslaeghe_2EACL1.Components;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects
{
	class Blok : IEntity, ITransform, ICollision
	{
		public Rectangle CollisionRectangle { get; set; }
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }

		private RenderComponent render;

		public Blok(Vector2 position)
		{
			render = new RenderComponent("", "", "Level/grassCenter");
			this.Position = position;

			var dimensions = render.GetDimensions();
			this.CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
		}

		public void Draw()
		{
			render.Draw(Position, Enums.EntityState.idle);
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}
