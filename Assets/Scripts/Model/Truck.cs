public class Truck : Vehicle
{
    public bool hasTrailer;

    public override void Drive()
    {
        base.Drive();
        if (hasTrailer) AttachTrailer();
    }

    private void AttachTrailer()
    {
        // Code to attach trailer
    }

    private void DetachTrailer()
    {
        // Code to detach trailer
    }
}