
public class Attack {


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
