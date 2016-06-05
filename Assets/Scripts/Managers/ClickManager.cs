using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickManager : MonoBehaviour {

    public static ClickManager instance = null;

	private bool _canClick = true;

	public bool CanClick
	{
		get { return _canClick; }
		set { _canClick = value; }
	}
	
	private void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Update (){
		
		if (Input.GetMouseButtonDown (0))
		{
			if (MainManager.instance.ActiveDialog == null && _canClick)
			{
				Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

				if (hitInfo)
				{
					//just a test to see what was clicked
					//Debug.Log ("Clicked " + hitInfo.transform.gameObject.name); 

					ObjectBehaviour objectData = hitInfo.transform.gameObject.GetComponent<ObjectBehaviour>();
					
					if (objectData.levelToLoad != null && objectData.levelToLoad != "")
					{
						SceneManager.LoadScene(objectData.levelToLoad);
					}
					
					if (objectData.dialog != null)
					{
						objectData.dialog.initiateDialog ();
					}
				} 
			}
			else if(!_canClick)
			{

			}
			else
			{
				MainManager.instance.ActiveDialog.advanceDialog ();
			}

		}
	}

	/*void startFlowchart(RaycastHit2D hitInfo, string flowchartName){
		if(hitInfo.transform.gameObject.name == flowchartName.Substring(0, flowchartName.Length-9)){
			for(int k = 0; k < flowcharts.Length; ++k){
				if(flowcharts[k].name == flowchartName){
					flowcharts[k].SendFungusMessage("Start");
				}
			}
		}
	}*/



}
