using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float speed;
	public float xBoundaries;
	public float yBoundaries;
	public float xMargin;
	public float yMargin;
	public GameObject background;

	private float screenWidth = (float) Screen.width;
	private float screenHeight = (float) Screen.height;
	private float sceneHeight;
	private float sceneWidth;


	void Start () {

		transform.position = new Vector3(0,0,-10);
		sceneHeight = background.GetComponent<SpriteRenderer>().bounds.size.y;
		sceneWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
		/*Debug.Log (sceneHeight);
		Debug.Log (sceneWidth);
		Debug.Log (screenHeight);
		Debug.Log (screenWidth);

		Debug.Log (screenHeight/185);*/


	}

	void Update() {

		/*if (((-boundaries) <= (transform.position.x)) && ((transform.position.x) <= (boundaries))) {
			var move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;
		}

		if (transform.position.x < -xBoundaries){
			Vector3 temp = new Vector3(-xBoundaries,transform.position.y,-10);
			transform.position = temp;
		}

		if (transform.position.x > xBoundaries){
			Vector3 temp = new Vector3(xBoundaries,transform.position.y,-10);
			transform.position = temp;
		}*/



		if (transform.position.x + screenWidth/170 < sceneWidth/2) {
			if (Input.mousePosition.x > screenWidth - xMargin) {
				var move = new Vector3 (1, 0, 0);
				transform.position += move * speed * Time.deltaTime;
			}
		}

		if (-sceneWidth/2 < transform.position.x - screenWidth/170) {
			if (Input.mousePosition.x < 0 + xMargin) {
				var move = new Vector3 (-1, 0, 0);
				transform.position += move * speed * Time.deltaTime;
			}
		}

		if (transform.position.y + screenHeight/170 < sceneHeight/2) {
			if (Input.mousePosition.y > screenHeight - yMargin) {
				var move = new Vector3 (0, 1, 0);
				transform.position += move * speed * Time.deltaTime;
			}
		}

		if (-sceneHeight/2 < transform.position.y - screenHeight/170) {
			if (Input.mousePosition.y < 0 + yMargin) {
				var move = new Vector3 (0, -1, 0);
				transform.position += move * speed * Time.deltaTime;
			}
		}

	}



}