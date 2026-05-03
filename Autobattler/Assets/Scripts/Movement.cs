using System;
using System.Numerics;
using Player;
using Player.PlayerProviders;
using Player.StateController;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private PlayerProvider playerStateController;
    
    [Header("Animations")] 
    [SerializeField] private string animationWalkName;

    private Vector2 _mouseInputCoordinate;
    private const float DistanceToEndMove = 0.5f;

    private bool IsWalking = false;
    
    public void Update()
    {
        if (!playerStateController.GetCurrentState(PlayerStates.IsDialogWindow))
        {
            ResetMovement();
            
            return;
        }
        
        ClickListener();
        Move();
    }

    public void ResetMovement()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        _mouseInputCoordinate = Vector2.zero;
            
        if (IsWalking)
        {
            animator.SetBool(animationWalkName, false);
            IsWalking = false;
        }
    }
    
    private void ClickListener()
    {
        if (Input.GetMouseButton(0))
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouseInputCoordinate = mouse;
        }
    }

    private void Move()
    {
        if (_mouseInputCoordinate == Vector2.zero)
            return;
        
        spriteRenderer.flipX = _mouseInputCoordinate.x < transform.position.x;
        
        Vector2 moveDirection = (_mouseInputCoordinate - (Vector2)transform.position).normalized;
        rigidbody2D.linearVelocity = moveDirection * playerConfig.Speed;
        
        if (!IsWalking)
        {
            animator.SetBool(animationWalkName, true);
            IsWalking = true;
        }
        
        CheckDistanceToTarget();
    }
    

    private void CheckDistanceToTarget()
    {
        if (Vector2.Distance(transform.position, _mouseInputCoordinate) < DistanceToEndMove)
        {
            _mouseInputCoordinate = Vector2.zero;
            rigidbody2D.linearVelocity = Vector2.zero;

            animator.SetBool(animationWalkName, false);
            IsWalking = false;
        }
    }
}
