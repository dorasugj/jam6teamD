using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartEvent() {
		gameManager.StartEvent ();
	}
}
