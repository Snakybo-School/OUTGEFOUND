﻿using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour
{
	private EntityMovement movement;
	private NavMeshAgent agent;
	private Animator animator;
	private Entity entity;

	protected void Awake()
	{
		animator = GetComponent<Animator>();
		entity = GetComponentInParent<Entity>();
		agent = entity.GetComponent<NavMeshAgent>();
		movement = entity.GetComponent<EntityMovement>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<ReloadEvent>(OnReloadEvent);
		entity.Events.AddListener<WeaponFireEvent>(OnWeaponFireEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<ReloadEvent>(OnReloadEvent);
		entity.Events.RemoveListener<WeaponFireEvent>(OnWeaponFireEvent);
	}

	protected void LateUpdate()
	{
		animator.SetFloat("Velocity", agent.velocity.magnitude);
		animator.SetBool("Crouching", movement.Crouching);
	}

	private void OnReloadEvent(ReloadEvent evt)
	{
		animator.SetTrigger("Reload");
	}

	private void OnWeaponFireEvent(WeaponFireEvent evt)
	{
		animator.SetTrigger("Shoot");
	}
}