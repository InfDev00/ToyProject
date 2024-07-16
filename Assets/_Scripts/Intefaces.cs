using Entities;

public interface IPlayerHitHandler
{ 
    public void OnHitPlayer(PlayerController player);
}

public interface IEnemyHitHandler
{
    public void OnHitEnemy(Enemy enemy);
}

public interface IInteractObjectHitHandler
{
    public void OnHitInteractObject(InteractObject interact);
}

public interface IDamageable
{
    public void Damaged(float damage);
}