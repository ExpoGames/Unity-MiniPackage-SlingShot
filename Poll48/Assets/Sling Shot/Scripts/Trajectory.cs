using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] protected int dotsNumber;
	[SerializeField] protected GameObject dotsParent;
	[SerializeField] protected GameObject dotPrefab;
	[SerializeField] protected float dotSpacing;
	[SerializeField] [Range (0.01f, 0.5f)] protected float dotMinScale;
	[SerializeField] [Range (0.50f, 1.0f)] protected float dotMaxScale;

	private Transform[] _dotsList;
	private Vector3 _dotPos;
	private float _timeStamp;


	private void Start() {
		// Hide trajectory in the start
		Hide ();
		// Prepare dots
		PrepareDots ();
	}

	private void PrepareDots() {
		_dotsList = new Transform[dotsNumber];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++) {
			_dotsList[i] = Instantiate(dotPrefab, null).transform;
			_dotsList[i].parent = dotsParent.transform;
			_dotsList[i].gameObject.SetActive(true);

			_dotsList[i].localScale = Vector3.one * scale;
			if (scale > dotMinScale) {
				scale -= scaleFactor;
			}
		}
	}

	public Vector3 UpdateDots(Vector3 ballPos, Vector3 forceApplied) {
		_timeStamp = dotSpacing;
		for (int i = 0; i < dotsNumber; i++) {
			_dotPos.x = (ballPos.x + forceApplied.x * _timeStamp);
			_dotPos.y = (ballPos.y + forceApplied.y * _timeStamp) - (Physics.gravity.magnitude * _timeStamp * _timeStamp) / 2f;
			_dotPos.z = (ballPos.z + forceApplied.z * _timeStamp);
			
			_dotsList[i].position = _dotPos;
			_timeStamp += dotSpacing;
		}

		return _dotsList[1].position;
	}

	public void Show() {
		dotsParent.SetActive (true);
	}

	public void Hide() {
		dotsParent.SetActive (false);
	}
}