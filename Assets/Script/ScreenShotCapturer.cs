using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//DateTime使用のため

public class ScreenShotCapturer : MonoBehaviour
{//デスクトップにスクショ撮れるように変更。使わない時は非アクティブにしておく
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CaptureScreenShot("ScreenShot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string fileName = "ScreenShot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            string filePath = System.IO.Path.Combine(desktopPath, fileName);
            CaptureScreenShot(filePath);
        }
    }

    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log("スクリーンショット");
    }
}
