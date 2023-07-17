public class BonusMultBall : Bonus
{
    protected override void Activate()
    {
        base.Activate();


        Game.Instance.MultiplyBall();

        Destroy(gameObject);
    }
}
