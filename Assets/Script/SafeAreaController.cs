using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class SafeAreaController : MonoBehaviour
{ 
    [SerializeField] private RectTransform panelRectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);
    private void Start()
    {
        SetSafeArea();
        // 画面のサイズが変化した時にSafeAreaを更新する
        Observable.EveryUpdate().Select(_ => Screen.safeArea)
            .DistinctUntilChanged()
            .Subscribe(safeArea =>
            {
                if (safeArea != lastSafeArea)
                    ApplySafeArea(safeArea);
            })
            .AddTo(this);
    }
    private void ApplySafeArea(Rect r)
    {
        lastSafeArea = r;
        SetSafeArea();

    }

    public void SetSafeArea()
    {
        if (panelRectTransform == null || Screen.width <= 0 || Screen.height <= 0)
            return;

        var area = Screen.safeArea;

        // Unity 6のGameビューやAndroidの縦画面切り替え直後は、
        // Screen.safeAreaとScreen.width/heightの向きが一時的に一致しないことがある。
        // その状態の値をアンカーへ適用すると、Anchor Maxが1を超えてUIが画面外へずれる。
        bool isValidSafeArea = area.width > 0f
            && area.height > 0f
            && area.xMin >= 0f
            && area.yMin >= 0f
            && area.xMax <= Screen.width
            && area.yMax <= Screen.height;

        if (!isValidSafeArea)
        {
            panelRectTransform.anchorMin = Vector2.zero;
            panelRectTransform.anchorMax = Vector2.one;
            return;
        }

        /*

        print("panelRectTransform_" + panelRectTransform);
        print("Screen.safeArea" + Screen.safeArea);
        print("area.position" + area.position);
        print("area.position + area.size" + area.position + area.size);
        */
        

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panelRectTransform.anchorMin = anchorMin;
        panelRectTransform.anchorMax = anchorMax;
        /*
        print("anchorMin.x_" + anchorMin.x);
        print("anchorMin.y_" + anchorMin.y);
        print("anchorMax.x_" + anchorMax.x);
        print("anchorMax.y" + anchorMax.y);

        print("panelRectTransform.anchorMin_" + panelRectTransform.anchorMin);
        print("panelRectTransform.anchorMax" + panelRectTransform.anchorMax);
        */
    }

}

