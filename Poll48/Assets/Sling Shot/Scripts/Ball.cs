using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;


    private void Start() {
        _rb = GetComponent<Rigidbody>();
        StartHook();    
    }

    protected virtual void StartHook() {
    }

    private void Update() {
        UpdateHook();
    }

    protected virtual void UpdateHook() {
    }
    
	public void Push(Vector3 force) {
		_rb.isKinematic = false;
		_rb.AddForce(force, ForceMode.Impulse);
	}
}
