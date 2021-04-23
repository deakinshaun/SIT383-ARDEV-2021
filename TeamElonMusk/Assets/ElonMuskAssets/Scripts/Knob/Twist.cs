using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twist : MonoBehaviour
{
    public Transform knob;
    public Image fill;
    public Text value;
    Vector3 mousePos;

    public void onHandleDrag()
    {
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - knob.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        if (angle<=225 || angle>=315)
        {
            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            knob.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f);
            value.text = Mathf.Round((fill.fillAmount * 100) / 0.75f).ToString();


        }
    }
}
