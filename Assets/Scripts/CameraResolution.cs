using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera=GetComponent<Camera>();
        Rect rect=camera.rect;
        float scaleHeight=((float)Screen.width/Screen.height)/((float)16/9);
        float scaleWidth=1f/scaleHeight;

        if(scaleHeight<1)
        {
            rect.height=scaleHeight;
            rect.y=(1f-scaleHeight)/2f;
        }

        camera.rect=rect;
    }
}
