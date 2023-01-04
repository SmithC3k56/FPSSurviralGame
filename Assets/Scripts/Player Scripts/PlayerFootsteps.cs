using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;
    [SerializeField] private AudioClip[] footstep_Clips;

    private CharacterController character_Controller;

    [HideInInspector] public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    private void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }


    private void Update()
    {
        CheckToPlayFootstepSound();        
    }
    private void CheckToPlayFootstepSound()
    {
        // if we are NOT on the ground
        if(!character_Controller.isGrounded){
            return;
        }

        if(character_Controller.velocity.sqrMagnitude > 0){
            // accumulate distance is  the value how far can we go
            accumulated_Distance += Time.deltaTime;
            if(accumulated_Distance > step_Distance){
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clips[Random.Range(0, footstep_Clips.Length)];
                footstep_Sound.Play();
                accumulated_Distance = 0f;

            
            }
        }else{
            accumulated_Distance = 0f;
        }
    }
}
