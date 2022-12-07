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
                SteerTanks(scene);
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't steer tank.", exception);
            }
        }

        private void ReleaseBall(Scene scene)
        {
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");
            TankReleaseBall(tank1, KeyboardKey.Space);
            TankReleaseBall(tank2, KeyboardKey.Enter);
        }

        private void TankReleaseBall(Tank tank, KeyboardKey Fire)
        {
            if (!_keyboardService.IsKeyDown(Fire)){
            tank.SetFireKeyStatus(true);
            }
            if (tank.HasBall() && _keyboardService.IsKeyDown(Fire) && tank.IsFireKeyReleased())
            {
                tank.ReleaseBall();
                tank.SetFireKeyStatus(false);
            }
        }
        

        private void SteerTanks(Scene scene)
        {
            float directionX = 0;
            float directionY = 0;
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");
            SteerTank(tank1, KeyboardKey.W, KeyboardKey.A, KeyboardKey.D, KeyboardKey.S, directionX, directionY);
            SteerTank(tank2, KeyboardKey.I, KeyboardKey.J, KeyboardKey.L, KeyboardKey.K, directionX, directionY);

        }
        
        private void SteerTank(Tank tank, KeyboardKey Up, KeyboardKey Left, KeyboardKey Right, KeyboardKey Down, float directionX, float directionY) 
        {
            if (_keyboardService.IsKeyDown(Up))
            {
                directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("tankVelocity") * -1;
                directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("tankVelocity");
                
            }
            else if (_keyboardService.IsKeyDown(Down))
            {
                directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("tankVelocity");
                directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("tankVelocity")*-1;
         
         
            }
            
            tank.Steer(directionX, directionY);

            directionY = Convert.ToSingle(Math.Cos( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("ballVelocity") * -1;
            directionX = Convert.ToSingle(Math.Sin( (tank.GetRotation() * Math.PI) / 180)) * _settingsService.GetFloat("ballVelocity");

            tank.SteerBall(directionX, directionY);

            if (_keyboardService.IsKeyDown(Left))
                {
                    tank.Rotate(-4);
                }
                else if (_keyboardService.IsKeyDown(Right))
                {
                    tank.Rotate(4);
                }
        }
    }
}