using UnityEngine;
using System.Collections;

public class ObjectBehaviour : MonoBehaviour {

	public Transform mTarget;
	public Dialog dialog;
	public float bigger;
	public float range;

	Vector3 originalSize;
	BoxCollider2D collider;

	void Awake () {
		originalSize = transform.localScale;
		collider = GetComponent<BoxCollider2D>();
	}

	void Start(){
		collider.enabled = false;
	}

	void Update () {
		if (mTarget != null) {
			Vector3 targetPosition = new Vector3 (mTarget.transform.position.x, transform.position.y, transform.position.z);
			Vector3 direction = targetPosition - transform.position;

			if (direction.magnitude <= range) {
				transform.localScale = originalSize + new Vector3 (bigger, bigger, 0);
				collider.enabled = true;

			} else {
				transform.localScale = originalSize;
				collider.enabled = false;
			}
		} else {
			collider.enabled = true;
		}
	}

	void OnMouseEnter() {
		GetComponent<SpriteRenderer>().color = Color.red;
	}

	void OnMouseExit() {
		GetComponent<SpriteRenderer>().color = Color.white;
	}

}
