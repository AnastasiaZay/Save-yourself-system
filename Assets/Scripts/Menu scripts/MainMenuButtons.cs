using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject prefab;
    public Transform buttonPosition;

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


}
