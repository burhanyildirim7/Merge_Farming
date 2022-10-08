using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YapbozAlaniDoluluk : MonoBehaviour
{
    public bool _soketDoluluk;
    public int _soketNumber,_turretListeDeger=12;

    
    void Start()
    {
        if (PlayerPrefs.GetInt("OyunIlkKezAcildi"  + _soketNumber )== 0)
        {
            PlayerPrefs.SetInt("OyunIlkKezAcildi" + _soketNumber, 1);
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, 12);
        }
        else
        {
            if (PlayerPrefs.GetInt("TurretGetir" + _soketNumber) < 12)
            {
                Instantiate(GameObject.Find("SOKETLER_PARENT").transform.GetComponent<PlayerPrefKontrol>()._turrets[PlayerPrefs.GetInt("TurretGetir" + _soketNumber)], null).transform.position =
        new Vector3(transform.position.x, 0.25f, transform.position.z);
            }
            else
            {

            }

        }

    }
     void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="turret")
        {
            switch (other.transform.GetComponent<TurretMergeKontrol>()._turretNum)
            {

                case 1:
                    _turretListeDeger = 0;
                    break;

                case 2:
                    _turretListeDeger = 1;
                    break;

                case 4:
                    _turretListeDeger = 2;
                    break;

                case 8:
                    _turretListeDeger = 3;
                    break;

                case 16:
                    _turretListeDeger = 4;
                    break;

                case 32:
                    _turretListeDeger = 5;
                    break;

                case 64:
                    _turretListeDeger = 6;
                    break;

                case 128:
                    _turretListeDeger = 7;
                    break;

                case 256:
                    _turretListeDeger = 8;
                    break;

                case 512:
                    _turretListeDeger = 9;
                    break;

                case 1024:
                    _turretListeDeger = 10;
                    break;

                case 2048:
                    _turretListeDeger = 11;
                    break;


            }
            PlayerPrefs.SetInt("TurretGetir"+ _soketNumber, _turretListeDeger);
        }   
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "turret")
        {
            switch (other.transform.GetComponent<TurretMergeKontrol>()._turretNum)
            {

                case 1:
                    _turretListeDeger = 0;
                    break;

                case 2:
                    _turretListeDeger = 1;
                    break;

                case 4:
                    _turretListeDeger = 2;
                    break;

                case 8:
                    _turretListeDeger = 3;
                    break;

                case 16:
                    _turretListeDeger = 4;
                    break;

                case 32:
                    _turretListeDeger = 5;
                    break;

                case 64:
                    _turretListeDeger = 6;
                    break;

                case 128:
                    _turretListeDeger = 7;
                    break;

                case 256:
                    _turretListeDeger = 8;
                    break;

                case 512:
                    _turretListeDeger = 9;
                    break;

                case 1024:
                    _turretListeDeger = 10;
                    break;

                case 2048:
                    _turretListeDeger = 11;
                    break;


            }
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, _turretListeDeger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "turret")
        {
            PlayerPrefs.SetInt("TurretGetir" + _soketNumber, 12);


        }
    }


}
