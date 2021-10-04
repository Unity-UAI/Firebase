using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{

    public Text scoreText;
    public InputField nameText;
    private System.Random random = new System.Random();
    string databaseUrl = "https://testfirebase-dd200-default-rtdb.firebaseio.com/";



    User user = new User();

    public static int playerScore;
    public static string playerName;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Puntos: " + playerScore;
        
    }

   
    private void UpdateScore()
    {
        scoreText.text = "Puntos: " + user.userScore;
    }


private void PostToDataBase()
    {
        User user = new User();
        RestClient.Put( databaseUrl + playerName + ".json", user);
    }

    public void OnSubmit() {

        playerName = nameText.text;
        PostToDataBase();
    }

    public void OnGetScore()
    {
        RetrieveFromDataBase();
    }



    private void RetrieveFromDataBase()
    {

        Debug.Log("nameText = " + nameText.text);

        RestClient.Get<User>(databaseUrl + nameText.text + ".json").Then(response =>
        {

            Debug.Log("After Response : User: " + user.userName + "  Score: " + user.userScore);

            user = response;

            Debug.Log("Later:  User: " + user.userName + "  Score: " + user.userScore);

            UpdateScore();

        });
    }

    

}

