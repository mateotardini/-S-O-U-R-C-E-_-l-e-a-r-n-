using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelecction : MonoBehaviour {

	public Button[] lvlButtons;
	private int levelAt;
	private int scene;
	public GameObject loadingscreen;

	void Start () {
		levelAt = PlayerPrefs.GetInt("levelAt", 2);

		for(int i=0; i< lvlButtons.Length; i++){
			if (i + 1 > levelAt)
				lvlButtons [i].interactable = false;
		}
	}

	IEnumerator LoadAsynchronously (int scene){
		AsyncOperation op = SceneManager.LoadSceneAsync (scene);
		loadingscreen.SetActive (true);

		while (!op.isDone) {
			float progress = Mathf.Clamp01 (op.progress / .9f);
			yield return null;
		}
	}

	public void SelectScene(int scene){
			StartCoroutine (LoadAsynchronously (scene));
		}
	}
