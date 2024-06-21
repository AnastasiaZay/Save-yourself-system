using System;
using System.Collections.Generic;
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
        errorLoginText.gameObject.SetActive(false);
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
        errorLoginText.gameObject.SetActive(false);
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
        RecordsWithNames recordsList = JsonUtility.FromJson<RecordsWithNames>(recordJson);
        Debug.Log(recordJson);

        for (int i = 0; i < recordsList.names.Count; i++)
        {

            Debug.Log(name);

        }

    }

    public void MakeRecordConnect()
    {

        AsyncMakeRecordConnect();
    }

    private async Task AsyncMakeRecordConnect()
    {
        int isRecord;
        if (Storage.isRecord) { isRecord = 1; }
        else { isRecord = 2; }
        //string x = DateTime.Now;
        string newRecordTime = DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + " 00:00:00";
        Debug.Log(newRecordTime);
        Debug.Log(DateTime.Now);
        Record newRecord = new Record(0, Storage.scoreRecord, Storage.currentUser.id, 5, "00:00:00",
           newRecordTime, Storage.isExit, Storage.number_of_solved_quests_records, Storage.playerHP, Storage.currentKey, isRecord);
        string newRecordJson = JsonUtility.ToJson(newRecord);
        var content = new StringContent(newRecordJson);
        Debug.Log(newRecordJson);
        HttpResponseMessage response = await httpClient.PostAsync(Storage.HTTPreference + "unityNewRecord", content);
        Debug.Log(await response.Content.ReadAsStringAsync());
    }
    [Serializable]
    public class MiniUser
    {
        public string miniLogin;
        public string miniPassword;
    }
}
