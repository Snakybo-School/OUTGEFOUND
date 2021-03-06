﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Listens for the death of it's parent entity, and does something when that happens
/// </summary>
public class EnemyDeathBehaviour : MonoBehaviour
{
	[SerializeField] private AudioAssetGroup deathSounds;
	[SerializeField] private GameObject ragdoll;

	private AudioManager audioManager;
	private Entity entity;

	protected void Awake()
	{
		audioManager = Dependency.Get<AudioManager>();
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<EntityDiedEvent>(OnDead);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<EntityDiedEvent>(OnDead);
	}

	private void OnDead(EntityDiedEvent evt)
	{
		audioManager.PlayRandomAt(deathSounds, entity.transform.position);

		GameObject go = Instantiate(ragdoll, entity.transform.position, entity.transform.rotation) as GameObject;
		go.transform.SetParent(entity.transform.parent);

		Destroy(entity.gameObject);
	}
}
