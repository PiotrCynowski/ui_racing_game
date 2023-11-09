public class RaceCar : Vehicle
{
    public bool isElectric;
    public bool hasNitro;

    public override void Drive()
    {
        base.Drive();
        if (hasNitro) ActivateNitro(); 
    }

    private void ActivateNitro()
    {
        // Code to activate nitro boost
    }
}