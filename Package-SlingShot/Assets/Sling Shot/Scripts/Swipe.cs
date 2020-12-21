using UnityEngine;

public class Swipe : MonoBehaviour 
{
	[SerializeField] protected Ball ballPrefab;			// Ball prefab to spawn ball to throw
	[SerializeField] protected Trajectory trajectory;	// Trajectory will be drawn
	[SerializeField] protected float pushForce;			// Push vector multiplier
	[SerializeField] protected float maxForce;			// You can limit the force with this
	[SerializeField] [Range(0f, 10f)] protected float zMultiplier;	// How much the 'y' of force vector impacts the 'z' of force vector.
																	// If you want to throw only on 2D Space, set this as 0 (zero).
	
	private Vector2 _startPos, _endPos;	// Screen points to calculate Force vector
	private Vector3 _forcevector;		// Force vector to apply ball
	private Ball _ball;					// Ball to throw


	private void Start() {
		Spawn();
	}

	private void Update() {
		ControlSwipe();
	}

	private void ControlSwipe() {
		// Starting to drag
		if (Input.GetMouseButtonDown(0)) {
			// Starting to showing trajectory
			trajectory.Show();
		
			// Taking first point on screen for force vector
			_startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}

		// Dragging
        if (Input.GetMouseButton(0)) {
			// Taking secnd point on screen for force vector
			_endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			
			// Direction and distance
			Vector3 direction = (_startPos - _endPos).normalized;
			float distance = Vector2.Distance(_startPos, _endPos);
			// Determining Force Vector
			_forcevector = direction * distance * pushForce;
			_forcevector.z = _forcevector.y * zMultiplier;
			_forcevector = Vector3.ClampMagnitude(_forcevector, maxForce);

			// Updating positions of dots acc to Force vector starting with ball position
			trajectory.UpdateDots(transform.position, _forcevector);
		}

		// End the dragging and push the ball
		if (Input.GetMouseButtonUp(0)) {
			if (_ball) {
				// Appling Force vector to the ball
				_ball.Push(_forcevector);
				_ball = null;

				// Spawning a new ball to throw
				Invoke("Spawn", 1);
			}

			// Hiding the trajectory
			trajectory.Hide();
		}
	}

	// Spawns a new ball
	public void Spawn() {
		_ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
	}
}
