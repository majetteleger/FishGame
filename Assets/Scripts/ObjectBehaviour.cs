using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObjectBehaviour : MonoBehaviour {
	
	public Dialog dialog;
	public string levelToLoad;
	public float bigger;
	public float range;
	public enum ObjectType
	{
		GameplayObject,
		GameplayTrigger,
		NarrativeObject,
		NarrativeTrigger,
	}
	public ObjectType Type;

	private Vector3 _originalSize;
	private BoxCollider2D _objectCollider;
	private Transform _target;
	private Vector3 _targetPosition;
	private Vector3 _direction;
	private SpriteRenderer _sprite;

	void Awake () {
		_originalSize = transform.localScale;
		_objectCollider = GetComponent<BoxCollider2D>();
		_objectCollider.enabled = FindObjectOfType<PlayerController>() ? false : true;
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Start(){
		
	}

	void Update () {
		if(_target == null && MainManager.instance.PlayerController != null)
		{
			_target = MainManager.instance.PlayerController.GetComponent<Transform>();
		}
		else
		{
			if(_target)
			{
				_targetPosition = new Vector3(_target.transform.position.x, transform.position.y, transform.position.z);
				_direction = _targetPosition - transform.position;
			}

			switch(Type)
			{
				case ObjectType.GameplayObject:
					
					break;
				case ObjectType.GameplayTrigger:
					if(_direction.magnitude <= range && !(levelToLoad == null || levelToLoad == ""))
					{
						SceneManager.LoadScene(levelToLoad);
					}
					break;
				case ObjectType.NarrativeObject:
					if (_direction.magnitude <= range)
					{
						transform.localScale = new Vector3(bigger, bigger, 0);
						_objectCollider.enabled = true;
					}
					else
					{
						transform.localScale = _originalSize;
						_objectCollider.enabled = false;
					}
					break;
				case ObjectType.NarrativeTrigger:
					if (_direction.magnitude <= range && MainManager.instance.ActiveDialog == null)
					{
						dialog.initiateDialog();
						this.enabled = false;
					}
					break;
			}
		}
	}

	void OnMouseEnter() {
		if(_sprite)
		{
			_sprite.color = Color.red;
		}
	}

	void OnMouseExit() {
		if (_sprite)
		{
			_sprite.color = Color.white;
		}
	}

}
