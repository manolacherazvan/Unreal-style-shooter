using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{
    int equipedGun=0; // 0 - minigun, 1 - sniper
    public GameObject[] gunModels;
    public Camera camera;
    public ParticleSystem[] muzzleflash;

    private float timeToFire = 0f;
    private float fireRate = 15f;

    int ammoInMagazine;
    int magazineCapacity;

    int minigunAmmo;
    int sniperAmmo;

    public Text ammoText;
    public GameObject reloadText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (equipedGun)
        {
            case 0:
                minigun();
                break;
            case 1:
                sniper();
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            equipedGun++;
            equipedGun = equipedGun % 2;
            switchGunModels();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }
        if (ammoInMagazine > 0 && Input.GetButton("Fire1")&&Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            gunShoot();
            muzzleflash[equipedGun].Play();
            if (equipedGun == 0)
            {
                minigunAmmo--;
            }
            else{
                sniperAmmo--;
            }
        }
        showUI();
    }
    void showUI()
    {
        ammoText.text = ammoInMagazine + "/" + magazineCapacity;
        if (ammoInMagazine == 0)
        {
            reloadText.SetActive(true);
        }else{
            reloadText.SetActive(false);
        }
    }
    void minigun()
    {
        fireRate = 15f;
        magazineCapacity = 60;
        ammoInMagazine = minigunAmmo;
    }
    void sniper()
    {
        fireRate = 5f;
        magazineCapacity = 10;
        ammoInMagazine = sniperAmmo;
    }
    void reload()
    {
        if (equipedGun == 0)
        {
            minigunAmmo=magazineCapacity;
        }
        else
        {
            sniperAmmo = magazineCapacity;
        }
    }
    void switchGunModels()
    {
        switch (equipedGun)
        {
            case 0:
                gunModels[0].SetActive(true);
                gunModels[1].SetActive(false);
                break;
            case 1:
                gunModels[1].SetActive(true);
                gunModels[0].SetActive(false);
                break;
            default:
                break;
        }
    }
    void gunShoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
