using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

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

	public string[] Actions;
	public Node[] ActionModifiers;

	private Vector3 _originalSize;
	private Color _orginalColor;
	private BoxCollider2D _objectCollider;
	private Vector2 _originalColliderSize, _originalColliderOffset;
	private Transform _target;
	private Vector3 _targetPosition;
	private Vector3 _direction = Vector3.zero;
	private SpriteRenderer _sprite;
	private Button[] _actionButtons;
	private bool _isInteractable;

	[MenuItem("GameObject/FishGame/Object", false, 7)]
	static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject Object = new GameObject("Object");
		GameObjectUtility.SetParentAndAlign(Object, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(Object, "Create " + Object.name);
		Selection.activeObject = Object;
		Object.AddComponent<ObjectBehaviour>();
		Object.AddComponent<SpriteRenderer>();
		Object.AddComponent<BoxCollider2D>();
	}

	void Awake () {
		_originalSize = transform.localScale;
		_sprite = GetComponent<SpriteRenderer>();
		_orginalColor = _sprite ? GetComponent<SpriteRenderer>().color : Color.white;
		_objectCollider = GetComponent<BoxCollider2D>();
		_objectCollider.enabled = FindObjectOfType<PlayerController>() ? false : true;
		_originalColliderSize = _objectCollider.size;
		_originalColliderOffset = _objectCollider.offset;
	}

	void Start()
	{
		if(Actions != null)
		{
			_actionButtons = new Button[Actions.Length];

			for (int i = 0; i < Actions.Length; i++)
			{
				Button newActionButton = Instantiate(InGamePanel.instance.ObjectActionButton);
				newActionButton.transform.SetParent(UIManager.instance.InGamePanel.transform, false);
				newActionButton.transform.position = Camera.main.WorldToScreenPoint(transform.position);
				newActionButton.GetComponentInChildren<Text>().text = Actions[i];

				Node firstNode = ActionModifiers[i];

				newActionButton.onClick.AddListener
				(
					() => { DoStartDialog(firstNode); }
				);

				_actionButtons[i] = newActionButton;

				newActionButton.gameObject.SetActive(false);
			}
		}
	}
	
	public void DoStartDialog(Node firstNode)
	{
		if(MainManager.instance.ActiveDialog == null)
		{
			dialog.initiateDialog(firstNode);
		}
	}

	
	void Update () {
		if(_target == null && MainManager.instance.PlayerController != null)
		{
			_target = MainManager.instance.PlayerController.GetComponent<Transform>();
		}
		else
		{
			if (_target)
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
					if (_target && _direction.magnitude <= range)
					{
						transform.localScale = new Vector3(bigger, bigger, 0);
						_objectCollider.enabled = true;

						if(!_isInteractable)
						{
							for (int i = 0; i < _actionButtons.Length; i++)
							{
								_actionButtons[i].gameObject.SetActive(true);
							}

							_isInteractable = true;
						}
					}
					else if(_target)
					{
						transform.localScale = _originalSize;
						_objectCollider.enabled = false;

						if (_isInteractable)
						{
							for (int i = 0; i < _actionButtons.Length; i++)
							{
								_actionButtons[i].gameObject.SetActive(false);
							}

							_isInteractable = false;
						}
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

		if(_actionButtons != null)
		{
			for (int i = 0; i < _actionButtons.Length; i++)
			{
				if (_actionButtons[i].gameObject.activeSelf)
				{
					_actionButtons[i].transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, -40 - (25 * i), 0);
				}
			}
		}
	}

	void OnMouseEnter() {
		if(_sprite.sprite != null && _target == null)
		{
			_sprite.color = Color.red;

			if (!_isInteractable && _actionButtons != null)
			{
				for (int i = 0; i < _actionButtons.Length; i++)
				{
					_actionButtons[i].gameObject.SetActive(true);
				}

				_isInteractable = true;

				_objectCollider.offset = new Vector2(0, -(_objectCollider.size.y / 2) * 3);
				_objectCollider.size = new Vector2(_objectCollider.size.x, _objectCollider.size.y * 4);
				
			}
		}
	}

	void OnMouseExit() {
		if (_sprite.sprite != null && _target == null)
		{
			_sprite.color = _orginalColor;

			if (_isInteractable && _actionButtons != null)
			{
				for (int i = 0; i < _actionButtons.Length; i++)
				{
					_actionButtons[i].gameObject.SetActive(false);
				}

				_isInteractable = false;

				_objectCollider.offset = _originalColliderOffset;
				_objectCollider.size = _originalColliderSize;
			}
		}
	}

}
