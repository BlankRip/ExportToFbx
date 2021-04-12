using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct LeaderBoardData {
    public string username;
    public int score;
    public LeaderBoardData(string username, int score) {
        this.username = username;
        this.score = score;
    }
}

public class SendRecieveDb : MonoBehaviour
{
    public static SendRecieveDb instance;
    LeaderBoardData recievedData;
    
    void Start() {
        if(instance == null)
            instance = this;
        //LeaderBoardData theData = new LeaderBoardData("Chang", 420);

        //StartCoroutine(Upload(theData));
        //StartCoroutine(GetScoreLeaderBoard("Troy"));
    }

    public void Send(LeaderBoardData data) {
        StartCoroutine(Upload(data));
    }

    public void Recieve(string userName) {
        StartCoroutine(GetScoreLeaderBoard(userName));
    }


    IEnumerator GetScoreLeaderBoard(string username) {
        UnityWebRequest www = UnityWebRequest.Get($"http://127.0.0.1:4020/LeaderBoard?username={username}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else {
            recievedData = JsonUtility.FromJson<LeaderBoardData>(www.downloadHandler.text);
            Debug.Log(recievedData.username);
            Debug.Log(recievedData.score);
        }
    }

    IEnumerator Upload(LeaderBoardData data) {
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:4020/LeaderBoard", JsonUtility.ToJson(data));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            Debug.Log("Form upload complete!");
    }
}
