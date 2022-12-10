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
        private int tank = 10000;

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
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");

            if (tank1.IsAlive() == 0){
                tank--;
            }
            if (tank1.IsAlive() == 0){
                tank--;
            }

            if (tank < 2)
                {
                    _menuSceneLoader.Load(scene);
                }
            else{
                tank--;
            }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't load scene.", exception);
            }
        }
    }
}