using UnityEngine;
using System.Collections;
using EZCameraShake;

public class Gun : MonoBehaviour
{

    private Camera_FPS Recoil;

    public GameObject MuzzleFlashEffect;
    public float RecoveryTime; //responsible for recoil management
    public float damage = 10f;
    public float range = 100f;
    public float FireRate = 15f;
    public int maxAmmo = 10;
    public float Recoil_Amount;

    public Camera FpsCam;
    public ParticleSystem Muzzle;
    public GameObject ImpactEffect;

    private bool IsShooting = false;
  
    private int currentAmmo;
    public float ReloadTime = 1f;
    private bool Reloading = false;

    public GameObject CameraAnimatorGet;
    Animator CamAnim;

    public AudioClip shot;
    public AudioSource Source;

    public GameObject AnimatorGet;
    Animator GunAnim;

    void Start()
    {
        CamAnim = CameraAnimatorGet.GetComponent<Animator>();

        Recoil = FindObjectOfType<Camera_FPS>();

        currentAmmo = maxAmmo;
        GunAnim = AnimatorGet.GetComponent<Animator> ();
    }

    private float nextTimeToFire = 0f;

    private void FixedUpdate() // For recoil
    {
        if (IsShooting == true)
        Recoil._rotationX = Recoil._rotationX - Recoil_Amount;
    }

    private void Update()
    {
       

        if (Input.GetKeyDown("r") && currentAmmo <= maxAmmo-1 && !Reloading) // if you press r, start the coroutine of reloading
            StartCoroutine(Reload());
            

        if (Reloading)
        {
            return;
            
        }
            

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/FireRate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        Reloading = true;

        GunAnim.SetTrigger("Reload");

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(ReloadTime);

        currentAmmo = maxAmmo;
        Reloading = false;
    }
    void Shoot()
    {
        StartCoroutine(MuzzleFlash());
        StartCoroutine(GunRecoil());

        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.1f, 0.4f); //strenght, magnitude, fade in time, fade out time

        Muzzle.Play();

        Source.Play();

        

        GunAnim.SetTrigger("Start_shoot");
        CamAnim.SetTrigger("Shot");

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); // gets the name of what you hit, so you can check 

            Target target = hit.transform.GetComponent<Target>();


            hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGo = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f); //this is so the particles get destroyed after a while (optimization)

        }
    }
    IEnumerator MuzzleFlash()
    {
        Debug.Log("Ciao a tutti Ragazzi");
        MuzzleFlashEffect.SetActive(true);
        yield return new WaitForSeconds(0.035f);
        MuzzleFlashEffect.SetActive(false);
    }
    IEnumerator GunRecoil()
    {
        IsShooting = true;
        yield return new WaitForSeconds(RecoveryTime/10); //the lower the divider, the faster it is at recovering |||| WE MIGHT WANT TO CHANGE THIS TO AN EQUATION OF SOME SORT LATER DOWN THE LINE
        IsShooting = false;
    }

}