using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject selectedObj;
    public static Manager instance;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
