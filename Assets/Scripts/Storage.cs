using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Storage 
{
    //������� ������������
    public static User currentUser;

    //������� ���� �������
    public static string currentKey;

    //������� ��������� ����� (���������� �������, ���������� �������, ����� ������, ������������ ��, ������� ����������)
    public static int number_of_quests_records, 
        number_of_traps_record, 
        point_of_spawn_record, 
        max_health_record, 
        smoke_level_records;
    public static string time_limit_records; //����� �� �������

    //�� ������
    public static int playerHP;

    //���������� ����� ������ �� Java �����
    public static string HTTPreference = "http://5.42.98.103:8080/";     //"http://localhost:8080/";

   




}

