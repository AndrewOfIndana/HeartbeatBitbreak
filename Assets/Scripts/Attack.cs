
public class Attack {

    private int Damage;

    public Attack(int Damage) {
        //Takes the values and initializes the attributes
        this.Damage = Damage;
    }

    public int GetDamage() {
        //Getter for Damage attribute, called if there is no flow bonus
        return this.Damage;
    }

    public int GetDamage(int bonus) {
        //Getter for damage Attribute, called if there is a flow bonus
        return this.Damage + bonus;
    }
}
