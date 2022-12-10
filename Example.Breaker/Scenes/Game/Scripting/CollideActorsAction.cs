using System;
using System.Collections.Generic;
using System.Numerics;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;

namespace Example.Breaker.Game
{
    public class CollideActorsAction : Byui.Games.Scripting.Action
    {
        private IAudioService _audioService;
        private ISettingsService _settingsService;

        public CollideActorsAction(IServiceFactory serviceFactory) 
        {
            _audioService = serviceFactory.GetAudioService();
            _settingsService = serviceFactory.GetSettingsService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                DoTankCollisions(scene);
                DoBallWallCollision(scene);
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't move actors.", exception);
            }
        }

        private void DoTankCollisions(Scene scene)
        {
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");
            DoBallTankCollision(scene, tank1, "tank1");
            DoTankWallCollision(scene, tank1);
            DoBallTankCollision(scene, tank2, "tank2");
            DoTankWallCollision(scene, tank2);
        }

        private void DoBallTankCollision(Scene scene, Tank tank, string group)
        {
            List<Ball> balls = scene.GetAllActors<Ball>("balls");

            foreach(Ball ball in balls)
            {

                if (ball.Overlaps(tank) && ball != tank.GetBall())
                {
                    // scene.RemoveActor(group, tank);
                    tank.Tint(Color.Green());
                    tank.KillTank();
                }
            }
            
        }

        private void DoBallWallCollision(Scene scene)
        {
            List<Actor> walls = scene.GetAllActors<Actor>("walls");
            List<Ball> balls = scene.GetAllActors<Ball>("balls");
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");

            
            foreach(Ball ball in balls)
            {
                if ((ball != tank1.GetBall()) && ( ball != tank2.GetBall()))
                {
                        foreach(Actor wall in walls)
                    {
                        if (ball.OverlapsTop(wall) || ball.OverlapsBottom(wall) && !ball.OverlapsLeft(wall) && !ball.OverlapsRight(wall))
                        {
                            ball.BounceY();
                            string sound = _settingsService.GetString("bounceSound");
                            ball.RemoveLife();
                        // _audioService.PlaySound(sound);
                        }
                        if (ball.OverlapsRight(wall) || ball.OverlapsLeft(wall) && !ball.OverlapsTop(wall) && !ball.OverlapsBottom(wall))
                        {
                            ball.BounceX();
                            string sound = _settingsService.GetString("bounceSound");
                            ball.RemoveLife();
                        //  _audioService.PlaySound(sound);
                        }
                        else if (ball.Overlaps(wall))
                        {
                            ball.BounceCorner();
                            string sound = _settingsService.GetString("bounceSound");
                            ball.RemoveLife();
                        }

                    }
                }
            }
        }

        private void DoTankWallCollision(Scene scene, Tank tank)
        {
            List<Image> walls = scene.GetAllActors<Image>("walls");
            
            foreach(Image wall in walls)
            {
                if (tank.Overlaps(wall))
                {
                    tank.Steer(-tank.GetVelocity());
                }
                if (tank.Overlaps(wall))
                {
                    tank.Steer(-tank.GetVelocity());
                }

            }
        }

        
    }
}