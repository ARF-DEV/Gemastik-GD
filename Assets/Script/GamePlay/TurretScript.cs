using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : Unit, IConnectabel
{
    public RocketScript rocketPrefab;
    public float rocketMovementSpeed = 10f;
    
    public GameObject con;
    public float coolDown = 1f;
    public float curCoolDown = 0f;
    public bool canShoot;
    public AudioClip shootSound;
    
    private AudioSource audioSource;
    protected override void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();
        if (con != null)
            con.GetComponent<ICanConnect>().Connect(this.gameObject);
    }
    public void TurretAction()
    {
        
        if (curCoolDown > 0)
        {
            curCoolDown -= Time.deltaTime;
        }
        else
        {
            Attack();
            curCoolDown = coolDown;
        }
    }
    
    public GameObject Attack()
    {
        if (canShoot)
        {
            Vector3 rocketSpawnPos = transform.position + transform.up;
            GameObject rocket = Instantiate(rocketPrefab.gameObject,  rocketSpawnPos, transform.rotation);

            audioSource.PlayOneShot(shootSound);
               
            RocketScript rc = rocket.GetComponent<RocketScript>();
            rc.SetMoveSpeed(rocketMovementSpeed);
            
            return rocket;
        }
        return null;
    }

    public void Connect(GameObject ICanConnect)
    {
        con = ICanConnect;
    }
    public void Disconnect()
    {
        if (con != null)
            con.GetComponent<ICanConnect>().Disconnect();
        
        con = null;
    }
    public bool isConnected()
    {
        return con != null;
    }
    public bool CanDisconnect()
    {
        if (con == null) return true;
        
        return con.GetComponent<ICanConnect>().CanDisconnect();
    }

    public void On()
    {
        canShoot = true;
    }
    public void Off()
    {
        canShoot = false;
    }

    
}
