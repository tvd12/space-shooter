using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using com.tvd12.ezyfoxserver.client.logger;
using com.tvd12.ezyfoxserver.client.entity;

public class Done_GameController : MonoBehaviour
{
	public GameObject ship;
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public InputField usernameInput;
	public Button loginButton;

	private bool gameOver;
	private bool restart;
	private int score;

	public Done_GameController()
    {
		EzyLoggerFactory.setLoggerSupply(type => new UnityLogger(type));
		var socketClientProxy = SocketClientProxy.getInstance();
		socketClientProxy.onReconnected(data => {
			var gameId = data.get<int>("gameId");
			var gameState = data.get<string>("gameState");
			GameManager.getInstance().gameId = gameId;
			var gameObjectDatas = data.get<EzyArray>("gameObjects");
			for (var i = 0; i < gameObjectDatas.size(); ++i)
			{
				var gameObjectData = gameObjectDatas.get<EzyObject>(i);
				var gameObjectType = gameObjectData.get<int>("type");
				var position = gameObjectData.get<EzyObject>("position");
				var x = position.get<float>("x");
				var y = position.get<float>("y");
				var z = position.get<float>("z");
				var spawnPosition = new Vector3(x, y, z);
				var visible = gameObjectData.get<bool>("visible");
				gameObject.SetActive(visible);
				var objectName = gameObjectData.get<string>("name");
				if (objectName.Equals("hazard"))
				{
					GameObject hazard = hazards[gameObjectType];
					var gameObject = Instantiate(hazard, spawnPosition, Quaternion.identity);
					GameManager.getInstance().addGameObject(gameObjectType, gameObject);
				}
				else if (objectName.Equals("ship"))
				{
					ship.transform.position = spawnPosition;
				}
			}
			score = data.get<int>("playerScore");
			UpdateScore();
			if (gameId > 0 && gameState.Equals("PLAYING"))
			{
				StartCoroutine(SpawnWaves());
			}
			else
			{
				GameManager.getInstance().getGameId();
			}
			usernameInput.gameObject.SetActive(false);
			loginButton.gameObject.SetActive(false);
		});
		socketClientProxy.onGameIdReceived(data => {
			var gameId = data.get<int>("gameId");
			GameManager.getInstance().gameId = gameId;
			SocketClientProxy.getInstance().startGame(gameId);
		});
		socketClientProxy.onStartGame(data => {
			StartCoroutine(SpawnWaves());
		});
		socketClientProxy.onDisconnected(() => {
			// do something
		});
	}

	void Start()
	{
		ship.name = "ship";
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		usernameInput.gameObject.SetActive(false);
		loginButton.gameObject.SetActive(false);
		score = 0;
		UpdateScore();
		GameManager.getInstance().clear();
		GameManager.getInstance().addGameObject(0, ship);
		var socketClientProxy = SocketClientProxy.getInstance();
		if (socketClientProxy.firstLogin)
        {
			usernameInput.gameObject.SetActive(true);
			loginButton.gameObject.SetActive(true);
		}
	}

	void Update()
	{
		var socketClientProxy = SocketClientProxy.getInstance();

		if (!socketClientProxy.isConnected())
		{
			return;
		}
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				SocketClientProxy.getInstance().getGameId();
			}
		}
		if (!gameOver)
		{
			GameManager.getInstance().syncGameObjectPositions();
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			var socketClientProxy = SocketClientProxy.getInstance();
			for (int i = 0; i < hazardCount && socketClientProxy.isConnected(); i++)
			{
				var gameObjectType = Random.Range(0, hazards.Length);
				GameObject hazard = hazards[gameObjectType];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				var gameObject = Instantiate(hazard, spawnPosition, spawnRotation);
				gameObject.name = "hazard";
				GameManager.getInstance().addGameObject(gameObjectType, gameObject);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		GameManager.getInstance().syncScore(score);
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		GameManager.getInstance().finishGame();
	}
}