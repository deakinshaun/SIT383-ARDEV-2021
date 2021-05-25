using UnityEngine;
using UnityEngine.UI;

public enum InputPosition
{
    down, held, release, none
}

public class ARcontroller : MonoBehaviour
{
    public Camera arCamera;
    private Vector2 touchStartPos = Vector2.zero;
    private Vector2 touchEndPos = Vector2.zero;

    private float touchStartGyroRoll = 0.0f;
    private float touchEndGyroRoll = 0.0f;
    private Vector3 touchEndUpVector = Vector3.zero;

    private GameObject touchDragObject = null;

    private void Start()
    {
        _ShowAndroidToastMessage("This is an android toast...");
        arCamera = arCamera.GetComponent<Camera>();
        Input.gyro.enabled = true;
    }


    void Update()
    {
        // Touch detection
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        {
            Vector2 screenInputPosition = Vector2.zero;
            InputPosition inputType = InputPosition.none;

            // Touch Input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                screenInputPosition = Input.GetTouch(0).position;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        inputType = InputPosition.down;
                        break;
                    case TouchPhase.Moved:
                        inputType = InputPosition.held;
                        break;
                    case TouchPhase.Ended:
                        inputType = InputPosition.release;
                        break;
                }
            }

            // Mouse Input
            if (Input.GetMouseButtonUp(0)) {
                screenInputPosition = Input.mousePosition;
                inputType = InputPosition.release;
            }

            if (Input.GetMouseButtonDown(0))
            {
                screenInputPosition = Input.mousePosition;
                inputType = InputPosition.down;
            } else if (Input.GetMouseButton(0))
            {
                screenInputPosition = Input.mousePosition;
                inputType = InputPosition.held;
            }


            //Debug.unityLogger.Log("TCPAR", "Input Type: " + inputType);

            RaycastHit hit = RaycastFromScreen(screenInputPosition);

            switch (inputType)
            {
                case InputPosition.down:
                    if (hit.collider != null)
                    {
                        if (hit.transform.gameObject.tag == "ar_dial")
                        {
                            touchStartPos = screenInputPosition;
                            touchDragObject = hit.transform.gameObject;
                            touchStartGyroRoll = GetDeviceRoll();
                        }
                    }
                    break;
                case InputPosition.release:
                    if (touchStartPos == Vector2.zero)
                    {
                        if (hit.collider != null)
                        {
                            if (hit.transform.gameObject.tag == "ar_btn")
                            {
                               // _ShowAndroidToastMessage("AR Button hit by ray... " + hit.transform.name);
                                hit.transform.SendMessage("ARTap");
                            }
                            touchEndPos = screenInputPosition;
                        }
                    }
                    else
                    {
                        // User pressed a dial object
                        touchEndPos = screenInputPosition;
                        touchEndGyroRoll = GetDeviceRoll();
                       
                        var totalRotationChange = touchStartGyroRoll - touchEndGyroRoll;

                        if (totalRotationChange > 180 && totalRotationChange < 360 )
                        {
                            totalRotationChange = totalRotationChange - 360;
                        }

                        Debug.unityLogger.Log("TCPAR", "Start Rotation: " + touchStartGyroRoll +  " end: " + touchEndGyroRoll);

                        _ShowAndroidToastMessage("Total Rotation Angle Change: " + totalRotationChange);
#if !UNITY_EDITOR
                        if (totalRotationChange > 0)
                        {
                            if (touchDragObject != null)
                            {
                                // touch ended to the right of start position
                                touchDragObject.SendMessage("ARDragRight");

                                Debug.unityLogger.Log("TCPAR", "Detected rotation right on " + touchDragObject.name);
                            }
                        }
                        else if (totalRotationChange < 0)
                        {
                            if (touchDragObject != null)
                            {
                                // touch ended to the left of start position
                                touchDragObject.SendMessage("ARDragLeft");

                                Debug.unityLogger.Log("TCPAR", "Detected rotation left on " + touchDragObject.name);
                            }
                        }
#endif
#if UNITY_EDITOR
                        if (touchEndPos.x - touchStartPos.x > 0)
                        {
                            if (touchDragObject != null)
                            {
                                // touch ended to the right of start position
                                touchDragObject.SendMessage("ARDragRight");

                                Debug.unityLogger.Log("TCPAR", "Detected drag right on " + touchDragObject.name);
                            }
                        }
                        else
                        {
                            if (touchDragObject != null)
                            {
                                // touch ended to the left of start position
                                touchDragObject.SendMessage("ARDragLeft");

                                Debug.unityLogger.Log("TCPAR", "Detected drag left on " + touchDragObject.name);
                            }
                        }
#endif
                        // Reset touch vars
                        touchStartGyroRoll = 0.0f;
                        touchEndGyroRoll = 0.0f;
                        touchDragObject = null;
                        touchStartPos = Vector2.zero;
                        touchEndPos = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private RaycastHit RaycastFromScreen(Vector3 screenPos)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPos);

        //_ShowAndroidToastMessage("Touch pos " + screenPos);

        //_ShowAndroidToastMessage("Raycasting to button from " + ray.direction);
        //Debug.unityLogger.Log("TCPAR", "Raycasting to button from " + ray.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit;

        }
        return hit;
    }

    // Gyro rotation code example from Unity.com: https://answers.unity.com/questions/434096/lock-rotation-of-gyroscope-controlled-camera-to-fo.html
    float GetDeviceRoll()
    {
        Quaternion referenceRotation = Quaternion.identity;
        Quaternion deviceRotation = ReadGyroscopeRotation();
        Quaternion eliminationOfXY = Quaternion.Inverse(
            Quaternion.FromToRotation(referenceRotation * Vector3.forward,
                                      deviceRotation * Vector3.forward)
        );
        Quaternion rotationZ = eliminationOfXY * deviceRotation;
        float roll = rotationZ.eulerAngles.z;
        return roll;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private static Quaternion ReadGyroscopeRotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    private void _ShowAndroidToastMessage(string message)
    {
        Debug.unityLogger.Log("TCPAR", "*ANDROID TOAST*:" + message);
#if !UNITY_EDITOR

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
#endif
    }

}