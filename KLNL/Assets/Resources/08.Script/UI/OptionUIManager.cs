using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUIManager : MonoBehaviourUI
{
    private Slider backSlider, effectSlider;
    float bgm;
    float effectSound;
    bool isMutebgm = false, isMuteEffect=false;
    Image bgmMuteImg, effectMuteImg;
    // Start is called before the first frame update
    void Awake()
    {
        bgmMuteImg = transform.Find("OptionBackground/Sound/BackgroundMusic/Mute").GetComponent<Image>();
        effectMuteImg = transform.Find("OptionBackground/Sound/SoundEffect/Mute").GetComponent<Image>();
        backSlider = transform.Find("OptionBackground/Sound/BackgroundMusic/Slider").gameObject.GetComponent<Slider>();
        effectSlider = transform.Find("OptionBackground/Sound/SoundEffect/Slider").gameObject.GetComponent<Slider>();
        backSlider.value = SoundManager.Instance.BGM.volume;
        effectSlider.value = SoundManager.Instance.Playeraction.volume;
        AddBtnEvent("OptionBackground/Back", CloseOptionUI);

        AddBtnEvent("OptionBackground/Sound/BackgroundMusic/Mute", MuteBackground);
        AddBtnEvent("OptionBackground/Sound/SoundEffect/Mute", MuteEffectSound);
   
    }
    private void Update()
    {
        bgm =  backSlider.value;
        effectSound =  effectSlider.value;
        CheckMuteImg();
        SoundManager.Instance.BGM.volume = bgm;
        SoundManager.Instance.Playeraction.volume = effectSound;
        
    }
    public void CloseOptionUI() 
    {
        SetUI(this.gameObject);
    }

    public void MuteBackground() {
        
        if (SoundManager.Instance.BGM.mute)
        {
            SoundManager.Instance.BGM.mute = false;
        }
        else 
        {
            SoundManager.Instance.BGM.mute = true;
        }
        isMutebgm = SoundManager.Instance.BGM.mute;
    }
    public void MuteEffectSound() {
        
        if (SoundManager.Instance.Playeraction.mute)
        {
            SoundManager.Instance.Playeraction.mute = false;
        }
        else
        {
            SoundManager.Instance.Playeraction.mute = true;
        }
        isMuteEffect = SoundManager.Instance.Playeraction.mute;
    }
    public void CheckMuteImg() 
    {
        if (isMutebgm) 
        { 
             bgmMuteImg.sprite = Resources.Load("04.Image/UIButton/optioncheckon_2", typeof(Sprite)) as Sprite; 
        }
        else 
        {
            bgmMuteImg.sprite = Resources.Load("04.Image/UIButton/optioncheckoff_1", typeof(Sprite)) as Sprite;
        }

        if (isMuteEffect) 
        {
            effectMuteImg.sprite = Resources.Load("04.Image/UIButton/optioncheckon_2", typeof(Sprite)) as Sprite;
        }
        else 
        {
            effectMuteImg.sprite = Resources.Load("04.Image/UIButton/optioncheckoff_1", typeof(Sprite)) as Sprite;
        }
    
    }
}
