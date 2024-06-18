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
    string reference = "http://localhost:8080/";
    public InputField loginInput;
    public InputField passwordInput;
    public string SceneName;
    

    public void StartRandom()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void StartWithSeed()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SignOut()
    {
        Storage.currentUser = null;
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
            HttpResponseMessage response = await httpClient.PostAsync(reference + "unityConnectSingIn", content);
            string userJson = await response.Content.ReadAsStringAsync();
            if (userJson.Equals("���"))
            {
                Debug.Log(userJson);
                return;
            }
            if (userJson.Equals("�������� ����� ��� ������"))
            {
                Debug.Log(userJson);
                return;
            }
            Storage.currentUser = JsonUtility.FromJson<User>(userJson);
            Debug.Log(userJson);
        }
        catch (Exception)
        {
            Debug.Log("����������� �� �������");
        }
        /*using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, reference + "unityConnectSingIn"); //� �������� �� ������� ����������� �� ��������
        try
        {
            // �������� �����
            using HttpResponseMessage response = await httpClient.SendAsync(request);
            // ������������� ������ ������
            // ������
            Debug.Log($"Status: {response.StatusCode}\n");
            //���������
            Debug.Log("Headers");
            foreach (var header in response.Headers)
            {
                Debug.Log($"{header.Key}:");
                foreach (var headerValue in header.Value)
                {
                    Debug.Log(headerValue);
                }
            }
            // ���������� ������
            Debug.Log("\nContent");
            string content = await response.Content.ReadAsStringAsync();
            Debug.Log(content);
        }
        catch
        {
            Debug.Log("����������� �� �������");
        }*/
    }
    ///����� ������
    //������� � �������� ����
    /*-------------------------------------------------------------------------*/

    ///������ �������� ����
    //������ ������������

    //������ ��������

    //����� � �������

    //�����������

    //�������

    //����� �� ����

    /*-------------------------------------------------------------------------*/

    ///������ �����������
    //������� ��� � �������

    //������� �����

    //������� �����

    //������� ������ � ��������� ������

    //�������� ���

    //������������������

    /*-------------------------------------------------------------------------*/
    ///������ �����
    //������� �����

    //������� ������

    /*-------------------------------------------------------------------------*/
    ///������ ��������
    ///
    [Serializable]
    public class MiniUser
    {
        public string miniLogin;//{ get; set; }
        public string miniPassword; //{ get; set; }
    }
}
