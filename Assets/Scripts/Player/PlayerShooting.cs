using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShotPrimary = 20;
    public int damagePerShotSecondary = 50;
    public float timeBetweenPrimaryBullets = .15f;
    public float timeBetweenSecondaryBullets = .5f;
    public float rangePrimary = 100f;
    public float rangeSecondary = 5f;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    [SerializeField] private LineRenderer gunLine;
    [SerializeField] private LineRenderer[] shotgunLines;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = .2f;
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        if (PauseManager.isPaused || playerMovement.isDashing)
        {
            return;
        }

        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenPrimaryBullets)
        {
            ShootPrimary();
        }

        if(Input.GetButton("Fire2") && timer >= timeBetweenSecondaryBullets)
        {
            ShootSecondary();
        }

        if(timer >= timeBetweenPrimaryBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void ShootPrimary()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay, out shootHit, rangePrimary, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShotPrimary, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * rangePrimary);
        }
    }

    void ShootSecondary()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        // Setting each shotgun line's starting point
        foreach (LineRenderer l in shotgunLines)
        {
            l.enabled = true;
            l.SetPosition(0, transform.position);
        }
        // Preparing to aim each shotgun line at a certain angle
        shootRay.origin = transform.position;
        Quaternion[] shotAngles = 
        {
            Quaternion.AngleAxis(30f, Vector3.up),
            Quaternion.AngleAxis(15f, Vector3.up),
            Quaternion.AngleAxis(-15f, Vector3.up),
            Quaternion.AngleAxis(-30f, Vector3.up)
        };
        // Firing each shotgun line
        for (int i = 0; i < shotAngles.Length; i++)
        {
            shootRay.direction = shotAngles[i] * transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, rangeSecondary, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShotSecondary, shootHit.point);
                }
                shotgunLines[i].SetPosition(1, shootHit.point);

            }
            else
            {
                shotgunLines[i].SetPosition(1, shootRay.origin + shootRay.direction * rangeSecondary);
            }
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        foreach(LineRenderer l in shotgunLines)
        {
            l.enabled = false;
        }
    }
}
