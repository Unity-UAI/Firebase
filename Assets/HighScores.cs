using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using System.Linq;

public class HighScores : MonoBehaviour
{
    private static readonly string databaseURL = "https://testfirebase-dd200-default-rtdb.firebaseio.com/";
    
    public User[] userList;
    public GameObject scorePanel;
    public GameObject linePrefab;
    public GameObject[] lines;
    public GameObject currentUserLine;

    public Transform newLinePosition;

    public int lineOffset;

    public static int userID;
    public static int userScore;
    public static string userName;


    // Use this for initialization
    void Start()
    {
        CreateAllLines();
    }

    void CreateAllLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            CreateNewLine(i);
            newLinePosition.Translate(0f, -lineOffset, 0);
        }
    }

    public void CreateNewLine(int i)
    {
        GameObject newLine = Instantiate(linePrefab, newLinePosition.position, Quaternion.identity);
        newLine.transform.SetParent(scorePanel.transform);
        newLine.name = "User_" + i.ToString();
        lines[i] = newLine;
    }

    private void WriteAllLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            WriteUserLine(lines[i], userList[i]);
        }
    }

    private void WriteUserLine(GameObject userLine, User user)
    {
        userLine.transform.GetChild(0).GetComponent<Text>().text = user.userID.ToString();
        userLine.transform.GetChild(1).GetComponent<Text>().text = user.userScore.ToString(); 
        userLine.transform.GetChild(2).GetComponent<Text>().text = user.userName;
    }

   
    public void OnGetScore()
    {
        RetrieveFromDataBase();
    }

    private void RetrieveFromDataBase()
    {
        RestClient.GetArray<User>(databaseURL + ".json").Then(response =>
        {
            userList = response;
            WriteAllLines();

        });
    }

    
    public void OrderArray()
    
    {
        userList = userList.OrderByDescending(User => User.userScore).ToArray();
        WriteAllLines();
    }

    public void CreateDatabase()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            User user = new User
            {
                userID = i,
                userName = "User " + i.ToString(),
                userScore = Random.Range(0, 100)
            };

            PostToDataBase(user);
        }
    }
    private void PostToDataBase(User user)
    {
        RestClient.Put(databaseURL + user.userID.ToString() + ".json", user);
    }
}