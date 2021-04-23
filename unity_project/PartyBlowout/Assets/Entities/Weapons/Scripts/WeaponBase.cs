using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class WeaponBase : MonoBehaviour
{
    //à compléter si vous avez des idées
    [Header("Gun Settings")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected int clipSize;
    [SerializeField] protected int damage;
    [SerializeField] protected int impactForce;
    [SerializeField] protected int reservedAmmoCapacity;

    [SerializeField] protected bool canShoot;
    [SerializeField] public int currentAmmoClip;
    [SerializeField] public int ammoInReserve;

    //Muzzle Flash (sinon c'est moche quand on tire)
    [SerializeField] private Image muzzleFlashImage;
    [SerializeField] protected Sprite[] flashes;

    //Viser
    [SerializeField] protected Vector3 normalLocalPosition;
    [SerializeField] protected Vector3 aimingLocalPosition;
    private float aimSmoothing = 10f;
    private PhotonView PV;

    // voir plus bas dans DetermineRecoil.
    // public bool randomizeRecoil;
    // public Vector2 randomRecoilConstraints;

    private void Start()
    {
        currentAmmoClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
        canShoot = true;
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(!PV.IsMine) return;
        DetermineAim();
        //shoots
        if (Input.GetMouseButton(0) && canShoot && currentAmmoClip > 0)
        {
            canShoot = false;
            currentAmmoClip--;
            StartCoroutine(ShootGun());
        }

        //reload weapon
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmoClip < clipSize && ammoInReserve > 0)
        {
            int ammountNeeded = clipSize - currentAmmoClip;
            if (ammountNeeded >= ammoInReserve)
                currentAmmoClip += ammoInReserve;
            else
                currentAmmoClip = clipSize;
            ammoInReserve = Math.Max(0, ammoInReserve - ammountNeeded);
            // Let's call this event to update the player's ui, we may have to edit this in the future
            // TODO: Edit this dirty implementation
            onWeaponShoot();
        }
    }

    /// <summary>
    /// shifts into aim mode when right click is pressed.
    /// </summary>
    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
        }
        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPosition;
    }

    /// <summary>
    /// applies small horizontal recoil (no impact on precision)
    /// </summary>
    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * (fireRate * 1.5f);
        // ça peut être utile si on veut mettre du VRAI recoil:
        // if (randomizeRecoil)
        // {
        //     Random random = new Random();
        //     float xRecoil = random.Next((int) -randomRecoilConstraints.x, (int) randomRecoilConstraints.x);
        //     float yRecoil = random.Next((int) -randomRecoilConstraints.y, (int) randomRecoilConstraints.y);
        //
        //     Vector2 recoil = new Vector2(xRecoil, yRecoil);
        // et ensuite appliquer ca à la rotation genre : rotation = recoil.
        // }
    }


    private int range = 500;
    /// <summary>
    /// shoots raycasts from the gun, to the enemy's direction but it hits only those who have the layer AliveEntities.
    /// </summary>
    void RayCastForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, range))
        {
            AliveEntity target = hit.transform.GetComponent<AliveEntity>();
            if (target != null)
            {
                target.TakeDamage(damage, GetComponentInParent<Player>());
            }

            // Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            // if (rb != null)
            // {
            //     rb.constraints = RigidbodyConstraints.None;
            //     rb.AddForce(transform.parent.transform.forward * impactForce);
            // }
        }
    }

    public static event Action onWeaponShootHook;
    public static void onWeaponShoot() => onWeaponShootHook?.Invoke();

    /// <summary>
    /// applies recoil and shoots raycasts.
    /// </summary>
    /// <returns> return if the player can shoot another time</returns>
    IEnumerator ShootGun()
    {
        onWeaponShoot();
        DetermineRecoil();
        StartCoroutine(MuzzleFlash());
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    /// <summary>
    /// randomly spawns sprites in front of gun
    /// </summary>
    /// <returns>return if a sprite can be shown</returns>
    IEnumerator MuzzleFlash()
    {
        Random random = new Random();
        muzzleFlashImage.sprite = flashes[random.Next(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(fireRate);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
