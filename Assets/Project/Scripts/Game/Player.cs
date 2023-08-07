using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;
    [Header("Gameplay")]
    public int initialAmmo = 12;
    public int initialHealth = 100;
    private int ammo;
    public int Ammo { get { return ammo; } }
    public AudioClip GunShot;
    private AudioSource m_AudioSource;
    public AudioMixerGroup GunShotGroup;
    public AudioMixer mixer;

    private bool isHurt;
    public float knockbackForce = 10f;
    public float hurtDuration = 0.5f;

    private int health;
    public int Health { get { return health; } }
    private bool killed;

    public bool Killed { get { return killed; } }

    void Start()
    {
        ammo = initialAmmo;
        health = initialHealth;
        m_AudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0 && !Killed)
        {
            ammo--;
            GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
            bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;
            m_AudioSource.outputAudioMixerGroup = GunShotGroup;
            m_AudioSource.clip = GunShot;
            m_AudioSource.Play();
        }
    }

    //Check for collisions

    void OnTriggerEnter(Collider otherCollider)
    {
        //Collect ammo crates
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);
        }

        if (!isHurt)
        {
            GameObject hazard = null;
            if (otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if (!enemy.Killed)
                {
                    hazard = enemy.gameObject;
                    health -= enemy.damage;
                }
            }
            else if (otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                if (!bullet.ShotByPlayer)
                {
                    hazard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }

            if (hazard != null)
            {
                isHurt = true;
                //Knockback effect
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutone());
            }

            if (health <= 0)
            {
                if (killed == false)
                {
                    killed = true;

                    OnKill();
                }
            }
        }

        //Hitting an enemy

    }
    IEnumerator HurtRoutone()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }

    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

    }
}
