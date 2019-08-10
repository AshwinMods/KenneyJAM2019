using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotCunt : MonoBehaviour
{

    public Vector2 sensivity = Vector2.one;
    public Transform[] targetObjects;

    public Vector3 curAngle;
    public Vector3 targetAngle;
    public float accuracy;
    public Image accColor;

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

    public Quaternion cRot;

    public void Set_Rotation( Vector3 r )
    {
        foreach (var item in targetObjects)
        {
            item.RotateAround(Vector3.up, -r.x * Mathf.Deg2Rad * sensivity.x);
            item.RotateAround(Vector3.right, r.y * Mathf.Deg2Rad * sensivity.y);
            curAngle = item.eulerAngles;

            cRot = item.rotation;
        }
    }
}
