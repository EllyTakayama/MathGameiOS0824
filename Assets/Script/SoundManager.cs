using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//1016

public class SoundManager : MonoBehaviour
{
    //シングルトン設定(音を管理するものなど)
    //シーン間でのデータ共有、オブジェクト共有
    //書き方
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public AudioSource audioSourseBGM;//BGMスピーカー
    public AudioClip[] audioClipsBGM;//BGMの素材　0：タイトル、1：タウン、2：クエスト、3：バトル

    public AudioSource audioSourceSE;//SoundEffectのスピーカー
    public AudioClip[] audioClipSE;//ならす音源

    //public bool BGMis;//toggleでのBGMのオンオフ設定
    //public bool SEis;//toggleでのSEのオンオフ設定

    public void SetBgmVolume(float bgmVolume)
    {
        audioSourseBGM.volume = bgmVolume;
    }

    public void SetSeVolume(float seVolume)
    {
        audioSourceSE.volume = seVolume;
    }

    public void StopBGM()
    {
        audioSourseBGM.Stop();
    }
    public void StopSE()
    {
        audioSourceSE.Stop();
    }

    public void PlayBGM(string panelName)
    {
        audioSourseBGM.Stop();
        switch (panelName)
        {
            default:
            case "TopMenuPanel":
                audioSourseBGM.clip = audioClipsBGM[0];
                break;

            case "ModeMenuPanel":
                audioSourseBGM.clip = audioClipsBGM[1];
                break;

            case "Renshuu":
                    audioSourseBGM.clip = audioClipsBGM[2];
                break;
            
            case "Test":
                audioSourseBGM.clip = audioClipsBGM[3];
                break;
            case "GameOverPanel":
                audioSourseBGM.clip = audioClipsBGM[4];
                break;

        }
        audioSourseBGM.Play();
    }
/*
 * SE効果音 0正解 1不正解　2落下音ひよひよ 3ひよこの鳴き声ピヨピヨ 4ボタン操作音
 * 5移動音プーい 6成長音ぷるるる 7プイ効果音 8プぅーい効果音 9endがっかりチーン
 * 10小さいボタン操作 11すごく小さいボタン操作音　12歓声
 * 13ファンファーレ 14風船が割れる音 15ガチャを回す音 16コインをゲットする音ちょい長い
 * 17 コインを1かいゲットする音 18ジャン 19jingleパッ 20爆発音
 */
    public void StopPlaySE()
    {//効果音を止める
        audioSourceSE.Stop();
    }
    public void PlaySE0()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[0]);
    }//正解だとなる

    public void PlaySE1()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[1]);
    }//まちがいだとなる

    public void PlaySE2()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[2]);
    }//おやつをあげる時になる

    public void PlaySE3()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[3]);
        //ピヨがおやつを食べるとなる
    }

    public void PlaySEButton()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[4]);
        //Buttonの操作音
    }
    public void PlaySE5End1()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[5]);
        //点数
    }

    public void PlaySE6End2()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[6]);
        //点数
    }

    public void PlaySE7End3()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[7]);
        //点数
    }
    public void PlaySE8End4()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[8]);
        //点数
    }
    public void PlaySE9End5()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[9]);
        //点数
    }
    public void PlaySE10Button2()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[10]);
        //
    }
    public void PlaySE11Button3()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[11]);
        //
    }
    public void PlaySE12GradePanel()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[12]);
        //GradePanelが出てきた時の効果音
    }
    public void PlaySE13rewardButton()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[13]);
        //
    }
    public void PlaySE14BreakBalloon()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[14]);
        
    }
    public void PlaySE15Gacha()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[15]);
        
    }
    public void PlaySE16GetCoin()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[16]);
        
    }
    public void PlaySE17CoinOneceGet()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[17]);
        
    }
    public void PlaySE18()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[18]);
        Debug.Log("PlaySE18");
    }
    public void PlaySE19()
    {
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[19]);
    }
    public void PlaySE20()
    {//爆発音
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[20]);
    }
    public void PlaySE21()
    {//子供の歓声
        audioSourceSE.Stop();
        audioSourceSE.PlayOneShot(audioClipSE[21]);
    }
    

    public void BGMmute()
    {
        audioSourseBGM.mute = true;
    }

    public void UnmuteBGM()
    {
        audioSourseBGM.mute = false;
    }

    public void SEmute()
    {
        audioSourceSE.mute = true;
    }

    public void UnmuteSE()
    {
        audioSourceSE.mute = false;
    }

}
