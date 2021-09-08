namespace All.Modes
{
    public class PlayerDataModel
    {
        public readonly float Score;
        public readonly int KillCount;
        public readonly int DeathCount;

        public PlayerDataModel(float score, int killCount, int deathCount)
        {
            Score = score;
            KillCount = killCount;
            DeathCount = deathCount;
        }
    }
}
