using UnityEngine;

public class SaveDataController : MonoBehaviour
{

    static SaveDataController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Hi");
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

