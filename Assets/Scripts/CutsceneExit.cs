using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneExit : MonoBehaviour
{
	public string scene; //takes the name of the new scene to be loaded, in a string.
    public float delay; //takes a float as the time it takes before loading new scene.

	private void Start()
	{
		StartCoroutine(LoadScene(delay));
	}

	IEnumerator LoadScene(float delay)
	{
		yield return new WaitForSeconds(delay);
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}
}
