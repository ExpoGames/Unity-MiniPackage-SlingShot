using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] protected Transform dotsParent;	// Parent of all dots
	[SerializeField] protected Transform dotPrefab;		// Dot prefab to clone
	[SerializeField] protected Transform target;		// Target of object
	[SerializeField] protected int dotsNumber;			// Amount of dots
	[SerializeField] protected float dotSpacing;		
	[SerializeField] [Range(0.01f, 0.5f)] protected float dotMinScale;
	[SerializeField] [Range(0.50f, 1.0f)] protected float dotMaxScale;

	private Transform[] _dotsList;		// All dots


	private void Start() {
		// Hiding trajectory on start
		Hide();
		// Initializing dots
		PrepareDots();
	}

	// Initializes dots on start
	private void PrepareDots() {
		_dotsList = new Transform[dotsNumber];
		dotPrefab.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++) {
			_dotsList[i] = Instantiate(dotPrefab, null);
			_dotsList[i].parent = dotsParent.transform;
			_dotsList[i].gameObject.SetActive(true);

			_dotsList[i].localScale = Vector3.one * scale;
			if (scale > dotMinScale) {
				scale -= scaleFactor;
			}
		}
	}

	// Updates positions of dots acc to Force vector starting with ball position
	public void UpdateDots(Vector3 ballPos, Vector3 forceApplied) {
		float timeStamp = dotSpacing;
		bool hideDot = false;
		foreach (Transform dot in _dotsList) {
			Vector3 dotPos;
			// Calculating positions of dots acc to basic physic formulas
			dotPos.x = (ballPos.x + forceApplied.x * timeStamp);
			dotPos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp) / 2f;
			dotPos.z = (ballPos.z + forceApplied.z * timeStamp);
			
			// Assigning new pos
			dot.position = dotPos;
			dot.LookAt(Camera.main.transform);
			// If a dot collide with ground hide remaining dots
			if (!hideDot && CheckGround(dotPos)) {
				hideDot = true;
			}
			dot.gameObject.SetActive(!hideDot);
			
			//
			timeStamp += dotSpacing;
		}

		target.gameObject.SetActive(hideDot);
	}

	// Checks if dot collide with object with "Ground" tag
	private bool CheckGround(Vector3 sphereCenter) {
		Collider[] coll = Physics.OverlapSphere(sphereCenter, 1f);
		if (coll.Length > 0 && coll[0].transform.tag == "Ground") {
			// Adjusting position of target
			Vector3 pos = sphereCenter;
			pos.y = coll[0].transform.position.y + coll[0].bounds.size.y / 2 + 0.01f;
			target.position = pos;
			return true;
		}
		return false;
	}

	public void Show() {
		dotsParent.gameObject.SetActive(true);
	}

	public void Hide() {
		dotsParent.gameObject.SetActive(false);
	}
}