using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    [SerializeField] protected GameObject StartScreen;
    [SerializeField] protected GameObject TopBar;           // TODO: Assign ingame top-bar
    [SerializeField] protected GameObject FailScreen;       // TODO: Assign Fail UI
    [SerializeField] protected GameObject WinScreen;        // TODO: Assign Win UI
    [SerializeField] protected TextMeshProUGUI LevelText;   // TODO: Assign ingame level panel

    [SerializeField] protected PushPowerUIController PushPowerScreen;


    public void StartScreenActive(bool active) {
        StartScreen.SetActive(active);
    }

    public void SetLevelText(int level) {
        LevelText.text = "Level " + level;
    }

    public void TopBarActive(bool active) {
        TopBar.SetActive(active);
    }

    public void FailScreenActive(bool active) {
        FailScreen.SetActive(active);
    }

    public void WinScreenActive(bool active) {
        WinScreen.SetActive(active);
    }

    // ******************************** // 

    public void SetPushPower(float progress) {
        PushPowerScreen.SetProgress(progress);    
    }
}