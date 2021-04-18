using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public struct LeaderBoardData {
    
    public string id;
    public string username;
    public int score;
    public LeaderBoardData(string username, int score) {
        this.username = username;
        this.score = score;
        id = "???";
    }
}

[System.Serializable]
public class LBArray {
    public LeaderBoardData[] recieved;
}

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance;
    [SerializeField] bool loadOnStart;
    [SerializeField] int topChartCount;
    [SerializeField] GameObject listItem;
    [SerializeField] Transform topNamesParent;
    [SerializeField] GameObject nameNotFound;
    [SerializeField] Text findName;
    [SerializeField] Text findScore;
    private LBArray recArray;
    private LeaderBoardData recievedData;
    
    private void Awake() {
        if(instance == null)
            instance = this;
    }

    private void Start() {
        if(loadOnStart)
            StartCoroutine(GetAllLeaderBoard());
    }

    private void OnEnable() {
        findName.text = "";
        findScore.text = "";
    }

    public void Send(LeaderBoardData data) {
        StartCoroutine(Upload(data));
    }

    public void Recieve(InputField field) {
        if(!string.IsNullOrEmpty(field.text)) {
            StartCoroutine(GetScoreLeaderBoard(field.text));
            field.text = "";
        }
    }


    IEnumerator GetScoreLeaderBoard(string username) {
        UnityWebRequest www = UnityWebRequest.Get($"http://127.0.0.1:4020/LeaderBoard?username={username}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else {
            try {
                recievedData = JsonUtility.FromJson<LeaderBoardData>(www.downloadHandler.text);
                nameNotFound.SetActive(false);
                findName.text = recievedData.username;
                findScore.text = recievedData.score.ToString();
            } catch {
                nameNotFound.SetActive(true);
                findName.text = "";
                findScore.text = "";
            }
            
        }
    }

    IEnumerator GetAllLeaderBoard() {
        UnityWebRequest www = UnityWebRequest.Get($"http://127.0.0.1:4020/LeaderboardAll");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else {
            //Debug.Log(www.downloadHandler.text);
            recArray = JsonUtility.FromJson<LBArray>(www.downloadHandler.text);
            for (int i = 0; i < topChartCount; i++) {
                if(i < recArray.recieved.Length) {
                    LB_ListItem item = GameObject.Instantiate(listItem, Vector3.zero, Quaternion.identity).GetComponent<LB_ListItem>();
                    item.UpdateData((i+1).ToString(), recArray.recieved[i].username, recArray.recieved[i].score.ToString());
                    item.transform.parent = topNamesParent;
                    //Debug.Log($"{recArray.recieved[i].username} : {recArray.recieved[i].score}");
                } else
                    break;
            }
        }
    }

    IEnumerator Upload(LeaderBoardData data) {
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:4020/LeaderBoard", JsonUtility.ToJson(data));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else {
            Debug.Log("Form upload complete!");
            if(!loadOnStart)
                StartCoroutine(GetAllLeaderBoard());
        }
    }
}
