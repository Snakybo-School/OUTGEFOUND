﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDependency : IDependency
{
	private SceneLoader sceneLoader;
	private CoroutineRunner coroutineRunner;

	private Currency currency;
	private DecalManager decalManager;

	public GameDependency()
	{
		sceneLoader = Dependency.Get<SceneLoader>();
		coroutineRunner = Dependency.Get<CoroutineRunner>();

		currency = Dependency.Get<Currency>();
		decalManager = Dependency.Get<DecalManager>();
	}

	public void Start()
	{
		// Hardcode Level_1 because we probably won't have more levels
		sceneLoader.Load("Level_1");

		coroutineRunner.StartCoroutine(WaitForGameLoad());
	}

	public void Stop()
	{
		GlobalEvents.Invoke(new GameStoppedEvent());

		currency.Destroy();
		decalManager.Destroy();

		sceneLoader.Load("Menu");
	}

	private IEnumerator WaitForGameLoad()
	{
		while(sceneLoader.Loading)
		{
			yield return null;
		}

		currency.Create();
		decalManager.Create();

		GlobalEvents.Invoke(new GameStartedEvent());
	}
}