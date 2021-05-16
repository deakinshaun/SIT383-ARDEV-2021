using UnityEngine;
public class Heart : MonoBehaviour
{
    private Vector3 initPos;
    public float distance = 1;
    public float speed = 1;
    private void Start() => initPos = transform.position;
    private void Update() => transform.position = new Vector3(initPos.x,initPos.y + Mathf.Sin(Time.time*speed)*distance, initPos.z);
}
