using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photographer
{

    private int resWidth = 32;
    private int resHeight = 20;

    private Camera _targetCamera;

    private RenderTexture rt;
    public Photographer(Camera targetCamera)
    {
        _targetCamera = targetCamera;
        rt = new RenderTexture(resWidth, resHeight, 24);
    }


    public static string ScreenShotName(int index, float forward, float left, float backward)
    {
        return string.Format("{0}/screenshots/{1}_{2}_{3}_{4}.png", Application.dataPath, index,forward,left,backward);
    }


    public void TakeScreenShotToFile(int index, float forward, float right, float backward)
    {

        _targetCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.R8, false);
        _targetCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        _targetCamera.targetTexture = null;
        // RenderTexture.active = null;

        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(index, forward, right, backward);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }


    public Texture2D TakeScreenShot()
    {
        _targetCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.R8, false);
        _targetCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        _targetCamera.targetTexture = null;
        return screenShot;
    }





}
