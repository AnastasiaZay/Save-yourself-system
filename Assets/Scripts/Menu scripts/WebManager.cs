using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[System.Serializable]
public class User
{
    public int id;
    public string login;
    public string password;
    public string email;
    public int Is_admin_user; //мб поменяю на инт
    public string name;
    public string last_name;
    public int is_woman;
    public int id_best_record_user;
    public string birth_date; //Поменять на дату
    public string picture_user;

    public string getName()
    {
        return name;
    }
    public string getLastName()
    {
        return last_name;
    }

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
    
    
}
[System.Serializable]
public class Record
{
    public int id;
    public int score_records;
    public int id_user_records;
    public int grade_records;
    public string time_records;
    public string date_of_records; ///А надо бы дату
    public bool exit_param_record;
    public int number_of_solved_quests_records;
    public int health_record;
    public string id_key_record;
    public int is_record;
    public string beaubeautifullDate;

    public Record(int id, int score_records, int id_user_records, int grade_records, string time_records,
        string date_of_records, bool exit_param_record, int number_of_solved_quests_records,
        int health_record, string id_key_record, int is_record)
    {
        this.id = id;
        this.score_records = score_records;
        this.id_user_records = id_user_records;
        this.grade_records = grade_records;
        this.time_records = time_records;
        this.date_of_records = date_of_records;
        this.exit_param_record = exit_param_record;
        this.number_of_solved_quests_records = number_of_solved_quests_records;
        this.health_record = health_record;
        this.id_key_record = id_key_record;
        this.is_record = is_record;
    }



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
    
}

public class RecordsWithNames
{
    public List<string> names;
    public List<Record> records;
}