using UnityEngine;

namespace DredgeMonstrousShip
{
	public class Loader
	{
		/// <summary>
		/// This method is run by Winch to initialize your mod
		/// </summary>
		public static void Initialize()
		{
			var gameObject = new GameObject(nameof(DredgeMonstrousShip));
			gameObject.AddComponent<DredgeMonstrousShip>();
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}