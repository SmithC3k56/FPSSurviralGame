using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scope : MonoBehaviour
{

    public event EventHandler OnRifleUp; 
    public event EventHandler OnRifleDown; 
    public event EventHandler OnZoomIn; 
    public event EventHandler OnZoomOut;

    [SerializeField] private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetBool("RifleDown",false);
            OnRifleUp?.Invoke(this,EventArgs.Empty);
        }
        if (Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("RifleDown",true);
            OnRifleUp?.Invoke(this,EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnZoomOut?.Invoke(this,EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnZoomIn?.Invoke(this,EventArgs.Empty);
        }
    }
}
