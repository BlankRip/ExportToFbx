using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform bulletPos;
    [SerializeField] KeyCode fireKey = KeyCode.Mouse0;
    [SerializeField] KeyCode bulletsModeKey = KeyCode.Alpha1;
    [SerializeField] KeyCode rocketModeKey = KeyCode.Alpha2;
    [SerializeField] float gapBetweenBulletFire;
    [SerializeField] float gapBetweenRocketFire;
    float tracker;
    bool fireRocket;
    bool fireBullets;

    void Start()
    {
        fireBullets = true;
        tracker = gapBetweenBulletFire;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(bulletsModeKey))
        {
            fireBullets = true;
            fireRocket = false;
        }

        if(Input.GetKeyDown(rocketModeKey))
        {
            fireRocket = true;
            fireBullets = false;
        }

        if (Input.GetKey(fireKey) && tracker < 0)
        {
            if (fireBullets)
            {
                ObjectPool.instance.SpawnPoolObj("Bullet", bulletPos.position, Quaternion.identity);
                tracker = gapBetweenBulletFire;
            }
            else if (fireRocket)
            {
                ObjectPool.instance.SpawnPoolObj("Rocket", bulletPos.position, Quaternion.identity);
                tracker = gapBetweenRocketFire;
            }
        }
        else if (tracker >= 0)
            tracker -= Time.deltaTime;
    }
}
