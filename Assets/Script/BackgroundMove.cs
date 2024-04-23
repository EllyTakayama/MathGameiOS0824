using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    Vector3 defaultPosition;
    [SerializeField]
    float scrollSpeed = -30;
    Vector3 cameraRectMin;
    Vector3 cameraBottom;
    // X軸方向のスクロールを有効にするかどうかのフラグfalseの場合はy軸に移動する
    public bool scrollXEnabled = true;
    void Start()
    {
        // GameObjectの初期位置を取得
        defaultPosition = transform.position;
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
            //カメラの下から完全に出たら、上に瞬間移動
            if (transform.position.y <= -1500f ) {
                transform.position = new Vector3 (defaultPosition.x, 1100f, 0);
            }
            // カメラの下端から完全に出たら、上端に瞬間移動
            /*
            if (transform.position.y < (cameraRectMin.y - Camera.main.transform.position.y) * 2)
            {
                transform.position = new Vector2(transform.position.x,
                    (Camera.main.transform.position.y - cameraRectMin.y) * 2);
            }
            */
        }
    }

}
