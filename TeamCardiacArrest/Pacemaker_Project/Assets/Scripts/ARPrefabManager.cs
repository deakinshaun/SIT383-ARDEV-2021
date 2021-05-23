using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPrefabManager : MonoBehaviour
{
    public GameObject prefab;
	public Texture[] roomTextures;
    public Dictionary<string, Texture> arTextures = new Dictionary<string, Texture>();

	private void Awake()
	{

		foreach(Texture tex in roomTextures)
		{
			Texture newTexture = Instantiate(tex);
			newTexture.name = tex.name;
			arTextures.Add(tex.name, tex);
		}

		Instantiate(prefab);
		
	}
	public void Room1Selection()
	{
		//Room 1 texture
	}
	public void Room2Selection()
	{
		//Attach room 2 texture
	}
}
