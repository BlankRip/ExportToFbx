using System;

[Serializable]
public class TestPlayerStats
{
    public int health;
    public int stamina;

    public TestPlayerStats(int hp, int stm) {
        health = hp;
        stamina = stm;
    }
}
