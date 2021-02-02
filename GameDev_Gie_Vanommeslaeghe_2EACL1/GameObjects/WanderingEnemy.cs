using System;
using System.Collections.Generic;
using System.Text;

using GameDev_Gie_Vanommeslaeghe_2EACL1.Components;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;
using Microsoft.Xna.Framework;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects
{
	class WanderingEnemy : IEntity, ITransform, ICollision
	{
		private RenderComponent render;
		int wanderDistance = 0;
		bool isFlipped = false;
		float distanceTravelled = 0.0f;

		public WanderingEnemy(Vector2 position, int wanderDistance, int wanderSpeed)
		{
			render = new RenderComponent("", "", "Enemies/snailWalk1");
			this.Position = position;
			UpdateRect();
			this.wanderDistance = wanderDistance;
			Velocity = new Vector2(wanderSpeed, 0);
		}

		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public Rectangle CollisionRectangle { get; set; }

		public void Draw()
		{
			render.Draw(Position, Enums.EntityState.idle, !isFlipped);
		}

		public void Update(GameTime gameTime)
		{
			if (distanceTravelled >= wanderDistance)
			{
				isFlipped = !isFlipped;
				distanceTravelled = -distanceTravelled;
			}

			if (isFlipped)
			{
				Position -= Velocity;
				distanceTravelled += Velocity.X;
			} 
			else
			{
				Position += Velocity;
				distanceTravelled += Velocity.X;
			}

			UpdateRect();
		}

		public void UpdateRect()
		{
			var dimensions = render.GetDimensions();
			this.CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
		}
	}
}
