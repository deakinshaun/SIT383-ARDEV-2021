using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{

    public float speed;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            speed -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            speed += Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0.5f, 5);
        var inputX = -Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        if (Manager.instance.selectedObj != null)
        {
            var dis = new Vector3(inputX, 0,inputY) * Time.deltaTime * speed;
            Manager.instance.selectedObj.transform.position +=dis ;

        }
        
        if(!Input.GetMouseButtonDown(0))
            return;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.collider.tag == "PC")
            {
                Manager.instance.selectedObj = hit.collider.gameObject;
            }
            else if(hit.collider.tag=="machine")
            {
                Manager.instance.selectedObj = hit.collider.gameObject;
            }
        }
    }
}
