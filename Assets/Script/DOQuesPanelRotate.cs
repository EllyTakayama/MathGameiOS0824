using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween
using Sirenix.OdinInspector; //Odin

    public class DOQuesPanelRotate : MonoBehaviour
    {
        Vector3 _initialPosition;  // 追加: 初期位置を保存する変数
        Vector3 _initialRotation;//x90回転の保存
        Vector3 _finalRotation;//x0最終どこになるか
        
        void Start()
        {
            _initialPosition = transform.localPosition;
            Debug.Log($"初期時の取得{_initialPosition}");
            _initialRotation = new Vector3(90, 0, 0);
            _finalRotation = new Vector3(0, 0, 0);
            
        }
        [Button("QuesPanel表示")]　//←[Button("ラベル名")]
        public void VisibleQuesPanel()
        {
            // 初期位置に戻す
            transform.localPosition = _initialPosition;
            
            // アニメーションの設定
            transform.rotation = Quaternion.Euler(_initialRotation);
            transform.DORotate(_finalRotation, 0.4f);
        }
        [Button("QuesPanel非表示")]　//←[Button("ラベル名")]
        public void InVisibleQuesPanel()
        {
            transform.DORotate(_initialRotation, 0.3f);
        }

    }

