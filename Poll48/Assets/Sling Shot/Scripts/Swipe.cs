using UnityEngine;

public class Swipe : MonoBehaviour 
{
	private Vector2 _startPos, _endPos, _direction;
	private Vector3 _forcevector;
	private float _distance;
	private Ball _ball;

	[SerializeField] protected Ball ballPrefab;
	[SerializeField] protected Trajectory trajectory;
	[SerializeField] protected float pushForce;


	private void Start() {
		Spawn();
	}

	private void Update () {
		ControlSwipe();
	}

	private void ControlSwipe() {
		if (Input.GetMouseButtonDown(0)) {
			trajectory.Show();

			_startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}

        if (Input.GetMouseButton(0)) {
			_endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			
			_distance = Mathf.Clamp(Vector2.Distance(_startPos, _endPos), 0.065f, 0.075f); // Vector2.Distance(_startPos, _endPos);
			_direction = (_startPos - _endPos).normalized;
			_forcevector = _direction * _distance * pushForce;
			_forcevector.z = _forcevector.y * 0.75f;
			//_forcevector = Vector3.ClampMagnitude(_forcevector, 12);

			trajectory.UpdateDots(transform.position, _forcevector);
		}

		if (Input.GetMouseButtonUp(0)) {
			_ball?.Push(_forcevector);
			_ball = null;

			trajectory.Hide();
			Invoke("Spawn", 1);
		}
	}

	public void Spawn() {
		_ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
	}
}
