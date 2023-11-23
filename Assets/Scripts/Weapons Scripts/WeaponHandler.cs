using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    public WeaponAim weapon_Aim;

 
    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_Point;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation(){
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }
    public void Aim(bool canAim){
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }
    private void Turn_On_MuzzleFlash(){
        muzzleFlash.SetActive(true);
    }
    private void Turn_Off_MuzzleFlash(){
        muzzleFlash.SetActive(false);
    }
    private void Play_ShootSound(){
        shootSound.Play();
    }
    private void Play_ReloadSound(){
        reload_Sound.Play();
    }
    private void Turn_On_AttackPoint(){
        attack_Point.SetActive(true);
    }
    private void Turn_Off_AttackPoint(){
        if(attack_Point.activeInHierarchy){
            attack_Point.SetActive(false);
        }
    }
}
