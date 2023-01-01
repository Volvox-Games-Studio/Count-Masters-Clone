using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Emre;
using UnityEngine;

public class CannonLevelEnd : MonoBehaviour
{
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject ragdollPlayerPrefab;
    [SerializeField] private Transform aim;
    [SerializeField] private Transform bulletExit;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireForce;
    [SerializeField] private float aimMoveSpeed;
    

    [SerializeField] private float aimXMin;
    [SerializeField] private float aimXMax;
    [SerializeField] private float aimYMin;
    [SerializeField] private float aimYMax;
    

    private FloatingJoystick joystick;
    private int playerBullet;
    private bool isEndGameStarted = false;
    
    public void GetPlayersInCannon()
    {
        GameEvents.RaiseStartLevelEnding(LevelEndingType.Cannon);
        foreach (var player in PlayerSpawner.players)
        {
            player.transform.DOMove(cannon.position, 1f).OnComplete(()=>
            {
                player.transform.DOScale(Vector3.zero, .25f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                     {
                        player.Kill(true);
                     });
            });
        }

        playerBullet = PlayerSpawner.players.Count;
        Debug.Log(playerBullet);
        joystick = FindObjectOfType<FloatingJoystick>();
        isEndGameStarted = true;
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (playerBullet > 0)
        {
            yield return new WaitForSeconds(fireRate);

            var direction = (aim.position - bulletExit.position).normalized;
            var force = direction * fireForce;
            var obj = Instantiate(ragdollPlayerPrefab, bulletExit.position, Quaternion.identity);
            var arr = obj.GetComponentsInChildren<Rigidbody>();
            foreach (var VARIABLE in arr)
            {
                VARIABLE.AddForce(force, ForceMode.VelocityChange);
            }
            

            playerBullet--;
        }
    }

    private void MoveAim()
    {
        if (!isEndGameStarted)
            return;
        
        var x= joystick.Horizontal;
        var y = joystick.Vertical;
        var dir = new Vector3(x, y, 0);
        
        aim.transform.Translate(dir * Time.deltaTime * aimMoveSpeed);

        if (aim.transform.position.x > aimXMax)
        {
            var newPos = new Vector3(aimXMax, aim.transform.position.y, aim.transform.position.z);
            aim.transform.position = newPos;
        }else if (aim.transform.position.x < aimXMin)
        {
            var newPos = new Vector3(aimXMin, aim.transform.position.y, aim.transform.position.z);
            aim.transform.position = newPos;
        }
        
        if (aim.transform.position.y > aimYMax)
        {
            var newPos = new Vector3(aim.transform.position.x, aimYMax, aim.transform.position.z);
            aim.transform.position = newPos;
        }else if (aim.transform.position.y < aimYMin)
        {
            var newPos = new Vector3(aim.transform.position.x, aimYMin, aim.transform.position.z);
            aim.transform.position = newPos;
        }
        

    }

    private void Update()
    {
        MoveAim();
        if (isEndGameStarted)
        {
            aim.gameObject.SetActive(true);
            var direction = (aim.position - bulletExit.position).normalized;
            cannon.forward = direction;
        }
    }
}
