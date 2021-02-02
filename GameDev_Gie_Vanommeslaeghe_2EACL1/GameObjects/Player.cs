using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev_Gie_Vanommeslaeghe_2EACL1.Components;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Input;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects
{
	class Player : IEntity, ITransform, IDestroyable, ICollision
	{
		//Member variables from the various interfaces
		public Rectangle CollisionRectangle { get; set; }
		private Rectangle _colRect;
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public int Health { get; set; }

		private RenderComponent render;

		private Enums.EntityState entityState;

		public bool canJump { get; set; }

		IInputReader inputReader;

		public Player(IInputReader inputReader)
		{
			this.inputReader = inputReader;
			render = new RenderComponent("playerWalk", "playerJump", "player", 30, 11);

			entityState = Enums.EntityState.idle;
			Health = 1;
			canJump = true;


			var dimensions = render.GetDimensions();
			this.CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
			_colRect = CollisionRectangle;
		}

		public void Draw()
		{
			render.Draw(Position, entityState, Velocity.X < 0);
		}

		public void Update(GameTime gameTime)
		{
			//Physics updates first
			Position += Velocity;
			_colRect.X = (int)Position.X;
			_colRect.Y = (int)Position.Y;
			CollisionRectangle = _colRect;

			render.Update(gameTime, entityState);

			Velocity = inputReader.ReadInput();
			Velocity *= 4;

			if (Velocity == Vector2.Zero) entityState = Enums.EntityState.idle;
			else
			{
				entityState = Enums.EntityState.walking;
				if (Velocity.Y != 0) entityState = Enums.EntityState.jumping;
			}

			Velocity = new Vector2(Velocity.X, Velocity.Y + 4);

			//If jumping, we need to boost our velocity
			if (entityState == Enums.EntityState.jumping && canJump)
			{
				Velocity = new Vector2(Velocity.X, Velocity.Y - 15);
				//canJump = false; //maakte een bug, dus floaty
			}
		}
	}
}
