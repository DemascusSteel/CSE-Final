using System;
using System.Diagnostics;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;
using Example.Breaker.Menu;
using Example.Breaker.Shared;


namespace Example.Breaker.Game
{
    public class LoadSceneAction : Byui.Games.Scripting.Action
    {
        private SceneLoader _menuSceneLoader;
        private int tank = 2;

        public LoadSceneAction(IServiceFactory serviceFactory)
        {
            _menuSceneLoader = new MenuSceneLoader(serviceFactory);
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
             {
            //    Lives status = scene.GetFirstActor<Lives>("lives");
            //     if (status.IsDead())
            //     {
            //         _menuSceneLoader.Load(scene);
            //     }
            
            // code to turn "tank" into 1 if either tank died on the game

            if (tank < 2)
                {
                    _menuSceneLoader.Load(scene);
                }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't load scene.", exception);
            }
        }
    }
}