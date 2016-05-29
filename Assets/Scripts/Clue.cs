using UnityEngine;
using System.Collections;

public class Clue : MonoBehaviour {
	
	// public stuff
	public bool state;
	public string name;
	public string description;
	public enum Category
	{
		firstLvl,
		secondLvl,
		thirdLvl
	}
	public Category category;
	//

	// private stuff
	public int position;
	private enum Status
	{
		newC,
		normalC,
		usedC,
	}
	private Status status;
	//

	// static stuff
	public static int cluesFound = 0;
	public static int getcluesFound(){return cluesFound;}
	//

	// accessors
	public bool getState(){return state;}
	public string getName(){return name;}
	public int getPosition(){return position;}
	//

	// mutators
	public void setState(bool s){state = s;}
	public void setPosition(int p){position = p;}
	//
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTrue(){
		
	}

}
