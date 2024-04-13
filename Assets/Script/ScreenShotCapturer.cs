using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//DateTime使用のため

public class ScreenShotCapturer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureScreenShot("ScreenShot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");
        }
    }

    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log("スクリーンショット");
    }
}
