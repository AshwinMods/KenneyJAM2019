using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotCunt : MonoBehaviour
{

    public Vector2 sensivity = Vector2.one;
    public Transform[] targetObjects;
    public float accuracy;
    public Image accColor;
    public Text accText;

    public void Input_OnDragBegin(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Set_Rotation(p.delta);
    }
    public void Input_OnDrag(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Set_Rotation(p.delta);
    }
    public void Input_OnDragEnds(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Set_Rotation(p.delta);
    }

    public void Set_Rotation( Vector3 r )
    {
        foreach (var item in targetObjects)
        {
            item.RotateAround(Vector3.up, -r.x * Mathf.Deg2Rad * sensivity.x);
            item.RotateAround(Vector3.right, r.y * Mathf.Deg2Rad * sensivity.y);
            //accuracy = Mathf.Abs(item.forward.z);
            //accuracy = Mathf.Pow(accuracy, 50); 
        }
    }

    private void Update()
    {
        accuracy = Mathf.Abs(targetObjects[0].forward.z);
        accText.text = System.Math.Round(accuracy * 100, 1) + "%";
        accuracy = Mathf.Pow(accuracy, 50);
        if (accColor) accColor.color = (Color.green * accuracy) + (Color.red * (1-accuracy));
    }

    public void Randomize_Rotation()
    {
        Set_Rotation(new Vector3(Random.value * 360, Random.value * 360));
        Set_Rotation(new Vector3(Random.value * 360, Random.value * 360));
        Set_Rotation(new Vector3(Random.value * 360, Random.value * 360));
        Set_Rotation(new Vector3(Random.value * 360, Random.value * 360));
        Set_Rotation(new Vector3(Random.value * 360, Random.value * 360));
    }
}
