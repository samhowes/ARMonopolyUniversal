using UnityEngine;
using System.Collections;

public class ChangeScript : MonoBehaviour {
	public string magicNumber;

	public static string defaultImageFileName = "LOGO";
	public static string imagesDirectoryName = "images";
	public const string dynamicImagePrefix = "imageTargetID";
	public const string imageFileExtension = ".png";
	public WWW www; 
	
	public void Start () 
	{
		AttemptToLoadAvatar ();	
	}
	public void AttemptToLoadAvatar()
	{
		//the path file name using streamingAssetsPath, the function can be used for any device
		// "file://" so the function knows we are accessing a file in the folder instead of a http address
		string dynamicImagePath;
		string defaultImagePath;
		
		string imagesDirectoryPath;
		
		if (Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			imagesDirectoryPath = Application.dataPath + "/../../Documents/" + imagesDirectoryName + "/";
		} 
		else 
		{
			imagesDirectoryPath = Application.streamingAssetsPath + "/" + imagesDirectoryName + "/";
		}
		
		dynamicImagePath = imagesDirectoryPath + dynamicImagePrefix + magicNumber + imageFileExtension;
		defaultImagePath = imagesDirectoryPath + defaultImageFileName + imageFileExtension;
		
		
		string pathToLoad = null;
		if (System.IO.File.Exists (dynamicImagePath)) 
		{
			Debug.Log("ARMUnity" + magicNumber + ": Found custom image file! Loading from: " + dynamicImagePath);
			pathToLoad = dynamicImagePath;
		} 
		else if (System.IO.File.Exists (defaultImagePath)) 
		{
			Debug.Log("ARMUnity" + magicNumber + ": Found default logo file Loading from: " + defaultImagePath);
			pathToLoad = defaultImagePath;
		}
		else
		{
			Debug.Log("ARMUnityError" + magicNumber + ": Failed to find any images");
			Debug.Log("DefaultImagePath" + magicNumber + ": " + defaultImagePath);
			Debug.Log("DynamicImagePath" + magicNumber + ": " + dynamicImagePath);
		}
		if (pathToLoad != null) 
		{
			Texture2D tempTex = new Texture2D(4, 4);
			tempTex.LoadImage(System.IO.File.ReadAllBytes(pathToLoad));
			renderer.material.mainTexture = tempTex;
		}
	}
	//Grabing the url of the path and creating the png or jpg file
	IEnumerator GrabFile(string path) 
	{
   		www = new WWW(path);
    	yield return www; 
	}
}
