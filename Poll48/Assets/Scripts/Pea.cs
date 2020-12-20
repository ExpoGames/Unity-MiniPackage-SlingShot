using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pea : Ball
{
    public int PowerOfTwo = 1;
    public bool PowerUp;
    public TextMeshPro NumberText;

    private Vector3 _numTextPos;
    private Quaternion _numTextRot;


    protected override void StartHook() {
        UpdateNumber();
        _numTextPos = NumberText.transform.localPosition;
        _numTextRot = NumberText.transform.rotation;
    }

    protected override void UpdateHook() {
        NumberText.transform.position = transform.position + _numTextPos;
        NumberText.transform.rotation = _numTextRot;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Pea") {

            Pea pea = other.gameObject.GetComponent<Pea>();
            if (PowerOfTwo == pea.PowerOfTwo) {        
                // Worst Code of The World
                if (transform.position.y == other.transform.position.y) {
                    if (transform.position.z == other.transform.position.z) {
                        if (transform.position.x > other.transform.position.x) {
                            PowerUp = true;
                        }
                    }
                    else if (transform.position.z > other.transform.position.z) {
                        PowerUp = true;
                    }
                }
                else if (transform.position.y > other.transform.position.y) {
                    PowerUp = true;
                }
                
                if (PowerUp) {
                    Destroy(other.gameObject);
                    PowerUpMe();
                }
            }
        }
    }

    public void PowerUpMe() {
        PowerUp = false;
        PowerOfTwo += 1;
        UpdateNumber();

        transform.localScale *= 1.25f;
    }

    private void UpdateNumber() {
        NumberText.text = "" + Mathf.Pow(2, PowerOfTwo);
    }
}
