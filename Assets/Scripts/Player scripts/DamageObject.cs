using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damage = 1;

    void Start()
    {
        damage = Random.Range(1, 10);
    }

    void Update()
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Storage.playerHP -= damage;
                Debug.Log(Storage.playerHP);
                //ѕотом надо перенести в адекватное место
                if (Storage.playerHP <= 0)
                {
                    Debug.Log(Storage.playerHP);
                }
            }
        }

    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Storage.playerHP -= damage;
            Debug.Log(Storage.playerHP);
            //ѕотом надо перенести в адекватное место
            if (Storage.playerHP <= 0)
            {
                Debug.Log(Storage.playerHP);
            }
        }
    }
}
