using System;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Breaker.Game
{
    public class SteerActorsAction : Byui.Games.Scripting.Action
    {
        private IKeyboardService _keyboardService;
        private ISettingsService _settingsService;
        private bool releasedSpacebar = false;

        public SteerActorsAction(IServiceFactory serviceFactory)
        {
            _keyboardService = serviceFactory.GetKeyboardService();
            _settingsService = serviceFactory.GetSettingsService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                ReleaseBall(scene);
                SteerTank(scene);
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't steer paddle.", exception);
            }
        }

        private void ReleaseBall(Scene scene)
        {
           
        if (!_keyboardService.IsKeyDown(KeyboardKey.Space)){
        releasedSpacebar = true;
        }
            Tank tank = scene.GetFirstActor<Tank>("tank");
            if (tank.HasBall() && _keyboardService.IsKeyDown(KeyboardKey.Space) && releasedSpacebar)
            {
                tank.ReleaseBall();
                releasedSpacebar = false;
            }
        }



        private void SteerTank(Scene scene)
        {
            float directionX = 0;
            float directionY = 0;
            Tank tank = scene.GetFirstActor<Tank>("tank");
            
            if (_keyboardService.IsKeyDown(KeyboardKey.W))
            {
      //          directionY = _settingsService.GetFloat("paddleVelocity") * -1;
                directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("paddleVelocity") * -1;
                directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("paddleVelocity");
                
            }
            else if (_keyboardService.IsKeyDown(KeyboardKey.S))
            {
         //       directionY = _settingsService.GetFloat("paddleVelocity");
                directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("paddleVelocity");
                directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("paddleVelocity")*-1;
         
         
            }
            
            tank.Steer(directionX, directionY);

            directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("ballVelocity") * -1;
            directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("ballVelocity");

            tank.SteerBall(directionX, directionY);

             if (_keyboardService.IsKeyDown(KeyboardKey.A))
                {
                    tank.Rotate(-4);
                }
                else if (_keyboardService.IsKeyDown(KeyboardKey.D))
                {
                    tank.Rotate(4);
                }
        }
    }
}