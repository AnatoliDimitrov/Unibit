namespace PracticumPrograming.Products.Contracts
{
    public interface IProduct
    {
        string Name { get; }

        decimal Price { get; }

        double CalculateCalories(double quantity, double coeficient);
    }
}
