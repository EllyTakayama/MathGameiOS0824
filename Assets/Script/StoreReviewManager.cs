using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreReviewManager : MonoBehaviour
{
    public void RequestReview(){
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.Device.RequestStoreReview();
        Debug.Log("レビュー画面表示");
       
#else
        Debug.LogWarning("This platform is not support RequestReview.");
#endif
    }
}

