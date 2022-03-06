
public class Attack {

    protected int Damage;
    protected int ToHit; //Valid values are 0-19

    public Attack(int Damage, int ToHit) {
        //Takes the values and initializes the attributes
        this.Damage = Damage;
        this.ToHit = ToHit;         
    }

    public int GetDamage() {
        //Getter for Damage attribute
        return this.Damage;
    }

    public int GetToHit() {
        //Getter for ToHitAttribute
        return this.ToHit;
    }
}