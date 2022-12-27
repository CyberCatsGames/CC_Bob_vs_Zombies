using Suriyun.MobileTPS.Code.Core;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MyYandex : MonoBehaviour {
    #region DLL's
    [DllImport("__Internal")]
    private static extern void ShowNormalAdExtern();

    [DllImport("__Internal")]
    private static extern void ShowRewardExtern();

    [DllImport("__Internal")]
    private static extern void CheckMobileExtern();
    #endregion

    public static MyYandex Instance;

    //delegate   ()
    public delegate void RewardedAdResult(bool isWatched);
    //event  
    public static event RewardedAdResult AdResult;

    public UnityEvent AddRewardEvent;

    public bool IsMobile = false; // For checking mobile device

    [Header("Debug Reward")]
    public TextMeshProUGUI DebugText;
    public int DebugCoins;

    #region AWAKE/START
    private void Awake() {
        if (MyYandex.Instance != null) {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start() {
        //MobileTurnOn();
        if (Application.isEditor) {
            return;
        }
        CheckMobileExtern();
    }
    #endregion

    #region ADS INTERN
    public void ShowNormalAD_Intern() {
        Debug.Log("Show YANDEX Normal AD Intern");

        if (Application.isEditor)
            return;

        YaPauseMusic();
        ShowNormalAdExtern();
    }

    public void ShowReward_Intern() {
        Debug.Log("Show YANDEX Reward AD Intern");
        
        if (Application.isEditor) {
            //AdResult(true);
            AddRewardEvent.Invoke();
            return;
        }


        YaPauseMusic();
        ShowRewardExtern();
    }

    public void DebugReward(int value) {
        DebugCoins += value;
        DebugText.text = "Coins: " + DebugCoins.ToString();
    }
    #endregion

    #region OTHER
    public void AddReward() { 
        AddRewardEvent.Invoke();
        //AdResult(true);

    }
    public void YaPauseMusic() {
        //AllSoundOff
        AudioListener.pause = true;
    }
    public void YaUnPauseMusic() {
        //AllSoundOn
        AudioListener.pause = false;
    }

    public void MobileTurnOn() {
        IsMobile = true;
        //DeviceInfo.text = "Mobile";
    }

    #endregion
}
