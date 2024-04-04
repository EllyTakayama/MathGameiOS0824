using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KuKuSpawn : MonoBehaviour
{
   
    public Toggle[] digitToggles; // 1から9までの数字用のToggle
       //public GameObject multiplicationPanel; // 九九の段ごとのパネル
       //public Text resultText; // 答えを表示するテキスト
       [SerializeField] private Button[] calculateButtons; // 計算ボタン
       private int selectedDigit = 1; // 選択された数字
       public AudioSource audioSourceKuku;//SoundEffectのスピーカー
       public AudioClip[] audioClipKuku;//ならす音源
       [SerializeField] private GameObject[] _balloonPrefabs;
       //[SerializeField] private Transform balloonSpawnPoint; // 風船の生成位置
       [SerializeField] private Transform ballonCanvas; // Canvasを参照するTransform
       [SerializeField] private Sprite[] balloonImages;//風船のスプライト
       [SerializeField] private TextMeshProUGUI danText;//段の差し替え
       [SerializeField] private BalloonObjectPool balloonObjectPool;//バルーンオブジェクトプール取得
       void Start(){
   
           // 各Toggleに対してリスナーを設定
           for (int i = 0; i < digitToggles.Length; i++)
           {
               int digit = i + 1; // 1から9までの数字
               print($"toggle,{digit}");
               digitToggles[i].onValueChanged.AddListener((isOn) =>
               {
                   if (isOn)
                   {
                       selectedDigit = digit;
                       danText.text = $"{selectedDigit} のだん";
                       UpdateButtonTexts(selectedDigit);
                       // SEを再生
                       SoundManager.instance.PlaySE3();
                   }
               });
           }
           // ボタンのテキストを段ごとに設定
           //
           // 
           /*for (int i = 0; i < calculateButtons.Length; i++)
           {
               TextMeshProUGUI buttonText = calculateButtons[i].GetComponentInChildren<TextMeshProUGUI>();
               //buttonText.text = buttonTexts2[i];
               int buttonNumber = i + 1;
               buttonText.text = $"{selectedDigit}\u00d7{buttonNumber}= {selectedDigit * buttonNumber}";
           }*/
           UpdateButtonTexts(selectedDigit);
           danText.text = $"{selectedDigit} のだん";
       }
       // ボタンのテキストを更新
       private void UpdateButtonTexts(int digit)
       {
           for (int i = 0; i < calculateButtons.Length; i++)
           {
               TextMeshProUGUI buttonText = calculateButtons[i].GetComponentInChildren<TextMeshProUGUI>();
               int buttonNumber = i + 1;
               buttonText.text = $"{digit}\u00d7{buttonNumber}= {digit * buttonNumber}";
           }
       }
       public void PlayKukuSE(int digit, int number)
       {
           audioSourceKuku.Stop();
           if(Application.systemLanguage == SystemLanguage.Japanese)
           {
               // システム言語が日本語の場合の処理
               int index = (digit - 1) * 9 + (number - 1); // インデックスの計算
               print($"index_{index}");
               if (index >= 0 && index < audioClipKuku.Length)
               {
                   // 適切なAudioClipを再生
                   audioSourceKuku.PlayOneShot(audioClipKuku[index]);
               }
           }
           else
           {
               // それ以外の場合の処理
               SoundManager.instance.PlaySEButton();
           }
     
       }
       // 風船を生成してククボタンの答えを表示
       public void GenerateBalloon(int result)
       {
           // ランダムな x 座標を生成 (-150 から 150 の範囲)
           float randomX = Random.Range(-150f, 150f);
        
           // ランダムな y 座標を生成 (-400 から -100 の範囲)
           float randomY = Random.Range(-400f, -100f);
        
           // 風船の生成位置を設定
           Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
           
           // ランダムにspriteを選択
           int randomIndex = Random.Range(0, balloonImages.Length);
           // 風船の生成
           //GameObject balloon = Instantiate(_balloonPrefabs[0], spawnPosition, Quaternion.identity);
           // オブジェクトプールから取得したオブジェクトに変更
           GameObject balloon = balloonObjectPool.GetObject();
           
           balloon.GetComponent<Image>().sprite = balloonImages[randomIndex];
           // Canvasの子として設定
           balloon.transform.SetParent(ballonCanvas, false);
           balloon.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
           print($"表示位置_{spawnPosition}");
           // 風船の位置を設
           balloon.GetComponent<BallonClick>().balloonIndex = randomIndex;
           balloon.GetComponentInChildren<TextMeshProUGUI>().text = result.ToString();
       }

   
       // 計算ボタンが押された時の処理
       public void Calculate(int ButtonNum)
       {
           print($"トグルの段{selectedDigit}");
           int result = ButtonNum * selectedDigit; // 九九の計算
           Debug.Log($"掛け算の結果{result}");
           //resultText.text = result.ToString(); // 結果を表示
           GenerateBalloon(result);
           // 九九の計算結果を元に対応するAudioClipを再生
           PlayKukuSE(selectedDigit,ButtonNum);
       }
}
