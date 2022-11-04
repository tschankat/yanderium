using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
	void Start()
	{
		SceneManager.LoadScene(SceneNames.SchoolScene);
	}
}
