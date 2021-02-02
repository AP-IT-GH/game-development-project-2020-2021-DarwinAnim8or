using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameDev_Gie_Vanommeslaeghe_2EACL1.GameObjects;
using GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1
{
	class SceneManager
	{
		Collision.CollisionManager collisionManager;

		private List<Blok> blokken;
		private List<Enemy> enemies;
		private List<WanderingEnemy> wanderingEnemies;

		Player player;

		EndTile endTile;

		Texture2D startScreen;
		Texture2D endScreen;

		int currLevel = 0;

		bool wonGame = false;
		bool isPlaying = false;

		public SceneManager()
		{
			collisionManager = new Collision.CollisionManager();
			player = new Player(new Input.KeyboardReader());
			blokken = new List<Blok>();
			enemies = new List<Enemy>();
			wanderingEnemies = new List<WanderingEnemy>();
			endTile = new EndTile();

			startScreen = Globals.Content.Load<Texture2D>("startScreen");
			endScreen = Globals.Content.Load<Texture2D>("winScreen");

			LoadLevel(currLevel); //load our first level
		}

		public void LoadLevel(int level)
		{
			blokken.Clear();
			enemies.Clear();
			wanderingEnemies.Clear();

			if (level == 0)
			{
				//move endTile:
				endTile.Position = new Vector2(700, 360);
				endTile.UpdateRect();

				//ground layer
				for (int i = 0; i < 50; i++)
					blokken.Add(new Blok(new Vector2(50* i, 420)));

				//start bloks:
				blokken.Add(new Blok(new Vector2(0, 100)));
				blokken.Add(new Blok(new Vector2(50, 100)));

				//a platform:
				blokken.Add(new Blok(new Vector2(300, 200)));
				blokken.Add(new Blok(new Vector2(350, 200)));

				//some enemies:
				enemies.Add(new Enemy(new Vector2(600, 370)));
				enemies.Add(new Enemy(new Vector2(400, 370)));

				//move player:
				player.Position = new Vector2(0, 0);
			}
			else
			{
				//move endTile:
				endTile.Position = new Vector2(725, 100);
				endTile.UpdateRect();

				//end bloks:
				blokken.Add(new Blok(new Vector2(675, 170)));
				blokken.Add(new Blok(new Vector2(725, 170)));

				//a platform:
				blokken.Add(new Blok(new Vector2(300, 250)));
				blokken.Add(new Blok(new Vector2(100, 200)));
				blokken.Add(new Blok(new Vector2(400, 150)));

				//some enemies:
				enemies.Add(new Enemy(new Vector2(500, 200)));
				enemies.Add(new Enemy(new Vector2(550, 200)));
				enemies.Add(new Enemy(new Vector2(600, 200)));

				//wandering enemy:
				wanderingEnemies.Add(new WanderingEnemy(new Vector2(210, 390), 25, 1));
				wanderingEnemies.Add(new WanderingEnemy(new Vector2(400, 390), 40, 1));
				wanderingEnemies.Add(new WanderingEnemy(new Vector2(500, 390), 40, 1));

				//zet player juist:
				player.Position = new Vector2(50, 300);

				//ground layer
				for (int i = 0; i < 50; i++)
					blokken.Add(new Blok(new Vector2(50 * i, 420)));
			}
		}

		public void Update(GameTime gameTime)
		{
			if (wonGame) return;

			player.Update(gameTime);
			if (player.Velocity.X != 0) isPlaying = true;

			foreach (Blok blok in blokken)
			{
				PlayerCollisionTest(blok);
			}

			foreach (Enemy enemy in enemies)
			{
				if (collisionManager.CheckCollision(player, enemy))
				{
					//todo: play die sound

					//restart level:
					LoadLevel(currLevel);
					return;
				}
			}

			foreach (WanderingEnemy enemy in wanderingEnemies)
			{
				enemy.Update(gameTime);

				if (collisionManager.CheckCollision(player, enemy))
				{
					//todo: play die sound

					//restart level:
					LoadLevel(currLevel);
					return;
				}
			}

			//test to see if we need to load the next level:
			if (isPlaying && collisionManager.CheckCollision(player, endTile))
			{
				currLevel++;

				if (currLevel == 2) //instead, show end screen
				{
					wonGame = true;
					return;
				}
				LoadLevel(currLevel);
			}
		}

		public void Draw()
		{
			if (!isPlaying)
			{
				Globals.SpriteBatch.Draw(startScreen, new Vector2(-50, 0), Color.White);
				return;
			}
			else if (wonGame)
			{
				Globals.SpriteBatch.Draw(endScreen, new Vector2(-50, 0), Color.White);
				return;
			}

			foreach (Blok blok in blokken)
			{
				blok.Draw();
			}

			foreach (Enemy enemy in enemies)
			{
				enemy.Draw();
			}

			foreach (WanderingEnemy enemy in wanderingEnemies)
			{
				enemy.Draw();
			}

			endTile.Draw();

			player.Draw();
		}

		void PlayerCollisionTest(Blok entity)
		{
			if (!collisionManager.CheckCollision(player, entity)) return;

			//Are we on top?
			if (player.CollisionRectangle.Y + 60 < entity.CollisionRectangle.Y)
			{
				player.Position = new Vector2(player.Position.X, entity.CollisionRectangle.Top - player.CollisionRectangle.Height);
				player.canJump = true;
			}

			//touching the sides?
			else if (player.CollisionRectangle.X > entity.CollisionRectangle.X || player.CollisionRectangle.X < entity.CollisionRectangle.X)
			{
				player.Position = new Vector2(player.Position.X - player.Velocity.X, player.Position.Y);
			}

			//if we're headcrushing ourselves
			if (player.CollisionRectangle.Y > entity.CollisionRectangle.Y)
			{
				player.Position = new Vector2(player.Position.X, entity.CollisionRectangle.Bottom);
			}
		}
	}
}
