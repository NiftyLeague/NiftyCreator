//This class is auto-generated do not modify
using UnityEngine.SceneManagement;

namespace NiftyLeague
{
	public static class Scenes
	{
		public const string NiftyCreator = "NiftyCreator";

		public const int TotalScenes = 1;


		public static int NextSceneIndex()
		{
			if( SceneManager.GetActiveScene().buildIndex + 1 == TotalScenes )
				return 0;
			return SceneManager.GetActiveScene().buildIndex + 1;
		}
	}
}
