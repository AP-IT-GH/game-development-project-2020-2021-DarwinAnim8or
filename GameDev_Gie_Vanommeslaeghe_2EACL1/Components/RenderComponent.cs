using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Components
{
	class RenderComponent
	{
		private Texture2D idleFrame;
		private Texture2D jumpFrame;
		private Texture2D spriteSheet;
		private Rectangle destRect;

		private int fps;
		private int widthPerFrame;

		private double frameTime = 0.0;
		private double timeSinceLastFrameChange = 0.0;

		public RenderComponent(string spriteName, string jumpName, string idleName, int fps = 0, int totalFrames = 1)
		{
			idleFrame = Globals.Content.Load<Texture2D>(idleName);

			if (fps != 0)
			{
				spriteSheet = Globals.Content.Load<Texture2D>(spriteName);
				jumpFrame = Globals.Content.Load<Texture2D>(jumpName);

				this.fps = fps;
				this.widthPerFrame = this.spriteSheet.Width / totalFrames;
				this.destRect = new Rectangle(0, 0, this.widthPerFrame, this.spriteSheet.Height);

				frameTime = 1.0 / fps;
			}
		}

		public void Update(GameTime gameTime, Enums.EntityState state)
		{
			if (fps == 0 || state != Enums.EntityState.walking)
			{
				destRect.X = 0; //just to be safe
			}

			if (timeSinceLastFrameChange > frameTime)
			{
				destRect.X += widthPerFrame;

				if (destRect.X >= spriteSheet.Width)
					destRect.X = 0;

				timeSinceLastFrameChange = 0.0;
			}

			timeSinceLastFrameChange += gameTime.ElapsedGameTime.TotalSeconds;
		}

		public void Draw(Vector2 position, Enums.EntityState state, bool isFlipped = false)
		{
			switch (state)
			{
				case Enums.EntityState.walking:
				{
					if (isFlipped)
					{
						Globals.SpriteBatch.Draw(spriteSheet, position, destRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 1.0f); ;
					}
					else
					{
						Globals.SpriteBatch.Draw(spriteSheet, position, destRect, Color.White);
					}

					break;
				}


				case Enums.EntityState.jumping:
					Globals.SpriteBatch.Draw(jumpFrame, position, Color.White);
					break;

				default:
					if (isFlipped)
					{
						Rectangle rect = new Rectangle(0,0, idleFrame.Width, idleFrame.Height);
						Globals.SpriteBatch.Draw(idleFrame, position, rect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 1.0f);
					}
					else
					{
						Globals.SpriteBatch.Draw(idleFrame, position, Color.White);
					}
					
					break;
			}
		}

		public Vector2 GetDimensions()
		{
			return new Vector2(this.idleFrame.Width, this.idleFrame.Height);
		}
	}
}
