using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkins : MonoBehaviour {

	public Button[] skinbuttons;
	public GameObject[] effects;
	public TMP_Text[] textmesh;
	public Material playermaterial;
	public Color[] colors;
	private int pickupscount, skinset;

	void Start () {
		pickupscount = Data.data.Pickupcount;
		if(pickupscount < 10)
			textmesh[0].text = "0" + pickupscount.ToString () + "/30";
		else
			textmesh[0].text = pickupscount.ToString () + "/30";

		if (PlayerPrefs.GetInt ("Skin") == 0) {
			playermaterial.color = colors [0];
			textmesh [1].text = "equiped";
		} else if (PlayerPrefs.GetInt ("Skin") == 1) {
			playermaterial.color = colors [1];
			textmesh[2].text = "equiped";
		} else if (PlayerPrefs.GetInt ("Skin") == 2) {
			playermaterial.color = colors [2];
			textmesh[3].text = "equiped";
		} else if (PlayerPrefs.GetInt ("Skin") == 3) {
			playermaterial.color = colors [3];
			textmesh[4].text = "equiped";
		} else if (PlayerPrefs.GetInt ("Skin") == 4) {
			playermaterial.color = colors [4];
			textmesh[5].text = "equiped";
		}

		if (pickupscount < 0) {
			skinbuttons [0].interactable = false;
			textmesh [1].text = "locked";
		}else
			textmesh [1].text = "use";
		
		if (pickupscount < 5) {
			skinbuttons [1].interactable = false;
			textmesh [2].text = "locked";
		}else
			textmesh [2].text = "use";
		
		if (pickupscount < 10) {
			skinbuttons [2].interactable = false;
			textmesh [3].text = "locked";
		}else
			textmesh [3].text = "use";
		
		if (pickupscount < 15) {
			skinbuttons [3].interactable = false;
			textmesh [4].text = "locked";
		}else
			textmesh [4].text = "use";
		
		if (pickupscount < 20) {
			skinbuttons [4].interactable = false;
			textmesh [5].text = "locked";
		} else
			textmesh [5].text = "use";

	}

	public void white(){
		playermaterial.color = colors [0];
		PlayerPrefs.SetInt ("Skin", 0);
		StartCoroutine (Pause0 ());
	}
	public void blue(){
		playermaterial.color = colors [1];
		PlayerPrefs.SetInt ("Skin", 1);
		StartCoroutine (Pause1 ());
	}
	public void gold(){
		playermaterial.color = colors [2];
		PlayerPrefs.SetInt ("Skin", 2);
		StartCoroutine (Pause2 ());
	}
	public void orange(){
		playermaterial.color = colors [3];
		PlayerPrefs.SetInt ("Skin", 3);
		StartCoroutine (Pause3 ());
	}
	public void violet(){
		playermaterial.color = colors [4];
		PlayerPrefs.SetInt ("Skin", 4);
		StartCoroutine (Pause4 ());
	}

	IEnumerator Pause0(){
		effects [0].SetActive (true);
		if(skinbuttons [1].interactable == true)
			textmesh [2].text = "use";
		if(skinbuttons [2].interactable == true)
			textmesh [3].text = "use";
		if(skinbuttons [3].interactable == true)
			textmesh [4].text = "use";
		if(skinbuttons [4].interactable == true)
			textmesh [5].text = "use";
		textmesh[1].text = "equiped";
		yield return new WaitForSeconds (1);
		effects [0].SetActive (false);
	}

	IEnumerator Pause1(){
		effects [1].SetActive (true);
		if(skinbuttons [0].interactable == true)
			textmesh [1].text = "use";
		if(skinbuttons [2].interactable == true)
			textmesh [3].text = "use";
		if(skinbuttons [3].interactable == true)
			textmesh [4].text = "use";
		if(skinbuttons [4].interactable == true)
			textmesh [5].text = "use";
		textmesh[2].text = "equiped";
		yield return new WaitForSeconds (1);
		effects [1].SetActive (false);
	}
	IEnumerator Pause2(){
		effects [2].SetActive (true);
		if(skinbuttons [0].interactable == true)
			textmesh [1].text = "use";
		if(skinbuttons [1].interactable == true)
			textmesh [2].text = "use";
		if(skinbuttons [3].interactable == true)
			textmesh [4].text = "use";
		if(skinbuttons [4].interactable == true)
			textmesh [5].text = "use";
		textmesh[3].text = "equiped";
		yield return new WaitForSeconds (1);
		effects [2].SetActive (false);
	}
	IEnumerator Pause3(){
		effects [3].SetActive (true);
		if(skinbuttons [0].interactable == true)
			textmesh [1].text = "use";
		if(skinbuttons [1].interactable == true)
			textmesh [2].text = "use";
		if(skinbuttons [2].interactable == true)
			textmesh [3].text = "use";
		if(skinbuttons [4].interactable == true)
			textmesh [5].text = "use";
		textmesh[4].text = "equiped";
		yield return new WaitForSeconds (1);
		effects [3].SetActive (false);
	}
	IEnumerator Pause4(){
		effects [4].SetActive (true);
		if(skinbuttons [0].interactable == true)
			textmesh [1].text = "use";
		if(skinbuttons [1].interactable == true)
			textmesh [2].text = "use";
		if(skinbuttons [2].interactable == true)
			textmesh [3].text = "use";
		if(skinbuttons [3].interactable == true)
			textmesh [4].text = "use";
		textmesh[5].text = "equiped";
		yield return new WaitForSeconds (1);
		effects [4].SetActive (false);
	}
}
