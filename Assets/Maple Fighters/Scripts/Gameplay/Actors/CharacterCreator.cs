﻿using System.Threading.Tasks;
using CommonTools.Coroutines;
using CommonTools.Log;
using Scripts.Containers;
using Scripts.Utils;
using Game.Common;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.Actors
{
    public class CharacterCreator : DontDestroyOnLoad<CharacterCreator>
    {
        private readonly ExternalCoroutinesExecutor coroutinesExecutor = new ExternalCoroutinesExecutor();

        protected override void OnAwake()
        {
            base.OnAwake();

            SubscribeToEvents();
        }

        private void Update()
        {
            coroutinesExecutor.Update();
        }

        private void OnDestroy()
        {
            coroutinesExecutor.Dispose();

            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            ServiceContainer.GameService.CharacterAdded.AddListener(OnCharacterAdded);
            ServiceContainer.GameService.CharactersAdded.AddListener(OnCharactersAdded);
        }

        private void UnsubscribeFromEvents()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            ServiceContainer.GameService.CharacterAdded.RemoveListener(OnCharacterAdded);
            ServiceContainer.GameService.CharactersAdded.RemoveListener(OnCharactersAdded);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            coroutinesExecutor.StartTask(EnterScene);
        }

        private async Task EnterScene(IYield yield)
        {
            var parameters = await ServiceContainer.GameService.EnterScene(yield);
            OnEnteredScene(parameters);
        }

        private void OnEnteredScene(EnterSceneResponseParameters? parameters)
        {
            if (!parameters.HasValue)
            {
                if (DummyCharacterDetails.Instance == null)
                {
                    LogUtils.Log(MessageBuilder.Trace("Could not find dummy character details. Please add DummyCharacterDetails into a scene."), LogMessageType.Warning);
                    return;
                }

                parameters = DummyCharacterDetails.Instance.GetDummyCharacterParameters();
            }

            // Will create scene object.
            ServiceContainer.GameService.EnteredScene.Invoke(parameters.Value);

            // Will create a character for this scene object.
            var characterSpawnDetails = parameters.Value.Character;
            CreateCharacter(characterSpawnDetails);
        }

        private void OnCharacterAdded(CharacterAddedEventParameters parameters)
        {
            var characterSpawnDetails = parameters.CharacterSpawnDetails;
            CreateCharacter(characterSpawnDetails);
        }

        private void OnCharactersAdded(CharactersAddedEventParameters parameters)
        {
            foreach (var characterSpawnDetails in parameters.CharactersSpawnDetails)
            {
                CreateCharacter(characterSpawnDetails);
            }
        }

        private void CreateCharacter(CharacterSpawnDetailsParameters characterSpawnDetails)
        {
            var id = characterSpawnDetails.SceneObjectId;
            var sceneObject = SceneObjectsContainer.Instance.GetRemoteSceneObject(id);
            sceneObject?.GetGameObject().GetComponent<ICharacterCreator>().Create(characterSpawnDetails);
        }
    }
}