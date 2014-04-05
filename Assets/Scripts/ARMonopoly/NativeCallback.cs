using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;


public class NativeCallback : MonoBehaviour {
	private static NativeCallback self;
	public ChangeScript[] scripts;
	public static bool displayLabel = false;
	
	// Declare the external functions
	private delegate void ARMUnityCallbackWithBoolDelegate(bool shouldAcquireCamera);
	
	#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void ARMRegisterUnityCameraCallback(ARMUnityCallbackWithBoolDelegate callbackWithBool);
	#else
	private static void ARMRegisterUnityCameraCallback(ARMUnityCallbackWithBoolDelegate callbackWithBool) {}
	#endif
	
	[MonoPInvokeCallback(typeof(ARMUnityCallbackWithBoolDelegate))]
	public static void acquireCameraCallback(bool shouldAcquireCamera)
	{
		Debug.Log ("It fucking works!");
		if (shouldAcquireCamera) 
		{
			Debug.Log ("Acquiring camera!");
			// Stop and deinit camera
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);
			CameraDevice.Instance.Start();

			for (int ii = 0; ii < self.scripts.Length; ++ii)
			{
				self.scripts[ii].AttemptToLoadAvatar();
			}

		}
		else 
		{
			Debug.Log ("Releasing camera!");
			// Reinit and restart camera, selecting back camera
			CameraDevice.Instance.Stop();
			CameraDevice.Instance.Deinit();
		}
	
		displayLabel = true;
	}


	// Use this for initialization
	void Start () {
		self = this;
		ARMRegisterUnityCameraCallback (acquireCameraCallback);
	}
	/*
	void OnGUI()
	{
		if (GUI.Button (new Rect(50,150,200,50), "Restart Camera")) {
			
			// Stop tracker
			//TrackerManager.Instance.GetTracker<Tracker.Type.IMAGE_TRACKER>().Stop();
			
			// Stop and deinit camera
			CameraDevice.Instance.Stop();
			CameraDevice.Instance.Deinit();
			
			// Reinit and restart camera, selecting back camera
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);
			CameraDevice.Instance.Start();
			
			// Restart tracker
			//TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Start();
		}
		
	}*/
}

