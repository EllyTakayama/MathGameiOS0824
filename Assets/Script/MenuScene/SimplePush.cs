using System.Collections;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class SimplePush : MonoBehaviour
{
    private int[] notificationIds = new int[4];//int型の配列を作成　配列の数4　通知ID用

    private void Start()
    {
        //アプリを起動した時に処理
        //もう一度テストする場合は、アプリをタスクキルして再起動してください

        //Android通知許可ウインドウの表示
        StartCoroutine(RequestNotificationPermission());

        //通知IDの設定　今回は4つ分を作成
        notificationIds[0] = (int)MyNotificationType.TestPush1;
        notificationIds[1] = (int)MyNotificationType.TestPush2;
        notificationIds[2] = (int)MyNotificationType.TestPush3;
        notificationIds[3] = (int)MyNotificationType.TestPush4;


        //通知チャンネルIDの設定
        string channelId = "pushtest_test000000_channel";

#if UNITY_ANDROID
        //プッシュ通知用のチャンネルを作成・登録
        AndroidLocalPushNotification.RegisterChannel(channelId, "テスト用の通知", "テストを通知します");
  
#endif

        //通知の削除
        LocalPushNotification.LocalPushClear(notificationIds);

        //通知内容を作成して追加
        LocalPushNotification.AddNotification("プッシュ通知1", "45秒後の通知です", 1, 45, channelId, notificationIds[0]);
        LocalPushNotification.AddNotification("プッシュ通知2", "60秒後の通知です", 2, 60, channelId, notificationIds[1]);
        LocalPushNotification.AddNotification("プッシュ通知3", "75秒後の通知です", 3, 75, channelId, notificationIds[2]);
        LocalPushNotification.AddNotification("プッシュ通知4", "90秒後の通知です", 4, 90, channelId, notificationIds[3]);
    }


    //Android通知許可ウインドウの表示
    private IEnumerator RequestNotificationPermission()
    {
        Debug.Log("simplePush");
#if UNITY_ANDROID
            PermissionRequest request = new PermissionRequest();
            while (request.Status == PermissionStatus.RequestPending)
#endif
            yield return null;
    }

}