using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialPresetSwitcher : MonoBehaviour
{
    //[SerializeField] private Material englishMaterialPreset;
    //[SerializeField] private Material japaneseMaterialPreset;
    //[SerializeField] private TMP_FontAsset EnglishFontAsset;
    //[SerializeField] private TMP_FontAsset JapaneseFontAsset;
    
    //0_china 1_English 2_Japan 3_Korea
    [SerializeField] private Material[] materialPresets;
    [SerializeField] private TMP_FontAsset[] fontAssets;
    [SerializeField] private TextMeshProUGUI textMeshPro;//インスペクターからアタッチ
    [SerializeField] private TMP_FontAsset _fontAsset;
    void Start()
    {
        // TextMeshProUGUIコンポーネントを取得
        //textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUIコンポーネントがアタッチされていません。");
            return;
        }
    }
    
    public void SwitchMaterialPreset()
    {
        // TextMeshProUGUIコンポーネントからFontAssetを取得
        _fontAsset = textMeshPro.font;
        if (_fontAsset == null)
        {
            Debug.LogError("TextMeshProUGUIのFontAssetが設定されていません。");
        }
        // FontAssetに対応するMaterial Presetを適用
        for (int i = 0; i < fontAssets.Length; i++)
        {
            if (fontAssets[i] == _fontAsset)
            {
                ApplyMaterialPreset(materialPresets[i]);
                break;
            }
        }
        /*
        // Fontアセットがnullでないかチェック
        if (_fontAsset != null)
        {
            // FontアセットがEnglishFontAssetかJapaneseFontAssetかを判定し、対応するMaterial Presetを適用
            if (_fontAsset == EnglishFontAsset)
            {
                ApplyMaterialPreset(englishMaterialPreset);
            }
            else if (_fontAsset == JapaneseFontAsset)
            {
                ApplyMaterialPreset(japaneseMaterialPreset);
            }
        }*/
        /*
        if (fontAsset == EnglishFontAsset || fontAsset == JapaneseFontAsset)
        {
            Material selectedMaterialPreset = SelectMaterialPreset(fontAsset);
            ApplyMaterialPreset(selectedMaterialPreset);
        }*/
    }
    
    private void ApplyMaterialPreset(Material material)
    {
        // Material Presetを適用
        if (textMeshPro != null && material != null)
        {
            textMeshPro.fontSharedMaterial = material;
        }
    }


}
