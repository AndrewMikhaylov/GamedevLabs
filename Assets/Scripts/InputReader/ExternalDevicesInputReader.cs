using System;
using Core.Services.Updater;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputReader
{
    public class ExternalDevicesInputReader: IEntityInputSource, IDisposable
    {

        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }
    
        public void ResetOneTimeActions()
        {
            Jump = false;
            Attack = false;
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
        }
    
        private void OnUpdate()
        {
         
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }

            if (Input.GetButtonDown("Fire1")&& !IsPointerOverUI())
            {
                Attack = true;
            }
        }
     
     

        private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    
    }
}
