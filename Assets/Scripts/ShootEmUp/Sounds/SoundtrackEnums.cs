namespace ShootEmUp.Sounds
{
    public enum TypeOfSoundtrack
    {
        OST=0, SFX=1
    }
    
    public enum TypeOfSFXByNumberOfSounds
    {
        Single=0, Multiple=1
    }

    public enum TypeOfSFXByItsNature
    {
        Door_Open=0,Bomber_Lights=1,Bomber_Explosion=2, FireBall_Explosion=3, FireBall_Burst=4,Arrow_DrawBow=5,
        Arrow_Fly=6, Arrow_HitNothing=7, Arrow_HitSomeThing=8, UI_MagicSkill=9,Enemy_MeatSplash=10, Player_MeatSplash=11,
        Mellee_Atack=12, Steps_Heavy=13, Steps_Light=14, None=15, UI_PlayButton_SFX=16, UI_RestartButton_SFX=17, HealPotion_Drop=18,
        HealPotion_Use=19, UI_GameOver_SFX=20, UI_Win_SFX=21, Staff_Turning=22
    }
    
    public enum TypeOfOSTByItsNature
    {
        MainMenu=0,Gameplay_Level1=1, None=2, Arena=3
    }

    public enum TypeOfSFXByPlace
    {
        Local=0, Global=1
    }
}
