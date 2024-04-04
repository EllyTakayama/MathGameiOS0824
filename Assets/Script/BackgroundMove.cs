using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = -30;
    Vector3 cameraRectMin;
    Vector3 cameraBottom;
    // X軸方向のスクロールを有効にするかどうかのフラグfalseの場合はy軸に移動する
    public bool scrollXEnabled = true;
    void Start()
    {
        //カメラの範囲を取得
        cameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        cameraBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Debug.Log($"cameraRectMin_{cameraRectMin}");
        Debug.Log($"cameraBottom_{cameraBottom}");
    }
    void Update()
    {
        Move();
    }
    /*
     * Vector3.right 右移動
     * Vector3.down　下移動
     */
    void Move()
    {
        if (scrollXEnabled)//x軸に移動
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); //X軸方向にスクロール
            // 瞬間移動させるために、背景の位置をカメラの右端の少し右に設定する

            //カメラの左端から完全に出たら、右端に瞬間移動
            if (transform.position.x <= -1000f ) {
                transform.position = new Vector3 (950f, 0, 0);
            }
        }
        else//y軸に移動 SpriteのpivotをBottomにしておく
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime); //y軸方向にスクロール
            // カメラの下端から完全に出たら、上端に瞬間移動
            if (transform.position.y < (cameraRectMin.y - Camera.main.transform.position.y) * 2)
            {
                transform.position = new Vector2(transform.position.x,
                    (Camera.main.transform.position.y - cameraRectMin.y) * 2);
            }
      
        }
    }

}
