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
       
       private string[] buttonTexts2 = {
           "<size=30>に いち が に</size>\n2 \u00d7 1 = 2", "<size=30>に にん が し</size>\n2 \u00d7 2 = 4",
           "<size=30>に さん が ろく</size>\n2 \u00d7 3 = 6",
           "<size=30>に し が はち</size>\n2\u00d7 4 = 8", "<size=30>に ご じゅう</size>\n2 \u00d7 5 = 10",
           "<size=30>に ろく じゅうに</size>\n2 \u00d7 6 = 12", "<size=30>に しち じゅうし</size>\n2 \u00d7 7 = 14", 
           "<size=30>にはち じゅうろく</size>\n2\u00d7 8 = 16","<size=30>に く じゅうはち</size>\n2 \u00d7 9 = 18"
       };
   
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
                       UpdateButtonTexts(selectedDigit);
                   }
               });
           }
           // ボタンのテキストを段ごとに設定
           for (int i = 0; i < calculateButtons.Length; i++)
           {
               TextMeshProUGUI buttonText = calculateButtons[i].GetComponentInChildren<TextMeshProUGUI>();
               //buttonText.text = buttonTexts2[i];
               int buttonNumber = i + 1;
               buttonText.text = $"{selectedDigit}\u00d7{buttonNumber}= {selectedDigit * buttonNumber}";
           }
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
           int index = (digit - 1) * 9 + (number - 1); // インデックスの計算
           print($"index_{index}");
           if (index >= 0 && index < audioClipKuku.Length)
           {
               // 適切なAudioClipを再生
               audioSourceKuku.PlayOneShot(audioClipKuku[index]);
           }
       }

   
       // 計算ボタンが押された時の処理
       public void Calculate(int ButtonNum)
       {
           print($"トグルの段{selectedDigit}");
           int result = ButtonNum * selectedDigit; // 九九の計算
           Debug.Log($"掛け算の結果{result}");
           //resultText.text = result.ToString(); // 結果を表示
           // 九九の計算結果を元に対応するAudioClipを再生
           PlayKukuSE(selectedDigit,ButtonNum);
       }
}
