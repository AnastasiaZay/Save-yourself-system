using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[System.Serializable]
public class User
{
    private int id;
    private string login;
    private string password;
    private string email;
    private int Is_admin_user; //мб поменяю на инт
    private string name;
    private string last_name;
    private int is_woman;
    private int id_best_record_user;
    private string birth_date; //Поменять на дату
    private string picture_user;


    public User(int id, string login, string password, string email, int is_admin_user, string name,
        string last_name, int is_woman, int id_best_record_user, string birth_date, string picture_user)
    {
        this.id = id;
        this.login = login;
        this.password = password;
        this.email = email;
        Is_admin_user = is_admin_user;
        this.name = name;
        this.last_name = last_name;
        this.is_woman = is_woman;
        this.id_best_record_user = id_best_record_user;
        this.birth_date = birth_date;
        this.picture_user = picture_user;
    }
    public void SetLogin(string login) => this.login = login;
    //public void SetSecondColor(string color) => colorSecond = color;
    //public void SetNickname(string name) => nickname = name;
}

[System.Serializable]
public class UserData
{
    public User playerData;
    public ErrorDB error;
}
[System.Serializable]
public class ErrorDB
{
    public string errorText;
    public bool isDBError;
}
public class WebManager : MonoBehaviour
{

    public static UserData userData = new UserData();
    [SerializeField] private string targetURL;

    [SerializeField] private UnityEvent OnLogged, OnRegistered, OnError;

    public enum RequestType
    {
        logging, register, save
    }


    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        print(data);
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new ErrorDB() { errorText = "text", isDBError = true };
        //userData.playerData = new User("Yagir", "", "");
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password))
        {
            Logging(login, password);
        }
        else
        {
            userData.error.errorText = "To small length";
            OnError.Invoke();
        }
    }
    public void Registration(string login, string password, string password2, string nickname)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password) && CheckString(password2) && CheckString(nickname) && password == password2)
        {
            Registering(login, password, password2, nickname);
        }
        else
        {
            userData.error.errorText = "To small length";
            OnError.Invoke();
        }
    }

    public bool CheckString(string toCheck)
    {
        toCheck = toCheck.Trim();
        if (toCheck.Length > 4 && toCheck.Length < 16)
        {
            return true;
        }
        return false;
    }

    public void SaveData(int id, string nickname, string main, string second)
    {
        StopAllCoroutines();
        SaveProgress(id, nickname, main, second);
    }

    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.logging.ToString());
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form, RequestType.logging));
    }

    public void Registering(string login, string password1, string password2, string nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.register.ToString());
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.register));
    }
    public void SaveProgress(int id, string nickname, string main, string second)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.save.ToString());
        form.AddField("id", id);
        form.AddField("colorMain", main);
        form.AddField("colorSecond", second);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.save));
    }

    IEnumerator SendData(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var data = SetUserData(www.downloadHandler.text);
                if (!data.error.isDBError)
                {
                    if (type != RequestType.save)
                    {
                        userData = data;
                        if (type == RequestType.logging)
                        {
                            OnLogged.Invoke();
                        }
                        else
                        {
                            OnRegistered.Invoke();
                        }
                    }
                }
                else
                {
                    userData = data;
                    OnError.Invoke();
                }
            }
        }
    }
}

