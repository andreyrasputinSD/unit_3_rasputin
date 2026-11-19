namespace Golf
{
    /// <summary>
    /// Статический контейнер игровых событий. Позволяет развязать классы:
    /// камень, мотыга, счёт и UI общаются через него, а не напрямую.
    /// </summary>
    public static class GameEvents
    {
        /// <summary>Два нетронутых камня соприкоснулись — конец игры.</summary>
        public static event System.Action onCollisionStones;

        /// <summary>Мотыга коснулась камня — начисляем балл.</summary>
        public static event System.Action onStickHit;

        public static void CollisionStonesInvoke(UnityEngine.Collision collision)
        {
            onCollisionStones?.Invoke();
        }

        public static void StickHit()
        {
            onStickHit?.Invoke();
        }
    }
}
