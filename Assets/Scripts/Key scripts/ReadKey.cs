using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadKey : MonoBehaviour
{
    public InputField keyField;

    public Text errorText;

    public void CreateKey()
    {

        char x1 = (char)('A' + Random.Range(1, 15));
        char x2 = (char)('B' + Random.Range(1, 15));
        char x3 = (char)('C' + Random.Range(1, 12));
        char x4 = (char)('D' + Random.Range(1, 6));
        char x5 = (char)('E' + Random.Range(1, 3));
        char x6 = (char)('F' + Random.Range(1, 6));
        keyField.text = "" + x1 + x2 + x3 + x4 + x5 + x6;

    }
    public void LoadKey()
    {
        if (keyField.text.Equals("")) { return; }
        char[] strKeyArray = keyField.text.ToCharArray();
        if (!(strKeyArray.Length == 6))
        {
            errorText.gameObject.SetActive(true);
            return;
        }
        for (int i = 0; i < strKeyArray.Length; i++)
        {
            int x;

            switch (i)
            {
                case 0:

                    Storage.number_of_quests_records = strKeyArray[i] - 'A';
                    if (!CheckIsKeyOk(15, Storage.number_of_quests_records))
                    {
                        return;
                    }
                    break;
                case 1:
                    Storage.number_of_traps_record = strKeyArray[i] - 'B';
                    if (!CheckIsKeyOk(15, Storage.number_of_traps_record))
                    {
                        return;
                    }
                    break;
                case 2:
                    Storage.point_of_spawn_record = strKeyArray[i] - 'C';
                    if (!CheckIsKeyOk(12, Storage.point_of_spawn_record))
                    {
                        return;
                    }
                    break;
                case 3:
                    x = strKeyArray[i] - 'D';
                    if (!CheckIsKeyOk(6, x))
                    {
                        return;
                    }
                  
                    switch (x)
                    {
                        case 1:
                            Storage.time_limit_records = "";
                            break;
                        case 2:
                            Storage.time_limit_records = "1 минута";
                            break;
                        case 3:
                            Storage.time_limit_records = "2 минуты";
                            break;
                        case 4:
                            Storage.time_limit_records = "3 минуты";
                            break;
                        case 5:
                            Storage.time_limit_records = "4 минуты";
                            break;
                        case 6:
                            Storage.time_limit_records = "5 минут";
                            break;

                    }

                    break;
                case 4:
                    Storage.smoke_level_records = strKeyArray[i] - 'E';
                    if (!CheckIsKeyOk(4, Storage.smoke_level_records))
                    {
                        return;
                    }
                    break;
                case 5:
                    x = strKeyArray[i] - 'F';
                    if (!CheckIsKeyOk(6, x))
                    {
                        return;
                    }
                    switch (x)
                    {
                        case 1:
                            Storage.max_health_record = 10;
                            break;
                        case 2:
                            Storage.max_health_record = 20;
                            break;
                        case 3:
                            Storage.max_health_record = 50;
                            break;
                        case 4:
                            Storage.max_health_record = 70;
                            break;
                        case 5:
                            Storage.max_health_record = 80;
                            break;
                        case 6:
                            Storage.max_health_record = 100;
                            break;

                    }
                    errorText.gameObject.SetActive(false);


                    SceneManager.LoadScene("SampleScene");
                    break;

            }
        }
    }
    bool CheckIsKeyOk(int HighLineNumber, int KeyNumber)
    {
        if (KeyNumber < 1 || KeyNumber > HighLineNumber)
        {
            errorText.gameObject.SetActive(true);
            return false;
        }
        return true;
    }
}

