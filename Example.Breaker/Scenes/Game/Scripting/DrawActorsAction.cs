using System;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Breaker.Game
{
    public class DrawActorsAction : Byui.Games.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawActorsAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                
                _videoService.ClearBuffer();
                DrawBalls(scene);
                DrawTank(scene);
                DrawWall(scene);
             //   DrawScore(scene);
    //            DrawLives(scene);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }

        private void DrawBalls(Scene scene)
        {
            List<Ball> balls = scene.GetAllActors<Ball>("balls");

             Tank tank1 = scene.GetFirstActor<Tank>("tank1");

            Tank tank2 = scene.GetFirstActor<Tank>("tank2");

            foreach (Ball ball in balls)
                {
                if ((ball != tank1.GetBall()) && ( ball != tank2.GetBall()))
                    {
                        _videoService.Draw(ball);
                    }
                }
        }

        private void DrawTank(Scene scene)
        {
            Tank tank1 = scene.GetFirstActor<Tank>("tank1");
            _videoService.Draw(tank1);
        
            Tank tank2 = scene.GetFirstActor<Tank>("tank2");
            _videoService.Draw(tank2);
        }
        private void DrawWall(Scene scene)
        {
            List<Actor> walls = scene.GetAllActors("walls");
            foreach(Actor wall in walls)
            {
                _videoService.Draw(wall);
            }
            
        }

        // private void DrawLives(Scene scene)
        // {
        //     Lives lives = scene.GetFirstActor<Lives>("lives");
        //     _videoService.Draw(lives);
        // }

        // private void DrawScore(Scene scene)
        // {
        //     Score score = scene.GetFirstActor<Score>("score");
        //     _videoService.Draw(score);
        // }
    }
}