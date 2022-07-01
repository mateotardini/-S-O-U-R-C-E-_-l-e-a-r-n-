using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Data: MonoBehaviour {

	public int Pickupcount;
	public int[]Pickups;
	public static Data data;

	void Awake () {
		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		}else if (data != this){
			Destroy (gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		PlayerData playerdata = new PlayerData ();

		playerdata.Pickupcount = Pickupcount;
		playerdata.Pickups = Pickups;
		bf.Serialize (file, playerdata);

		file.Close ();
	}
		
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData playerdata = (PlayerData) bf.Deserialize (file);
			file.Close ();

			Pickupcount = playerdata.Pickupcount;
			Pickups = playerdata.Pickups;
		}
	}
}

[Serializable]
class PlayerData{
	public int Pickupcount;
	public int Pickup1state;
	public int Pickup2state;
	public int[] Pickups;
}