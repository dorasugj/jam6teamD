using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{

	public GameObject hero;
	public GameObject heroine;
	public GameObject start;
	public GameObject replay;
	public GameObject help;
	public GameObject heroSprite;
	public GameObject heroineSprite;
	public GameObject heartEffect;
	public Sprite heroine1;
	public Sprite heroine2;
	public Sprite heroine3;
	public Sprite heroine4;
	public Sprite hero1;
	public Sprite hero2;
	public Sprite hero3;
	public Sprite hero4;
	public GameObject serifu1;
	public GameObject serifu2;
	public GameObject serifu3;
	public GameObject serifu4;
	public AudioSource bgm;
	public AudioSource successAudio;

	float timeCounter;
	int phase = 0;
	int success = 0;
	bool idle = true;
	bool started = false;

	// Use this for initialization
	void Start ()
	{
		iTween.MoveBy (hero, iTween.Hash (
			"y", 0.3f,
			"easeType", "easeOutSine",
			"loopType", "pingPong",
			"time", 0.5f,
			"delay", 0.0f
		));
	
		iTween.MoveBy (heroine, iTween.Hash (
			"y", 0.3f,
			"easeType", "easeOutSine",
			"loopType", "pingPong",
			"time", 0.5f,
			"delay", 0.0f
		));

		timeCounter = 0;
		phase = 0;
		success = 0;
	}

	public void StartEvent() {
		started = true;
		bgm.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float time = -1;
		if (started) {
			timeCounter += Time.deltaTime;
			time = timeCounter;
		}
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("click at " + time);
			int oldSuccess = success;

			if (replay.activeSelf) {
				// リプレイ
				SceneManager.LoadScene ("Main");
				return;
			}
			float margin = 0.5f;
			if (idle && ((time >= 3 - margin && time <= 3 + margin) || (time >= 6 - margin && time <= 6 + margin) || (time >= 9 - margin && time <= 9 + margin))) {
				idle = false;
				success += 1;
			}

			if (oldSuccess != success) {
				if (success == 1) {
					// えっ
					serifu1.SetActive (true);
					heroSprite.GetComponent<SpriteRenderer> ().sprite = hero3;
					heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine2;
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", 1.0f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.0f
					));
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", -1.0f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.5f,
						"oncomplete", "Success1Complete",
						"oncompletetarget", gameObject
					));
				} else if (success == 2) {
					// ドキッ
					serifu2.SetActive (true);
					heroSprite.GetComponent<SpriteRenderer> ().sprite = hero3;
					heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine2;
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", 1.0f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.0f
					));
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", -1.0f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.5f,
						"oncomplete", "Success2Complete",
						"oncompletetarget", gameObject
					));
				} else if (success == 3) {
					// クリア
					heroSprite.GetComponent<SpriteRenderer> ().sprite = hero4;
					heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine4;
					heartEffect.SetActive (true);
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", -0.5f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.0f
					));
					Invoke("ShowReplay", 3);
					successAudio.Play ();
				}
			} else {
				if (idle && time >= 0) {
					// ふざけんな
					idle = false;
					serifu3.SetActive (true);
					heroSprite.GetComponent<SpriteRenderer> ().sprite = hero4;
					heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine1;
					iTween.MoveBy (heroineSprite, iTween.Hash (
						"x", 3.0f,
						"easeType", "easeOutSine",
						"time", 0.5f,
						"delay", 0.0f
					));
					Invoke("ShowReplay", 1);
				}
			}

		}
		if (time >= 0 && phase == 0) {
			// ゲームスタート
			phase = 1;
			help.SetActive (false);
			start.SetActive (true);
			iTween.FadeTo(start, iTween.Hash(
				"a", 0.0f,
				"easeType", "easeOutSine",
				"time", 0.5f,
				"delay", 1.0f
			));
		}
		if (idle && time >= 11 && phase == 1) {
			// いくじなし
			phase = 2;
			idle = false;
			serifu4.SetActive (true);
			heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine1;
			iTween.MoveBy (heroineSprite, iTween.Hash (
				"x", 3.0f,
				"easeType", "easeOutSine",
				"time", 0.5f,
				"delay", 0.0f
			));
			Invoke("ShowReplay", 1);
		}
	}
	void ShowReplay() {
		replay.SetActive (true);
	}

	void Success1Complete() {
		idle = true;
		serifu1.SetActive (false);
		heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine3;
		heroSprite.GetComponent<SpriteRenderer> ().sprite = hero1;
	}
	void Success2Complete() {
		idle = true;
		serifu2.SetActive (false);
		heroineSprite.GetComponent<SpriteRenderer> ().sprite = heroine3;
		heroSprite.GetComponent<SpriteRenderer> ().sprite = hero1;
	}

}
