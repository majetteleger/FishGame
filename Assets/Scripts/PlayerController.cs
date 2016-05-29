using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerSpeed;

	private Vector3[] controlPath;
	private float pathPosition = 0;
	private iTweenPath path;

	void Awake()
	{
		path = Camera.main.GetComponent<iTweenPath>();
	}

	void Update()
	{

		if(controlPath == null)
		{
			controlPath = iTweenPath.GetPath("New Path 1");
			transform.position = path.nodes[0];
		}

		float moveHorizontal = Input.GetAxis("Horizontal");

		pathPosition = Mathf.Clamp(pathPosition + (moveHorizontal * (playerSpeed / 50) * Time.deltaTime), 0, 1);
		Vector3 coordinateOnPath = iTween.PointOnPath(controlPath, pathPosition);
		iTween.MoveUpdate(gameObject, iTween.Hash("position", coordinateOnPath, "time", Time.deltaTime));

		//	private void FaceDirection(Vector2 direction)
		//{
		//	mFacingDirection = direction;
		//	if (direction == Vector2.right)
		//	{
		//		Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		//		transform.localScale = newScale;
		//	}
		//	else
		//	{
		//		Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		//		transform.localScale = newScale;
		//	}
	}

}

	
