using System;


[Serializable]
public class User 
{
    // Start is called before the first frame update

    public int userID;
    public string userName;
    public Int32 userScore;

    public User()
    {
        userID = HighScores.userID;
        userName = HighScores.userName;
        userScore = HighScores.userScore;
    }

    public Int32 CompareTo(object obj)
    {
        return userScore.CompareTo(obj);
    }
}
