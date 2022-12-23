using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

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

    //public MenuManager _MenuManager;

    //public GameObject KeyboardButton;

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
            AdResult(true);
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
    public void AddReward() { // Âûçûâàåòñÿ èç ôàéëà jslib êîãäà âèäåîðåêëàìà ïðîñìîòðåíà

        //DebugReward(DebugCoins);
        AdResult(true);

    }
    public void YaPauseMusic() {
        //SoundManager.Instance.PauseMusic(true);
    }
    public void YaUnPauseMusic() {
        //SoundManager.Instance.PauseMusic(false);
    }

    public void MobileTurnOn() {
        IsMobile = true;
        //DeviceInfo.text = "Mobile";
    }

    #endregion
}
