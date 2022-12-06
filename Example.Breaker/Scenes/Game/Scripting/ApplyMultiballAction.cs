using System;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Breaker.Game
{
    public class ApplyMultiballAction : Byui.Games.Scripting.Action
    {
        // private int DEFAULT_EXTRA_BALLS = 1;
        private int counter = 0;
        private ISettingsService _settingsService;

        public ApplyMultiballAction(IServiceFactory serviceFactory) 
        {
            _settingsService = serviceFactory.GetSettingsService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            Tank tank1 = scene.GetFirstActor("tank1");
            Tank tank2 = scene.GetFirstActor("tank2");
            TankExecute(scene, deltaTime, callback, tank1);
            TankExecute(scene, deltaTime, callback, tank2);

        }
        private void TankExecute(Scene scene, float deltaTime, IActionCallback callback, Tank tank)
        {
            try
                {
                    counter ++;
                    {
                    bool isShot = tank.HasBall();
                    float x = tank.GetCenterX();
                    float y = tank.GetTop();
               //     Time = timeService.getCurrentTime();
                    if (!isShot && counter >= 5)
                    {
                            ActorFactory actorFactory = new ActorFactory(_settingsService);
                    //    Ball first = scene.GetFirstActor<Ball>("balls");
                    //    for (int i = 0; i < DEFAULT_EXTRA_BALLS; i++)
                     //   {
                            Ball ball = actorFactory.CreateBall(x, y);
                            // ball.MoveTo(first.GetPosition());
                            scene.AddActor("balls", ball);
                            tank.AttachBall(ball);
                            counter = 0;
                      //  }
                    }
                    };
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't apply multiball.", exception);
            }

        }
    }
}