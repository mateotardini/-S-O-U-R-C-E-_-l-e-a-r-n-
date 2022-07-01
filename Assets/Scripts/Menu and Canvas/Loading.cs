using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class Loading : MonoBehaviour {
	public GameObject loadingscreen;
	public int sceneIndex;
//	public TextEditor progresstext;

	void Start(){
		sceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
	}

	public void LoadScene (int sceneIndex){
		StartCoroutine (LoadAsynchronously (sceneIndex));
	}

	IEnumerator LoadAsynchronously (int sceneIndex){
		AsyncOperation op = SceneManager.LoadSceneAsync (sceneIndex);
		loadingscreen.SetActive (true);

		while (!op.isDone) {
			float progress = Mathf.Clamp01 (op.progress / .9f);
			yield return null;
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			if (sceneIndex > PlayerPrefs.GetInt ("levelAt"))
				PlayerPrefs.SetInt ("levelAt", sceneIndex);
			Data.data.Save ();
			Analytics.CustomEvent("Level", new Dictionary<string, object>{
				{"LevelNumber", sceneIndex} }
			);

			AnalyticsResult analitycResult = Analytics.CustomEvent("Level", new Dictionary<string, object>{
				{"LevelNumber", sceneIndex} }
			);
			print("Funciona esto:" + analitycResult);
			LoadScene(sceneIndex);
		}
	}
}

