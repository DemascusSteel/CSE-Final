using System;
using System.Diagnostics;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;
using Example.Breaker.Over;
using Example.Breaker.Shared;


namespace Example.Breaker.Game
{
    public class LoadSceneAction : Byui.Games.Scripting.Action
    {
        private SceneLoader _overSceneLoader;
        public int tank = 0;

        public LoadSceneAction(IServiceFactory serviceFactory)
        {
            _overSceneLoader = new OverSceneLoader(serviceFactory);
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
             {
            //    Lives status = scene.GetFirstActor<Lives>("lives");
            //     if (status.IsDead())
            //     {
            //         _overSceneLoader.Load(scene);
            //     }
            
            // code to turn "tank" into 1 if either tank died on the game
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");

            if (tank1.IsAlive() == 0){
                tank = 2;
                _overSceneLoader.Load(scene);
                }
            if (tank2.IsAlive() == 0){
                tank = 1;
                _overSceneLoader.Load(scene);
            }
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't load scene.", exception);
            }
        }
    }
}