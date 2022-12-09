using System;
using System.Collections.Generic;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;
using Example.Breaker.Shared;


namespace Example.Breaker.Game
{
    public class GameSceneLoader : SceneLoader
    {

        private ActorFactory _actorFactory;
        private List<int> wallx = new List<int> {200,200,500,800,800};
        private List<int> wally = new List<int> {250, 600, 800, 0, 400};

       

        public GameSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) 
        {
            ISettingsService settingsService = serviceFactory.GetSettingsService();
            _actorFactory = new ActorFactory(settingsService);
        }

        public override void Load(Scene scene)
        {
            scene.Clear();
            LoadBackground(scene);
            LoadActors(scene);
            LoadActions(scene);
        }

        private void LoadActions(Scene scene)
        {
            IServiceFactory serviceFactory = GetServiceFactory();
            
            SteerActorsAction steerActorsAction = new SteerActorsAction(serviceFactory);
            MoveActorsAction moveActorsAction = new MoveActorsAction(serviceFactory);
            CollideActorsAction collideActorsAction = new CollideActorsAction(serviceFactory);
            LoadSceneAction loadSceneAction = new LoadSceneAction(serviceFactory);
            DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);

            ApplyMultiballAction applyMultiballAction = new ApplyMultiballAction(serviceFactory);

            scene.AddAction(Phase.Input, steerActorsAction);
            scene.AddAction(Phase.Update, moveActorsAction);
            scene.AddAction(Phase.Update, collideActorsAction);
            scene.AddAction(Phase.Update, applyMultiballAction);
            scene.AddAction(Phase.Update, loadSceneAction);
            scene.AddAction(Phase.Output, drawActorsAction);
        }

        private void LoadActors(Scene scene)
        {
            Tank tank1 = _actorFactory.CreateTank("tank1X", "tank1Y", "tank1Image");
            Tank tank2 = _actorFactory.CreateTank("tank2X", "tank2Y", "tank2Image");
            Ball ball1 = _actorFactory.CreateBall(tank1);
            Ball ball2 = _actorFactory.CreateBall(tank2);
            Actor field = _actorFactory.CreateField();
            // Level level = _actorFactory.CreateLevel();
            // Score score = _actorFactory.CreateScore();

                int x = 0;
                int y = 0;

            for(int i = 0; i < 4; i++){
                x = wallx[i];
                y = wally[i];
            Image wall = _actorFactory.CreateWall(x,y);
            scene.AddActor("walls", wall);
             }
     //       Lives lives = _actorFactory.CreateLives();


            scene.AddActor("tank1", tank1);
            scene.AddActor("tank2", tank2);
            scene.AddActor("balls", ball1);
            scene.AddActor("balls", ball2);
            // scene.AddActor("paddle", paddle);
            scene.AddActor("field", field);
       //     scene.AddActor("level", level);
       //     scene.AddActor("score", score);
    //        scene.AddActor("lives", lives);
        }

        private void LoadBackground(Scene scene)
        {
            IServiceFactory serviceFactory = GetServiceFactory();
            ISettingsService settingsService = serviceFactory.GetSettingsService();
            IVideoService videoService = serviceFactory.GetVideoService();
            videoService.SetBackground(Color.Black());
        }
     
    }
}

