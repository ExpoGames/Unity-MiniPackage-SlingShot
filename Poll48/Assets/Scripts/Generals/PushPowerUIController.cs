using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class PushPowerUIController : MonoBehaviour
{
    [SerializeField] protected Color fullColor;
    [SerializeField] protected Image progress;


    public void SetProgress(float newProgress) {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Play", newProgress == 1);

        progress.color = fullColor * newProgress + Color.white * (1 - newProgress); 
        progress.fillAmount = newProgress;
    }
}
