using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Storage 
{
    //Текущий пользователь
    public static User currentUser;

    //Текущий ключ текстом
    public static string currentKey;

    //Текущие установки ключа (количество заданий, количество ловушек, точка спавна, максимальное ХП, уровень задымления)
    public static int number_of_quests_records, 
        number_of_traps_record, 
        point_of_spawn_record, 
        max_health_record, 
        smoke_level_records;
    public static string time_limit_records; //Лимит по времени

    //ХП игрока
    public static int playerHP;

    //Постоянная часть ссылки на Java часть
    public static string HTTPreference = "http://5.42.98.103:8080/";     //"http://localhost:8080/";

   




}

