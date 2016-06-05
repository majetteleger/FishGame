using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float Lboundaries;
	public float Rboundaries;

	private Transform _target;
	private float kFollowSpeed = 3.5f;
	private float stepOverThreshold = 0.5f;
	
	void Update ()
	{
		if (_target == null)
		{
			_target = MainManager.instance.PlayerController.GetComponent<Transform>();
		}
		else
		{
			if (((-Lboundaries) <= (transform.position.x)) && ((transform.position.x) <= (Rboundaries))) {
				Vector3 targetPosition = new Vector3 (_target.transform.position.x, transform.position.y, transform.position.z);
				Vector3 direction = targetPosition - transform.position;

				if(direction.magnitude > stepOverThreshold)
				{
					// If too far, translate at kFollowSpeed
					transform.Translate (direction.normalized * kFollowSpeed * Time.deltaTime);
				}
				else
				{
					// If close enough, just step over
					transform.position = targetPosition;
				}
			}

			if (transform.position.x < -Lboundaries){
				Vector3 temp = new Vector3(-Lboundaries,transform.position.y,-20);
				transform.position = temp;
			}

			if (transform.position.x > Rboundaries){
				Vector3 temp = new Vector3(Rboundaries,transform.position.y,-20);
				transform.position = temp;
			}

		}
	}
}
