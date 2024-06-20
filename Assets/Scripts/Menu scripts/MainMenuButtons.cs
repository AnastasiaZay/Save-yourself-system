using System;
using System.Net.Http;

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    public GameObject prefab;
    public Transform buttonPosition;
    HttpClient httpClient = new HttpClient();

    public InputField loginInput; //Поле с логином в логине
    public InputField passwordInput;
    public string SceneName;
    public GameObject panelKey;
    public GameObject panelRecords;
    public GameObject panelStudent;
    public GameObject panelGuest;
    public GameObject panelLogin;

    public Text nameText;
    public Text lastNameText;

    public Text errorLoginText;

    
    public void StartWithSeed()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void BackToMain()
    {
        if (Storage.currentUser == null)
        {
            panelGuest.gameObject.SetActive(true);
        }
        else
        {
            panelStudent.gameObject.SetActive(true);
        }
        panelKey.gameObject.SetActive(false);
        panelRecords.gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SignOut()
    {
        Storage.currentUser = null;
        panelGuest.gameObject.SetActive(true);
        panelStudent.gameObject.SetActive(false);
    }
    public void SingInConect()
    {
        AsyncConnectSingIn();
    }
    private async Task AsyncConnectSingIn()
    {
        MiniUser miniUser = new MiniUser();
        miniUser.miniLogin = loginInput.text;
        miniUser.miniPassword = passwordInput.text;
        string miniUserJson = JsonUtility.ToJson(miniUser);
        Debug.Log(miniUser.miniLogin);
        Debug.Log(miniUserJson);
        var content = new StringContent(miniUserJson);
        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(Storage.HTTPreference + "unityConnectSingIn", content);
            string userJson = await response.Content.ReadAsStringAsync();
            if (userJson.Equals("Нет"))
            {
                errorLoginText.gameObject.SetActive(true);
                Debug.Log(userJson);
                return;
            }
            if (userJson.Equals("Неверный логин или пароль"))
            {
                errorLoginText.gameObject.SetActive(true);

                Debug.Log(userJson);
                return;
            }
            User tempUser = JsonUtility.FromJson<User>(userJson);
            Storage.currentUser = tempUser;
            Debug.Log(userJson);
            Debug.Log(tempUser.getName());
            errorLoginText.gameObject.SetActive(false);
            panelLogin.gameObject.SetActive(false);
            panelStudent.gameObject.SetActive(true);

            Debug.Log(Storage.currentUser.getName());
            nameText.text = Storage.currentUser.getName();
            lastNameText.text = Storage.currentUser.getLastName();
        }
        catch (Exception)
        {
            Debug.Log("Подключение не удалось");
        }
        /*using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, reference + "unityConnectSingIn"); //В ковычках та строчка подключения из браузера
        try
        {
            // получаем ответ
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            // просматриваем данные ответа
            // статус
            Debug.Log($"Status: {response.StatusCode}\n");
            //заголовки
            Debug.Log("Headers");
            foreach (var header in response.Headers)
            {
                Debug.Log($"{header.Key}:");
                foreach (var headerValue in header.Value)
                {
                    Debug.Log(headerValue);
                }
            }
            // содержимое ответа
            Debug.Log("\nContent");
            string content = await response.Content.ReadAsStringAsync();
            Debug.Log(content);
        }
        catch
        {
            Debug.Log("Подключение не удалось");
        }*/
    }

    public void RecordsConect()
    {
        AsyncConnectRecords();
    }
    private async Task AsyncConnectRecords()
    {
        HttpResponseMessage response = await httpClient.GetAsync(Storage.HTTPreference + "unityConnectRecords");
        string recordJson = await response.Content.ReadAsStringAsync();

    }


    [Serializable]
    public class MiniUser
    {
        public string miniLogin;
        public string miniPassword;
    }
}
