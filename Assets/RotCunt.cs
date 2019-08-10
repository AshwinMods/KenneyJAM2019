using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotCunt : MonoBehaviour
{

    public Vector3 sensivity = Vector3.one;
    public Transform[] targetObjects;

    public void Input_OnDragBegin(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Add_Rotation(p.delta);
    }
    public void Input_OnDrag(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Add_Rotation(p.delta);
    }
    public void Input_OnDragEnds(BaseEventData e)
    {
        var p = (PointerEventData)e;
        Add_Rotation(p.delta);
    }

    public void Pixel_Rotation(Vector3 r)
    {
        Add_Rotation(r);
    }

    Vector3 rot;
    //Quaternion rot;
    public void Add_Rotation(Vector3 r)
    {
        rot.x += r.y * sensivity.x;
        rot.y -= r.x * sensivity.y;
        Set_Rotation(rot);
    }

    public void Set_Rotation( Vector3 r )
    {
        foreach (var item in targetObjects)
        {
            item.eulerAngles = r;
        }
    }
}
