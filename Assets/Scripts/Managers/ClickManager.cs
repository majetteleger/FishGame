using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

    public static ClickManager instance = null;

    private Dialog _activeDialog;

    public Dialog ActiveDialog
    {
        get { return _activeDialog; }
        set { _activeDialog = value; }
    }

    private bool _waitingForOption;

    public bool WaitingForOption
    {
        get { return _waitingForOption; }
        set { _waitingForOption = value; }
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
		
		if (Input.GetMouseButtonDown (0)) {

			if (_activeDialog == null && !_waitingForOption) {

				Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

				if (hitInfo) {
					
					//just a test to see what was clicked
					//Debug.Log ("Clicked " + hitInfo.transform.gameObject.name); 

					// click commands begin


					if (hitInfo.transform.gameObject.name == "Cabin") {
						Application.LoadLevel ("Indoors");
					}

					if (hitInfo.transform.gameObject.name == "Door") {
						Application.LoadLevel ("Outdoors");
					}

					// Start executing a dialog if clicked
					if (hitInfo.transform.gameObject.GetComponent<ObjectBehaviour>().dialog != null) {
						hitInfo.transform.gameObject.GetComponent<ObjectBehaviour> ().dialog.initiateDialog ();
						_activeDialog = hitInfo.transform.gameObject.GetComponent<ObjectBehaviour> ().dialog;
					}

					// click commands end
						
				} 

			} else if(_waitingForOption){
				

			} else {
                _activeDialog.advanceDialog ();
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
