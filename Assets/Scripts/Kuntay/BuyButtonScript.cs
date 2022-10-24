using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyButtonScript : MonoBehaviour
{
    [SerializeField] GameObject _level1Turret, _mergeAlaniParent, _turretOlusturmaNoktasi;
    [SerializeField] Text _turretBedel;

    private float _timer = 0, _sayac;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("TurretBedelBaslangic") == 0)
        {
            _turretBedel.text = "FREE";
            PlayerPrefs.SetInt("TurretBedelBaslangic", 1);

            PlayerPrefs.SetFloat("totalScore", 999999);  // TOTAL SCORE BAŞLANGIÇ AYARININ YERİ
            UIController.instance.SetGamePlayScoreText();

            PlayerPrefs.SetInt("TurretBedel", 0);
        }
        else
        {
            if ((PlayerPrefs.GetInt("TurretBedel")) == 0)
            {
                _turretBedel.text = "FREE";
            }
            else
            {
                _turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));

               /*if (PlayerPrefs.GetInt("TurretBedel")<1000)
                {
                    _turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));

                }
                else
                {
                    _turretBedel.text = "$" + ((PlayerPrefs.GetInt("TurretBedel")/1000).ToString("0.##")+" K");

                }*/

            }
            PlayerPrefs.SetInt("MergeAlaniDolulukAdeti", 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            if (PlayerPrefs.GetInt("SesKapat") == 0)
            {
                transform.GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                transform.GetComponent<AudioSource>().enabled = false;
            }
            _timer = _timer + Time.deltaTime;
            if (_timer > .1f)
            {
                _timer = 0;

                if (PlayerPrefs.GetFloat("totalScore") < PlayerPrefs.GetInt("TurretBedel"))
                {
                    transform.GetComponent<Button>().interactable = false;
                }
                else
                {
                    PlayerPrefs.SetInt("MergeAlaniDolulukAdeti", 0);
                    for (int i = 0; i < _mergeAlaniParent.transform.childCount; i++)
                    {
                        if (_mergeAlaniParent.transform.GetChild(i).GetChild(0).GetComponent<mergeAlaniDoluluk>()._doluluk == true)
                        {
                            PlayerPrefs.SetInt("MergeAlaniDolulukAdeti", PlayerPrefs.GetInt("MergeAlaniDolulukAdeti") + 1);
                        }
                        else
                        {

                        }
                    }
                    if (PlayerPrefs.GetInt("MergeAlaniDolulukAdeti") == _mergeAlaniParent.transform.childCount)
                    {
                        transform.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        transform.GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }


    public void BuyButtonActive()
    {
        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        if (PlayerPrefs.GetInt("OnboardingDone") == 0)
        {
            GameObject.Find("OnboardingCotrol").GetComponent<OnboardingControl>()._devam1 = true;
        }

        transform.GetComponent<Button>().interactable = false;
        for (int i = 0; i < _mergeAlaniParent.transform.childCount; i++)
        {
            if (_mergeAlaniParent.transform.GetChild(i).GetChild(0).GetComponent<mergeAlaniDoluluk>()._doluluk == false)
            {
                _mergeAlaniParent.transform.GetChild(i).GetChild(0).transform.GetComponent<mergeAlaniDoluluk>()._doluluk = true;
                GameObject _newTurret = Instantiate(_level1Turret, _turretOlusturmaNoktasi.transform.position, Quaternion.identity);
                _newTurret.transform.parent = null;
                _newTurret.transform.localPosition = _turretOlusturmaNoktasi.transform.position;
                _newTurret.transform.DOJump(new Vector3(_mergeAlaniParent.transform.GetChild(i).transform.position.x, 0.25f, _mergeAlaniParent.transform.GetChild(i).transform.position.z), 2, 1, .5f);
                PlayerPrefs.SetInt("ButonaBasmaSayisi", PlayerPrefs.GetInt("ButonaBasmaSayisi") + 1);

                //SDK icindeki level takip kodu buraya yazılacak

                AppMetrica.Instance.ReportEvent("buton_basma_sayisi - " + PlayerPrefs.GetInt("ButonaBasmaSayisi").ToString());
                AppMetrica.Instance.SendEventsBuffer();

                PlayerPrefs.SetFloat("EnemySpawnRate", PlayerPrefs.GetFloat("EnemySpawnRate") * 0.93f);
                break;
            }
        }

        PlayerPrefs.SetFloat("totalScore", PlayerPrefs.GetFloat("totalScore") - PlayerPrefs.GetInt("TurretBedel"));
        UIController.instance.SetGamePlayScoreText();
        if (PlayerPrefs.GetInt("ButonaBasmaSayisi") > 0)
        {
            PlayerPrefs.SetInt("TurretBedel", PlayerPrefs.GetInt("TurretBedel") + (PlayerPrefs.GetInt("ButonaBasmaSayisi") - 1) * 17 + 5);
            //_turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));
            if (PlayerPrefs.GetInt("TurretBedel") < 1000)
            {
                _turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));

            }
            else
            {
                _turretBedel.text = "$" + ((PlayerPrefs.GetInt("TurretBedel") / 1000).ToString("0.##") + " K");

            }

        }
        else
        {
            PlayerPrefs.SetInt("TurretBedel", PlayerPrefs.GetInt("TurretBedel") + 0);
            _turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));
            /*if (PlayerPrefs.GetInt("TurretBedel") < 1000)
            {
                _turretBedel.text = "$" + (PlayerPrefs.GetInt("TurretBedel"));

            }
            else
            {
                _turretBedel.text = "$" + ((PlayerPrefs.GetInt("TurretBedel") / 1000).ToString("0.##") + " K");

            }*/
        }
        transform.GetComponent<Button>().interactable = true;

    }

}