using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{

	float sceneWeight = 1;

	bool animatingIn;

	bool animatingOut;

	BossAttack boss;

	PostProcessVolume ppFade;

	// Start is called before the first frame update
	void Start()
    {
		boss = FindObjectOfType<BossAttack>();
		ppFade = Camera.main.transform.Find("ppFade").GetComponent<PostProcessVolume>();
		animatingIn = true;
    }

	// Update is called once per frame
	void Update()
	{
		if (animatingIn)
		{
			float difference = sceneWeight - 0;
			sceneWeight = Mathf.Lerp(sceneWeight, 0, Time.deltaTime * 5);
			if (Mathf.Abs(difference) <= 0.05f)
			{
				animatingIn = false;
				boss.StartBeat();
			}
		} else if(animatingOut)
		{
			float difference = sceneWeight - 1;
			sceneWeight = Mathf.Lerp(sceneWeight, 1, Time.deltaTime * 10);
			if (Mathf.Abs(difference) <= 0.001f)
			{
				if(!IsInvoking("Restart"))
					Invoke("Restart", 3);
			}
		}
		ppFade.weight = sceneWeight;
	}

	public void GameOver()
	{
		if (animatingOut)
			return;
		animatingOut = true;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnDestroy() => CancelInvoke();

}
