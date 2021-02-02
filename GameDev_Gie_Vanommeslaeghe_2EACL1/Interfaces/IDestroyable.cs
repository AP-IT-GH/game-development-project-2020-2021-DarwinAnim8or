using System;
using System.Collections.Generic;
using System.Text;

namespace GameDev_Gie_Vanommeslaeghe_2EACL1.Interfaces
{
	interface IDestroyable
	{
		public int Health { get; set; }

		public void TakeDamage(int damage) { Health -= damage; }

		public bool IsAlive() { return Health > 0; }
	}
}
