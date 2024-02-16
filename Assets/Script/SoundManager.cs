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

    public void PlaySE0()
    {
        audioSourceSE.PlayOneShot(audioClipSE[0]);
    }//正解だとなる

    public void PlaySE1()
    {
        audioSourceSE.PlayOneShot(audioClipSE[1]);
    }//まちがいだとなる

    public void PlaySE2()
    {
        audioSourceSE.PlayOneShot(audioClipSE[2]);
    }//おやつをあげる時になる

    public void PlaySE3()
    {
        audioSourceSE.PlayOneShot(audioClipSE[3]);
        //ピヨがおやつを食べるとなる
    }

    public void PlaySEButton()
    {
        audioSourceSE.PlayOneShot(audioClipSE[4]);
        //Buttonの操作音
    }
    public void PlaySE5End1()
    {
        audioSourceSE.PlayOneShot(audioClipSE[5]);
        //点数
    }

    public void PlaySE6End2()
    {
        audioSourceSE.PlayOneShot(audioClipSE[6]);
        //点数
    }

    public void PlaySE7End3()
    {
        audioSourceSE.PlayOneShot(audioClipSE[7]);
        //点数
    }
    public void PlaySE8End4()
    {
        audioSourceSE.PlayOneShot(audioClipSE[8]);
        //点数
    }
    public void PlaySE9End5()
    {
        audioSourceSE.PlayOneShot(audioClipSE[9]);
        //点数
    }
    public void PlaySE10Button2()
    {
        audioSourceSE.PlayOneShot(audioClipSE[10]);
        //
    }
    public void PlaySE11Button3()
    {
        audioSourceSE.PlayOneShot(audioClipSE[11]);
        //
    }
    public void PlaySE12GradePanel()
    {
        audioSourceSE.PlayOneShot(audioClipSE[12]);
        //GradePanelが出てきた時の効果音
    }
    public void PlaySE13rewardButton()
    {
        audioSourceSE.PlayOneShot(audioClipSE[13]);
        //
    }
    public void PlaySE14BreakBalloon()
    {
        audioSourceSE.PlayOneShot(audioClipSE[14]);
        //
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
