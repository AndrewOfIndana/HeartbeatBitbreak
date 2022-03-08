
public class Attack {

    private int Damage;
    private int ToHit;

    public Attack(int Damage, int ToHit) {
        //Takes the values and initializes the attributes
        this.Damage = Damage;
        this.ToHit = ToHit;         
    }

    public int GetDamage() {
        //Getter for Damage attribute, called if there is no flow bonus
        return this.Damage;
    }

    public int GetDamage(int bonus) {
        //Getter for damage Attribute, called if there is a flow bonus
        return this.Damage + bonus;
    }

    public int GetToHit() {
        //Getter for ToHitAttribute
        return this.ToHit;
    }
}
