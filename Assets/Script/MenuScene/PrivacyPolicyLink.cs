using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//ピヨとかけざんのプライバシーポリシーサイトへのリンク
public class PrivacyPolicyLink : MonoBehaviour
{
   public void PrivacyPolicyClick()
    {
        //urlの作成
        string url = "https://funfunnyapp.blog.jp/archives/16366328.html";

        //Twitter投稿画面の起動
        Application.OpenURL(url);
        print("InfoLink");
    }
}
