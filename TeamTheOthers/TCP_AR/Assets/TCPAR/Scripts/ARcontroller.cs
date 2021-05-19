using UnityEngine;
using UnityEngine.UI;

public class ARcontroller : MonoBehaviour
{
    public Camera arCamera;

    private void Start()
    {
        _ShowAndroidToastMessage("This is an android toast...");
        arCamera = arCamera.GetComponent<Camera>();
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    /* RaycastHit hit;
                    var rayPos = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, arCamera.nearClipPlane));

                    _ShowAndroidToastMessage("Touch pos " + touch.position);

                    _ShowAndroidToastMessage("Raycasting to button from " + rayPos);
                    if (Physics.Raycast(rayPos, arCamera.transform.forward, out hit))
                    {
                        if (hit.transform.gameObject.tag == "ar_btn") {
                            _ShowAndroidToastMessage("AR Button hit by ray... ");
                            hit.transform.SendMessage ("HitByRay");
                        }
                    }*/
                    Ray ray = arCamera.ScreenPointToRay(touch.position);

                    _ShowAndroidToastMessage("Touch pos " + touch.position);

                    _ShowAndroidToastMessage("Raycasting to button from " + ray.direction);
                    Debug.unityLogger.Log("TCPAR", "Raycasting to button from " + ray.direction);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.gameObject.tag == "ar_btn")
                        {
                            _ShowAndroidToastMessage("AR Button hit by ray... " + hit.transform.name);
                            hit.transform.SendMessage("ARTap");
                        }
         
                    }
                    break;
            }
        }

    }

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>(
                    "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}