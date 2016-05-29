using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	float mMoveSpeed;

	Vector2 mFacingDirection;
	Rigidbody2D mRigidBody2D;




	void Start ()
	{
		mRigidBody2D = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		
		if (Input.GetButton ("Left")) {
			transform.Translate (-Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection (-Vector2.right);
		}
		if (Input.GetButton ("Right")) {
			transform.Translate (Vector2.right * mMoveSpeed * Time.deltaTime);
			FaceDirection (Vector2.right);
		}


	}
		
	private void FaceDirection(Vector2 direction)
	{
		mFacingDirection = direction;
		if(direction == Vector2.right)
		{
			Vector3 newScale = new Vector3(Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
		else
		{
			Vector3 newScale = new Vector3(-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			transform.localScale = newScale;
		}
	}


}
