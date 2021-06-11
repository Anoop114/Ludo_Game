using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MainMenuUIManager_online : MonoBehaviourPunCallbacks {

	public InputField Create_Loby;
	public InputField join_loby;
	[SerializeField] private GameObject gamePlayPreference;
	// [SerializeField] private GameObject loadingSene;
	[SerializeField] private QuestionDialog quitDialog;

	[SerializeField] private TokensRadioGroup tokensRadioGroup;
	[SerializeField] private PlayerCountRadioGroup playerCountRadioGroup;

	private int playerCount = 2;
	private Token.TokenType selectedToken = Token.TokenType.Blue;

	void Start ()
	{
        PhotonNetwork.ConnectUsingSettings();
		tokensRadioGroup.onTokenTypeSelected += ((Token.TokenType type) => selectedToken = type);
		playerCountRadioGroup.onPlayerCountSelected += ((int count) => playerCount = count);
	}

	public void create_room(){
		PhotonNetwork.CreateRoom(Create_Loby.text);
	}
	public void join_room(){
		PhotonNetwork.JoinRoom(join_loby.text);
		OnPlay();

	}
	public override void OnJoinedRoom(){
		// PhotonNetwork.LoadLevel("GamePlay");
		OnVSOnline();
	}


	public override void OnConnectedToMaster(){
		PhotonNetwork.JoinLobby();
		Debug.Log("connected to master server");
	}
	public override void OnJoinedLobby(){
		// StartCoroutine(loading_sean());
		// loadingSene.SetActive (false);

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gamePlayPreference.activeSelf) {
				gamePlayPreference.SetActive (false);
			} else {
				quitDialog.ShowDialog ("Are you sure want to quit?", () => Application.Quit (), null);
			}
		}
	}

	public void OnVSOnline()
	{
		gamePlayPreference.SetActive (true);
		if(gamePlayPreference){
			StartCoroutine(Start_game());
		}
	}
	IEnumerator Start_game(){
		yield return new WaitForSeconds(7);
		OnPlay();
	}
	// IEnumerator loading_sean(){
	// 	yield return new WaitForSeconds(1);
	// 	loadingSene.SetActive (true);
	// 	yield return new WaitForSeconds(1);

	// }
	public void OnPlay ()
	{
		Token.TokenPlayer[] players = new Token.TokenPlayer[playerCount];
		Token.TokenType[] types = new Token.TokenType[playerCount];

		for (int i = 0; i < playerCount; i++) {
			// players [i] = Token.TokenPlayer.Computer;
			players [i] = Token.TokenPlayer.Human;
			types [i] = (Token.TokenType)i;

			if (types [i] == selectedToken) {
				players [i] = Token.TokenPlayer.Human;
			}
		}

		if ((int)selectedToken >= playerCount) {
			players [playerCount - 1] = Token.TokenPlayer.Human;
			types [playerCount - 1] = selectedToken;
		}

		GameMaster gm = GameMaster.instance;
		gm.SelectedTokens = types;
		gm.SelectedTokenPlayers = players;

		// SceneManager.LoadScene ("GamePlay");
		PhotonNetwork.LoadLevel("GamePlay");

	}

}
