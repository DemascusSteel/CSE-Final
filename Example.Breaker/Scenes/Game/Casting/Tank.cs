using System;
using System.Numerics;
using Byui.Games.Casting;


namespace Example.Breaker.Game
{
    public class Tank : Byui.Games.Casting.Image
    {
        private Ball _ball;
        private int Alive = 1;

        private bool releasedFireKey;
        
        public Tank() { }

        public bool HasBall()
        {
            return _ball != null;
        }
        public Ball GetBall()
        {
            return _ball;
        }

        public override void Move()
        {
            base.Move();
            if (_ball != null)
            {
                float x = this.GetCenterX() + Convert.ToSingle(Math.Sin( (this.GetRotation() * Math.PI) / 180))*60;
                float y = this.GetCenterY() - Convert.ToSingle(Math.Cos( (this.GetRotation() * Math.PI) / 180))*60;
                _ball.MoveTo(x, y);
            }
        }
        public void SteerBall(float directionX, float directionY){
             if (_ball != null)
            {
            _ball.Steer(directionX, directionY);
            }
            
        }

        public void AttachBall(Ball ball)
        {
            _ball = ball;
        }

        public void ReleaseBall()
        {            
            _ball = null;
        }

        public bool IsFireKeyReleased()
        {
            return releasedFireKey;
        }

        public void SetFireKeyStatus(bool status)
        {
            releasedFireKey = status;
        }
         public void KillTank()
        {
            Alive = 0;
        }
        public int IsAlive(){
            return Alive;
        }
    }
}