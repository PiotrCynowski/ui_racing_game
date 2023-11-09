public class Bus : Vehicle
{
    public int lineNumber;

    public override void Drive()
    {
        base.Drive();
        Board(); 
    }

    private void Board()
    {
        // Code to board passengers
    }
}
