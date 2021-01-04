using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;


    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

	public void Push(Vector3 force) {
		_rb.isKinematic = false;
		_rb.AddForce(_rb.mass * force, ForceMode.Impulse);
	}
}