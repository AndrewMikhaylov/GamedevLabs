using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Core.Services.Updater;
using InputReader;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputReader _gameUIInputReader;
        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;

        private void Awake()
        {
            if (_projectUpdater==null)
            {
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
            }
            else
            {
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;
            }
            _externalDevicesInput = new ExternalDevicesInputReader();
            _playerSystem = new PlayerSystem(_playerEntity, new List<IEntityInputSource>()
            {
                _externalDevicesInput,
                _gameUIInputReader
            });
        }
    }
}